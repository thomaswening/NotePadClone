using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using NotePadClone.ViewModels;

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

    public static void SubscribeToWindowEvents(this Window window, WindowVm viewModel)
    {
        viewModel.CloseWindowRequestedEvent += (s, e) => window.Close();
        viewModel.MinimizeWindowRequestedEvent += (s, e) => window.Minimize();
        viewModel.MaximizeWindowRequestedEvent += (s, e) => window.Maximize();
        viewModel.RestoreWindowRequestedEvent += (s, e) => window.Restore();
    }
}
