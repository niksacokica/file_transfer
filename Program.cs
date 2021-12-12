using System;
using System.Windows.Forms;

namespace file_transfer{
    static class Program{
        [STAThread]
        static void Main(){
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new transfer_form() );
        }
    }
}
