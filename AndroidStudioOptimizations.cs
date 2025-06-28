using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class AndroidStudioOptimizations
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_HIDE = 0;

    public static void Apply(IntPtr qemuHWnd)
    {
        // The Android Studio emulator has a toolbar window that can be hidden.
        // We'll try to find it and hide it.
        IntPtr toolbarHWnd = FindWindowEx(qemuHWnd, IntPtr.Zero, "Qt5QWindowIcon", null);

        if (toolbarHWnd != IntPtr.Zero)
        {
            ShowWindow(toolbarHWnd, SW_HIDE);
            Console.WriteLine("Android Studio emulator toolbar hidden.");
        }
    }
}
