using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32Host
{
    public class WindowAPI
    {
        private const string windowDll = @"E:\Projects\Win32Host\x64\Debug\WindowDll.dll";

        [DllImport(windowDll)]
        public static extern IntPtr InitializeWin32(IntPtr host, int width, int height);
    }
}
