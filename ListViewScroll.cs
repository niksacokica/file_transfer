using System.Windows.Forms;

namespace file_transfer{
    class ListViewScroll : ListView{
        public event ScrollEventHandler Scroll;

        protected virtual void OnScroll( ScrollEventArgs e ){
            this.Scroll?.Invoke( this, e );
        }
        protected override void WndProc( ref Message m ){
            base.WndProc( ref m );
            if( m.Msg == 0x114 || m.Msg == 0x115 || m.Msg == 0x020A)
                OnScroll( new ScrollEventArgs( ( ScrollEventType )( m.WParam.ToInt32() & 0xffff ), 0 ) );
        }
    }
}
