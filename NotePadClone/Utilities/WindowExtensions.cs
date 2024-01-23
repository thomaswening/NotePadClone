using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotePadClone.Utilities;

/// <summary>
/// Extension methods for the <see cref="Window"/> class.
/// </summary>
public static class WindowExtensions
{
    public static void Minimize(this Window window)
    {
        window.WindowState = WindowState.Minimized;
    }

    public static void Maximize(this Window window)
    {
        window.WindowState = WindowState.Maximized;
    }

    public static void Restore(this Window window)
    {
        window.WindowState = WindowState.Normal;
    }
}
