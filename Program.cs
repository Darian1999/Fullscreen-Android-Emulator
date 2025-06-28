using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class FullscreenManager
{
    // Import necessary WinAPI functions
    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

    public static void ForceFullscreen(string processName)
    {
        // Find the process by its name
        Process[] processes = Process.GetProcessesByName(processName);

        if (processes.Length > 0)
        {
            IntPtr hWnd = processes[0].MainWindowHandle;

            if (hWnd != IntPtr.Zero)
            {
                // Check for Android Studio and apply optimizations
                Process[] studioProcesses = Process.GetProcessesByName("studio64");
                if (studioProcesses.Length == 0)
                {
                    studioProcesses = Process.GetProcessesByName("studio");
                }

                if (studioProcesses.Length > 0)
                {
                    Console.WriteLine("Android Studio detected. Applying optimizations.");
                    AndroidStudioOptimizations.Apply(hWnd);
                }

                // Get screen dimensions
                int screenWidth = GetSystemMetrics(Vars.SM_CXSCREEN);
                int screenHeight = GetSystemMetrics(Vars.SM_CYSCREEN);

                // Get current window style
                int style = GetWindowLong(hWnd, Vars.GWL_STYLE);

                // Remove border, caption, and system menu
                style &= ~(Vars.WS_CAPTION | Vars.WS_THICKFRAME | Vars.WS_SYSMENU);
                SetWindowLong(hWnd, Vars.GWL_STYLE, style);

                // Resize and move the window to fill the screen
                SetWindowPos(hWnd, Vars.HWND_TOP, 0, 0, screenWidth, screenHeight, Vars.SWP_SHOWWINDOW);
            }
            else
            {
                Console.WriteLine("Main window not found for process '{0}'. The process may not have a GUI or is starting up.", processName);
            }
        }
        else
        {
            Console.WriteLine("Process '{0}' not found.", processName);
        }
    }

    // Example usage:
    public static void Main()
    {
        Process[] processes = Process.GetProcessesByName("qemu-system-x86_64");
        string processName = "qemu-system-x86_64";

        if (processes.Length == 0)
        {
            Console.WriteLine("Process 'qemu-system-x86_64' not found, looking for 'qemu-system-i386'.");
            processes = Process.GetProcessesByName("qemu-system-i386");
            processName = "qemu-system-i386";
        }

        if (processes.Length > 0)
        {
            ForceFullscreen(processName);
        }
        else
        {
            Console.WriteLine("No QEMU instance found (looked for qemu-system-x86_64 and qemu-system-i386).");
        }
    }
}
