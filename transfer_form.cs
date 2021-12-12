using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace file_transfer{

    public partial class transfer_form : Form{

        private TcpListener listener = new TcpListener( IPAddress.Parse( "127.0.0.1" ), 1507 );
        private List<TcpClient> connected = new List<TcpClient>();

        private bool recOn = false;

        #region delegate_callbacks

        delegate void SetChangeCallback();
        delegate void AddProg( ListViewScroll list, string key, string name, string size, int max );
        delegate void UpdateProgress( ListViewScroll list, string key, int value );
        delegate void UpdateProg( ListViewScroll list, int files );
        delegate void CancelProg( ListViewScroll list, string key );

        #endregion

        private int sentFiles = 0;
        private int recFiles = 0;

        public transfer_form(){
            InitializeComponent();
        }

        #region callbacks

        private void changeState(){
            if( this.InvokeRequired )
                this.Invoke( new SetChangeCallback( changeState ), new object[] {} );
            else{
                file_path_chat.Enabled = !file_path_chat.Enabled;
                open_file_but.Enabled = !open_file_but.Enabled;
                send_file_but.Enabled = !send_file_but.Enabled;
                ip_port_chat.Enabled = !ip_port_chat.Enabled;
            }
        }

        private void updateBars( ListViewScroll list, int files ){
            if( list.InvokeRequired )
                list.Invoke( new UpdateProg( updateBars ), new object[] { list, files } );
            else{
                for( int i=0; i < files; i++ ){
                    Rectangle rec = list.Items.Cast<ListViewItem>().FirstOrDefault( q => q.SubItems[3].Text == i.ToString() ).SubItems[2].Bounds;
                    ProgressBar pb = list.Controls.OfType<ProgressBar>().FirstOrDefault( q => q.Name == i.ToString() );
                    pb.SetBounds( rec.X, rec.Y, rec.Width, rec.Height );
                    pb.Visible =  rec.Y >= 20;
                }

            }
        }

        private void updateProgressBar( ListViewScroll list, string key, int value ){
            if( list.InvokeRequired )
                list.Invoke( new UpdateProgress( updateProgressBar ), new object[] { list, key, value } );
            else
                list.Controls.OfType<ProgressBar>().FirstOrDefault( q => q.Name == key ).Value = value;
        }

        private void addProgress( ListViewScroll list, string key, string name, string size, int max ){
            if( send_list.InvokeRequired )
                send_list.Invoke( new AddProg( addProgress ), new object[] { list, key, name, size, max } );
            else{
                ListViewItem lvi = new ListViewItem();
                ProgressBar pb = new ProgressBar();

                lvi.SubItems[0].Text = name;
                lvi.SubItems.Add( size );
                lvi.SubItems.Add( "" );
                lvi.SubItems.Add( key );
                list.Items.Add( lvi );

                Rectangle rec = lvi.SubItems[2].Bounds;
                pb.SetBounds( rec.X, rec.Y, rec.Width, rec.Height );
                pb.Minimum = 0;
                pb.Maximum = max;
                pb.Value = 0;
                pb.Name = key;
                list.Controls.Add( pb );
            }
        }

        private void cancelProgress( ListViewScroll list, string key ){
            if( send_list.InvokeRequired )
                send_list.Invoke( new CancelProg( cancelProgress ), new object[] { list, key } );
            else
                list.Controls.OfType<ProgressBar>().FirstOrDefault( q => q.Name == key ).BackColor = Color.Red;;
        }

        #endregion

        #region form_actions

        private void transfer_form_FormClosing( object sender, FormClosingEventArgs e ){
            if( MessageBox.Show( "Are you sure you want to close the program?\nThis will cancel any ongoing transfers!", "Close",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.No )
                e.Cancel = true;
        }

        #region send_files

        private void file_path_chat_DragOver( object sender, DragEventArgs e ){
            if( e.Data.GetDataPresent( DataFormats.FileDrop ) )
                e.Effect = DragDropEffects.Link;
        }

        private void file_path_chat_DragDrop( object sender, DragEventArgs e ){
            string[] path = e.Data.GetData( DataFormats.FileDrop ) as string[]; // get all files droppeds  
            if( path.Length > 0 && File.Exists( path[0] ) ){
                file_path_chat.Text = path[0];
                file_path_chat.ForeColor = Color.Black;
            }
            else
                MessageBox.Show( "Invalid path for a file!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
        }

        private void open_file_but_Click( object sender, EventArgs e ){
            if( open_file_dialog.ShowDialog() == DialogResult.OK ){
                file_path_chat.Text = open_file_dialog.FileName;
                file_path_chat.ForeColor = Color.Black;
            }
        }

        private void send_file_but_Click( object sender, EventArgs e ){
            if( !File.Exists( file_path_chat.Text ) )
                MessageBox.Show( "Invalid file path!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            else if( !checkIfIpPort( ip_port_chat.Text ) )
                MessageBox.Show( "Invalid IP/PORT!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            else
                Task.Run( () => sendFile( file_path_chat.Text, ip_port_chat.Text.Split( ':' )[0], int.Parse( ip_port_chat.Text.Split( ':' )[1] ) ) );
        }

        private void ip_port_chat_KeyPress( object sender, KeyPressEventArgs e ){
            if( e.KeyChar == ( char )13 && !string.IsNullOrEmpty( ip_port_chat.Text ) ){
                if( !File.Exists( ip_port_chat.Text ) )
                    MessageBox.Show( "Invalid file path!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                else if( !checkIfIpPort( ip_port_chat.Text ) )
                    MessageBox.Show( "Invalid IP/PORT!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                else
                    Task.Run( () => sendFile( file_path_chat.Text, ip_port_chat.Text.Split( ':' )[0], int.Parse( ip_port_chat.Text.Split( ':' )[1] ) ) );
            }
        }

        #endregion

        #region receiving_files

        private void callback( IAsyncResult iar ){
            TcpClient client = listener.EndAcceptTcpClient( iar );
            connected.Add( client );
            if( !recOn ){
                listener.Stop();
                return;
            }

            listener.BeginAcceptTcpClient( new AsyncCallback( callback ), null );
            NetworkStream ns = client.GetStream();

            int id = recFiles++;
            byte[] buffer = new byte[2621440];
            string read = "";
            try{
                ns.Read( buffer, 0, buffer.Length );
            }catch{
                if( !client.Connected )
                    return;
            }

            int n = buffer.Length - 1;
            if( n > 0 ){
                while ( buffer[n] == 0 )
                    --n;
                byte[] r = new byte[n + 1];
                Array.Copy( buffer, r, n + 1 );
                read = Encoding.UTF8.GetString( r );
            }

            Dictionary<string, string> msg = JsonConvert.DeserializeObject<Dictionary<string, string>>( @read );
            addProgress( rec_list, id.ToString(), msg["file_name"], msg["file_size"], int.Parse( msg["total_parts"] ) );

            File.Create( msg["file_name"] ).Dispose();
            FileStream fs = new FileStream( msg["file_name"], FileMode.Append );
            for( int i = 1; i < int.Parse( msg["total_parts"] ); i++ ){
                try{
                    ns.Read( buffer, 0, buffer.Length );
                }catch{
                    if( !client.Connected ){
                        fs.Close();
                        cancelProgress( rec_list, id.ToString() );
                        return;
                    }
                }

                if( buffer.Length > 0 ){
                    fs.Write( buffer, 0, buffer.Length );

                    updateProgressBar( rec_list, id.ToString(), i );
                }
            }

            MemoryStream ms = new MemoryStream();
            if( !client.Connected ){
                fs.Close();
                cancelProgress( rec_list, id.ToString() );
                return;
            }
            ns.CopyTo( ms );
            fs.Write( ms.ToArray(), 0, ms.ToArray().Length );
            updateProgressBar( rec_list, id.ToString(), int.Parse( msg["total_parts"] ) );
            fs.Close();
        }

        private void start_rec_but_Click( object sender, EventArgs e ){
            if( !checkIfIpPort( your_ip_port_chat.Text ) )
                    MessageBox.Show( "Invalid IP/PORT!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            else{
                listener.Start();
                listener.BeginAcceptTcpClient( new AsyncCallback( callback ), null );

                start_rec_but.Enabled = false;
                stop_rec_but.Enabled = true;

                recOn = true;
            }
        }

        private void stop_rec_but_Click( object sender, EventArgs e ){
            MessageBox.Show( "Are you sure you want to stop the ability to receive files?\nThis will cancel any ongoing transfers!", "Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Warning );

            recOn = false;
            new TcpClient( "127.0.0.1", 1507 );

            start_rec_but.Enabled = true;
            stop_rec_but.Enabled = false;
            disconnectAll();
        }

        #endregion

        #region update_progress_bars

        private void rec_list_ColumnWidthChanging( object sender, ColumnWidthChangingEventArgs e ){
            updateBars( rec_list, recFiles );
        }

        private void rec_list_Scroll( object sender, ScrollEventArgs e ){
            updateBars( rec_list, recFiles );
        }

        private void send_list_Scroll( object sender, ScrollEventArgs e ){
            updateBars( send_list, sentFiles );
        }

        private void send_list_ColumnWidthChanging( object sender, ColumnWidthChangingEventArgs e ){
            updateBars( send_list, sentFiles );
        }

        private void transfer_form_SizeChanged( object sender, EventArgs e ){
            updateBars( send_list, sentFiles );
            updateBars( rec_list, recFiles );
        }

        #endregion

        #region shadow_text

        private void file_path_chat_Enter( object sender, EventArgs e ){
            if( file_path_chat.Text == "File path here." ){
                file_path_chat.Text = "";
                file_path_chat.ForeColor = Color.Black;
            }
        }

        private void file_path_chat_Leave( object sender, EventArgs e ){
            if( string.IsNullOrEmpty( file_path_chat.Text ) ){
                file_path_chat.Text = "File path here.";
                file_path_chat.ForeColor = Color.Gray;
            }
        }

        private void ip_port_chat_Enter( object sender, EventArgs e ){
            if( ip_port_chat.Text == "IP and PORT here." ){
                ip_port_chat.Text = "";
                ip_port_chat.ForeColor = Color.Black;
            }
        }

        private void ip_port_chat_Leave( object sender, EventArgs e ){
            if( string.IsNullOrEmpty(ip_port_chat.Text ) ){
                ip_port_chat.Text = "IP and PORT here.";
                ip_port_chat.ForeColor = Color.Gray;
            }
        }

        private void your_ip_port_chat_Enter( object sender, EventArgs e ){
            if( your_ip_port_chat.Text == "Your IP and PORT here." ){
                your_ip_port_chat.Text = "";
                your_ip_port_chat.ForeColor = Color.Black;
            }
        }

        private void your_ip_port_chat_Leave( object sender, EventArgs e ){
            if( string.IsNullOrEmpty( your_ip_port_chat.Text ) ){
                your_ip_port_chat.Text = "Your IP and PORT here.";
                your_ip_port_chat.ForeColor = Color.Gray;
            }
        }

        #endregion

        #endregion

        private void sendFile( string path, string ip, int port ){
            changeState();
            TcpClient client;

            try{
                client = new TcpClient( ip, port );
            }catch( Exception ex ){
                MessageBox.Show( ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            changeState();

            int id = sentFiles++;
            byte[] buffer = new byte[2621440];
            FileStream fs = File.Open( path, FileMode.Open, FileAccess.Read );
            NetworkStream ns = client.GetStream();

            int max = (int)Math.Ceiling((double)((int)fs.Length / 2621440)) + 1;
            addProgress( send_list, id.ToString(), path, fs.Length.ToString(), max );
            Dictionary<string, string> msg = new Dictionary<string, string>(){
                { "file_name", Path.GetFileName( path ) },
                { "file_size", fs.Length.ToString() },
                { "total_parts", max.ToString() }
            };
            string json = JsonConvert.SerializeObject( msg );
            byte[] send = Encoding.UTF8.GetBytes( json.ToCharArray(), 0, json.Length );
            Array.Copy( send, buffer, send.Length );
            if( !client.Connected )
                return;
            
            ns.Write( buffer, 0, buffer.Length );

            int bytesRead;
            int c = 1;
            BufferedStream bs = new BufferedStream( fs );
            while( ( bytesRead = bs.Read( buffer, 0, 2621440 ) ) != 0 ){
                if( !client.Connected ){
                    cancelProgress( send_list, id.ToString() );
                    break;
                }
                ns.Write( buffer, 0, bytesRead );
                updateProgressBar( send_list, id.ToString(), c );
                c++;
            }

            fs.Close();
            ns.Close();
            if( client.Connected ){
                ns.Close();
                client.Close();
            }
        }

        private bool checkIfIpPort( string sus ){
            if( sus.Count( d => d == '.' ) != 3 || sus.Count( d => d == ':' ) != 1 )
                return false;

            string[] check = sus.Split( '.' );
            for( int i=0; i < check.Length - 1; i++ ){
                try{
                    int num = Int32.Parse( check[i] );

                    if( num > 255 || num < 0 )
                        return false;
                }
                catch{
                    return false;
                }
            }

            check = check[check.Length - 1].Split( ':' );
            try{
                int num = Int32.Parse( check[0] );
                if( num > 255 || num < 0 )
                    return false;

                num= Int32.Parse( check[1] );
                if( num > 65353 || num < 0 )
                    return false;
            }catch{
                return false;
            }

            return true;
        }

        private void disconnectAll(){
            foreach( TcpClient cl in connected ){
                cl.GetStream().Close();
                cl.Close();
            }
        }
    }
}
