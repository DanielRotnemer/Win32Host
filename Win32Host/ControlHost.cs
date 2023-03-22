using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Win32Host
{
    public class ControlHost : HwndHost
    {
        private IntPtr windowHandle;

        public ControlHost()
        {
            windowHandle = IntPtr.Zero;
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            windowHandle = WindowAPI.InitializeWin32(hwndParent.Handle, 800, 400);
            Debug.Assert(windowHandle != IntPtr.Zero);
            return new HandleRef(this, windowHandle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            windowHandle = IntPtr.Zero;
        }
    }
}
