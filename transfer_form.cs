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

        delegate void SetChangeCallback();
        delegate void AddProgress( string key, string name, string size, int max );
        delegate void UpdateProgress( string key, int value );

        public transfer_form(){
            InitializeComponent();
        }

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

        private void updateProgressBar( string key, int value ){
            if( send_list.InvokeRequired )
                send_list.Invoke( new UpdateProgress( updateProgressBar ), new object[] { key, value } );
            else{
                ProgressBar pb = send_list.Controls.OfType<ProgressBar>().FirstOrDefault( q => q.Name == key );
                if( pb != null )
                    pb.Value = value;
            }
        }

        private void addProgressSend( string key, string name, string size, int max ){
            if( send_list.InvokeRequired )
                send_list.Invoke( new AddProgress( addProgressSend ), new object[] { key, name, size, max } );
            else{
                ListViewItem lvi = new ListViewItem();
                ProgressBar pb = new ProgressBar();

                lvi.SubItems[0].Text = name;
                lvi.SubItems.Add( size );
                lvi.SubItems.Add( "" );
                send_list.Items.Add( lvi );

                Rectangle rec = lvi.SubItems[2].Bounds;
                pb.SetBounds( rec.X, rec.Y, rec.Width, rec.Height );
                pb.Minimum = 0;
                pb.Maximum = max;
                pb.Value = 0;
                pb.Name = key;
                send_list.Controls.Add( pb );
            }
        }

        #region form_actions

        private void stopToolStripMenuItem_Click( object sender, EventArgs e ){
            MessageBox.Show( "Are you sure you want to stop the ability to receive files?\nThis will cancel any ongoing transfers!", "Stop",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning );
        }

        private void transfer_form_FormClosing (object sender, FormClosingEventArgs e ){
            if( MessageBox.Show( "Are you sure you want to close the program?\nThis will cancel any ongoing transfers!", "Close",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.No )
                e.Cancel = true;
        }

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
                MessageBox.Show( "Invalid path for a file!", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning );
        }

        private void open_file_but_Click( object sender, EventArgs e ){
            if( open_file_dialog.ShowDialog() == DialogResult.OK ){
                file_path_chat.Text = open_file_dialog.FileName;
                file_path_chat.ForeColor = Color.Black;
            }
        }

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

        private void callback( IAsyncResult iar ){
            TcpClient client = listener.EndAcceptTcpClient( iar );

            listener.BeginAcceptTcpClient( new AsyncCallback( callback ), null );
            NetworkStream ns = client.GetStream();

            byte[] buffer = new byte[2621440];
            string read = "";
            try{
                ns.Read( buffer, 0, buffer.Length );
            }catch { }

            int n = buffer.Length - 1;
            if( n > 0 ){
                while ( buffer[n] == 0 )
                    --n;
                byte[] r = new byte[n + 1];
                Array.Copy( buffer, r, n + 1 );
                read = Encoding.UTF8.GetString( r );
            }

            Dictionary<string, string> msg = JsonConvert.DeserializeObject<Dictionary<string, string>>( @read );

            File.Create( msg["file_name"] ).Dispose();
            FileStream fs = new FileStream( msg["file_name"], FileMode.Append );
            for( int i = 0; i < int.Parse( msg["total_parts"] ) - 1; i++ ){
                try{
                    ns.Read( buffer, 0, buffer.Length );
                }catch { }

                if( buffer.Length > 0 )
                    fs.Write( buffer, 0, buffer.Length );
            }

            MemoryStream ms = new MemoryStream();
            ns.CopyTo( ms );
            fs.Write( ms.ToArray(), 0, ms.ToArray().Length );
            fs.Close();
        }

        private void menu_file_start_Click( object sender, EventArgs e ){
            listener.Start();
            listener.BeginAcceptTcpClient( new AsyncCallback( callback ), null );
        }

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

            byte[] buffer = new byte[2621440];
            FileStream fs = File.Open( path, FileMode.Open, FileAccess.Read );
            NetworkStream ns = client.GetStream();

            int max = (int)Math.Ceiling((double)((int)fs.Length / 2621440)) + 1;
            addProgressSend( client.Client.LocalEndPoint.ToString(), Path.GetFileName( path ), fs.Length.ToString(), max );
            Dictionary<string, string> msg = new Dictionary<string, string>(){
                { "file_name", Path.GetFileName( path ) },
                { "total_parts", max.ToString() }
            };
            string json = JsonConvert.SerializeObject( msg );
            byte[] send = Encoding.UTF8.GetBytes( json.ToCharArray(), 0, json.Length );
            Array.Copy( send, buffer, send.Length );
            ns.Write( buffer, 0, buffer.Length );

            int bytesRead;
            int c = 1;
            BufferedStream bs = new BufferedStream( fs );
            while( ( bytesRead = bs.Read( buffer, 0, 2621440 ) ) != 0 ){
                updateProgressBar( client.Client.LocalEndPoint.ToString(), c );
                ns.Write( buffer, 0, bytesRead );
                c++;
            }

            fs.Close();
            ns.Close();
            client.Close();
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
    }
}
