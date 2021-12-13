using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace file_transfer{
    static class Program{
        [STAThread]
        static void Main(){
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new transfer_form() );
        }

        [DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = false )]
        static extern IntPtr SendMessage( IntPtr hWnd, uint Msg, IntPtr w, IntPtr l );
        public static void SetState( this ProgressBar p, int state ){
            SendMessage( p.Handle, 1040, (IntPtr)state, IntPtr.Zero );
        }
    }
}
