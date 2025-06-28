using System;

public static class Vars
{
    // Window style constants
    public const int GWL_STYLE = -16;
    public const int WS_CAPTION = 0x00C00000;
    public const int WS_THICKFRAME = 0x00040000;
    public const int WS_SYSMENU = 0x00080000;

    // Screen metrics constants
    public const int SM_CXSCREEN = 0;
    public const int SM_CYSCREEN = 1;

    // SetWindowPos constants
    public static readonly IntPtr HWND_TOP = new IntPtr(0);
    public const uint SWP_SHOWWINDOW = 0x0040;
}
