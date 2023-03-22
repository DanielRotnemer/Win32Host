using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Win32Host
{
    /// <summary>
    /// Interaction logic for Win32View.xaml
    /// </summary>
    public partial class Win32View : UserControl
    {
        private enum Win32Msg
        {
            WM_SIZE = 0x0005,
            WM_SIZING = 0x0214,
            WM_ENTERSIZEMOVE = 0X0231,
            WM_EXITSIZEMOVE = 0x0232
        }

        private ControlHost host = null;
        private bool disposed;

        public Win32View()
        {
            InitializeComponent();
            Loaded += Win32ViewLoaded;
        }

        private void Win32ViewLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Win32ViewLoaded;
            host = new ControlHost();
            host.MessageHook += new System.Windows.Interop.HwndSourceHook(HostMessageFilter);
            Content = host;
        }

        private IntPtr HostMessageFilter(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam,
            ref bool handled)
        {
            switch ((Win32Msg)msg)
            {
                case Win32Msg.WM_SIZING: break;
                case Win32Msg.WM_ENTERSIZEMOVE: break;
                case Win32Msg.WM_EXITSIZEMOVE: break;
                case Win32Msg.WM_SIZE: break;
                default: break;
            }
            return IntPtr.Zero;
        }

        // implementation

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    host.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
