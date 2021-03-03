using System;
using System.Runtime.InteropServices;

namespace ObjectDetection.Utility
{
    public class NativeCaller
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
    }
}
