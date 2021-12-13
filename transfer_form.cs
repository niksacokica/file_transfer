using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace file_transfer{

    public partial class transfer_form : Form{

        private TcpListener listener = new TcpListener( IPAddress.Parse( "127.0.0.1" ), 1507 );
        private List<TcpClient> connected = new List<TcpClient>();

        private static ECDiffieHellmanCng dh = new ECDiffieHellmanCng{
                KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
                HashAlgorithm = CngAlgorithm.Sha256
            };
        private byte[] pKey = dh.PublicKey.ToByteArray();

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
            if( list.InvokeRequired )
                list.Invoke( new CancelProg( cancelProgress ), new object[] { list, key } );
            else
                list.Controls.OfType<ProgressBar>().FirstOrDefault( q => q.Name == key ).SetState( 2 );
        }

        #endregion

        #region encryption_decryption

        public byte[] encrypt( byte[] pubKey, byte[] message, byte[] iv ){
            CngKey key = CngKey.Import( pubKey, CngKeyBlobFormat.EccPublicBlob );
            byte[] simpKey = dh.DeriveKeyMaterial( key );
            Aes aes = new AesCryptoServiceProvider{
                Key = simpKey,
                IV = iv
            };

            MemoryStream ms = new MemoryStream();
            ICryptoTransform en = aes.CreateEncryptor();
            CryptoStream cs = new CryptoStream( ms, en, CryptoStreamMode.Write );
            cs.Write( message, 0, message.Length );

            return ms.ToArray();
        }

        public byte[] decrypt( byte[] pubKey, byte[] message, byte[] iv ){
            CngKey key = CngKey.Import( pubKey, CngKeyBlobFormat.EccPublicBlob );
            byte[] simpKey = dh.DeriveKeyMaterial( key );
            Aes aes = new AesCryptoServiceProvider{
                Key = simpKey,
                IV = iv
            };

            MemoryStream ms = new MemoryStream();
            ICryptoTransform de = aes.CreateDecryptor();
            CryptoStream cs = new CryptoStream( ms, de, CryptoStreamMode.Write );
            cs.Write( message, 0, message.Length );

            MessageBox.Show( "decryption: " + message.Length.ToString() + " - " + ms.Length.ToString(), "message", MessageBoxButtons.OK, MessageBoxIcon.Information );

            return ms.ToArray();
        }

        #endregion

        #region form_actions

        private void transfer_form_FormClosing( object sender, FormClosingEventArgs e ){
            if( recOn && MessageBox.Show( "Are you sure you want to close the program?\nThis will cancel any ongoing transfers!", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.No )
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
                connected.Remove( client );
                return;
            }

            listener.BeginAcceptTcpClient( new AsyncCallback( callback ), null );
            NetworkStream ns = client.GetStream();
            byte[] spkey = new byte[521];
            try{
                ns.Read( spkey, 0, spkey.Length );
            }catch{}

            ns.Write( pKey, 0, pKey.Length );            

            int id = recFiles++;
            byte[] buffer = new byte[2621440];
            try{
                ns.Read( buffer, 0, buffer.Length );
            }catch{
                if( !client.Connected ){
                    connected.Remove( client );
                    return;
                }
            }

            byte[] iv = new byte[16];
            for(int i=0; i<16; i++){
                iv[i] = (byte)( pKey[i] + spkey[i] );
            }

            int n = buffer.Length - 1;
            while( (int)buffer[n] == 0 )
                --n;
            byte[] r = new byte[n + 1];
            Array.Copy( buffer, r, n + 1 );

            MessageBox.Show( string.Join( " ", r.Select(x => (int)x).ToArray().Take(100)), "b", MessageBoxButtons.OK, MessageBoxIcon.Information );

            //Dictionary<string, string> msg = JsonConvert.DeserializeObject<Dictionary<string, string>>( Encoding.UTF8.GetString( decrypt( spkey, r, iv ) ) );

            byte[] dec = decrypt( spkey, r, iv );

            MessageBox.Show( "dec: " + string.Join( " ", dec.Select(x => (int)x).ToArray().Take(100)), "message", MessageBoxButtons.OK, MessageBoxIcon.Information );
            MessageBox.Show( "message: " + Encoding.UTF8.GetString( dec ), "message", MessageBoxButtons.OK, MessageBoxIcon.Information );

            /*if( File.Exists( msg["file_name"] ) && MessageBox.Show( "File " + msg["file_name"] + " already exists, do you want to overwrite it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No ){
                connected.Remove( client );
                ns.Close();
                client.Close();
                return;
            }else
                File.Delete( msg["file_name"] );

            addProgress( rec_list, id.ToString(), msg["file_name"], msg["file_size"], int.Parse( msg["total_parts"] ) );

            System.Timers.Timer cc = new System.Timers.Timer( 1000 );
            cc.Elapsed += delegate {
                if( !client.Connected ){
                    cancelProgress( rec_list, id.ToString() );
                    connected.Remove( client );
                    cc.Stop();
                    cc.Dispose();
                    return;
                }
            };
            cc.AutoReset = true;
            cc.Enabled = true;

            File.Create( msg["file_name"] ).Dispose();
            FileStream fs = new FileStream( msg["file_name"], FileMode.Append );
            for( int i = 1; i < int.Parse( msg["total_parts"] )+1; i++ ){
                try{
                    ns.Read( buffer, 0, buffer.Length );
                }catch{
                    if( !client.Connected ){
                        fs.Close();
                        cancelProgress( rec_list, id.ToString() );
                        connected.Remove( client );
                        cc.Stop();
                        cc.Dispose();
                        return;
                    }
                }

                if( buffer.Length > 0 ){
                    fs.Write( buffer, 0, buffer.Length );

                    updateProgressBar( rec_list, id.ToString(), i );
                }
            }

            if( !client.Connected ){
                fs.Close();
                cancelProgress( rec_list, id.ToString() );
                connected.Remove( client );
                cc.Stop();
                cc.Dispose();
                return;
            }
            try{
                //File.Create( "temp.txt" ).Dispose();
                //FileStream fsa = new FileStream("temp.txt", FileMode.Append);
                //MemoryStream ms = new MemoryStream();
                //ns.CopyTo( ms );
                //fs.Write( ms.ToArray(), 0, ms.ToArray().Length );
                //fsa.Write( ms.ToArray(), 0, ms.ToArray().Length );
                //fsa.Close();
                //updateProgressBar( rec_list, id.ToString(), int.Parse( msg["total_parts"] ) );
            }catch{}
            fs.Close();

            cc.Stop();
            cc.Dispose();
            ns.Flush();
            ns.Close();
            client.Close();
            connected.Remove( client );*/
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
            if( recOn && MessageBox.Show( "Are you sure you want to stop the ability to receive files?\nThis will cancel any ongoing transfers!", "Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Warning ) == DialogResult.Yes ){
                recOn = false;
                new TcpClient( "127.0.0.1", 1507 );

                start_rec_but.Enabled = true;
                stop_rec_but.Enabled = false;
                disconnectAll();
            }
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
                changeState();
                return;
            }

            changeState();

            NetworkStream ns = client.GetStream();
            ns.Write( pKey, 0, pKey.Length );

            byte[] cpkey = new byte[521];
            try{
                ns.Read( cpkey, 0, cpkey.Length );
            }catch{}

            byte[] iv = new byte[16];
            for(int i=0; i<16; i++){
                iv[i] = (byte)( pKey[i] + cpkey[i] );
            }

            Dictionary<string, string> msg = new Dictionary<string, string>(){
                { "file_name", Path.GetFileName( path ) },
                { "file_size", "" },//fs.Length.ToString() },
                { "total_parts", "" }//max.ToString() }
            };
            string json = JsonConvert.SerializeObject(msg);
            byte[] senda = encrypt(cpkey, Encoding.UTF8.GetBytes(json.ToCharArray(), 0, json.Length), iv);

            byte[] send = encrypt( cpkey, Encoding.UTF8.GetBytes( "hello you stupid ass fucking shit".ToCharArray(), 0, "hello you stupid ass fucking shit".Length ), iv );
            ns.Write( send, 0, send.Length );

            /*int id = sentFiles++;
            FileStream fs = File.Open( path, FileMode.Open, FileAccess.Read );
            int max = (int)Math.Ceiling((double)((int)fs.Length / 2621440)) + 1;
            addProgress( send_list, id.ToString(), path, fs.Length.ToString(), max );

            Dictionary<string, string> msg = new Dictionary<string, string>(){
                { "file_name", Path.GetFileName( path ) },
                { "file_size", fs.Length.ToString() },
                { "total_parts", max.ToString() }
            };
            string json = JsonConvert.SerializeObject( msg );
            byte[] send = encrypt( cpkey, Encoding.UTF8.GetBytes( json.ToCharArray(), 0, json.Length ), iv );
            if( !client.Connected )
                return;
            
            ns.Write( send, 0, send.Length );*/

            /*int bytesRead;
            int c = 1;
            byte[] buffer = new byte[2621440];
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
            if( client.Connected ){
                ns.Close();
                client.Close();
            }*/
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
            foreach( TcpClient cl in connected.ToList() ){
                if( cl.Connected ){
                    cl.GetStream().Close();
                    cl.Close();
                    connected.Remove( cl );
                }
            }
        }
    }
}
