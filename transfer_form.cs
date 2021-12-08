using System;
using System.Windows.Forms;

namespace file_transfer{

    public partial class transfer_form : Form{

        public transfer_form(){
            InitializeComponent();
        }

        private void stopToolStripMenuItem_Click( object sender, EventArgs e ){
            DialogResult res = MessageBox.Show( "Are you sure you want to stop the ability to receive files?\nThis will cancel any ongoing transfers!", "Stop",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning );
        }
    }
}
