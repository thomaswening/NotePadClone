using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WpfEssentials.Base;

namespace NotePadClone.ViewModels;

/// <summary>
/// Base class for all view models that represent a window.
/// </summary>
public class WindowVm : ObservableObject
{
    private bool _isDarkTheme = true;

    public event EventHandler? CloseWindowRequestedEvent;
    public event EventHandler? MinimizeWindowRequestedEvent;
    public event EventHandler? MaximizeWindowRequestedEvent;
    public event EventHandler? RestoreWindowRequestedEvent;

    public Action? SwitchThemeAction { get; set; }

    public DelegateCommand MinimizeWindowCommand { get; }
    public DelegateCommand MaximizeWindowCommand { get; }
    public DelegateCommand RestoreWindowCommand { get; }
    public DelegateCommand CloseWindowCommand { get; }

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (!SetField(ref _isDarkTheme, value))
                return;

            SwitchThemeAction?.Invoke();
        }
    }

    public WindowVm()
    {
        MinimizeWindowCommand = new DelegateCommand(_ => MinimizeWindowRequestedEvent?.Invoke(this, EventArgs.Empty));
        MaximizeWindowCommand = new DelegateCommand(_ => MaximizeWindowRequestedEvent?.Invoke(this, EventArgs.Empty));
        RestoreWindowCommand = new DelegateCommand(_ => RestoreWindowRequestedEvent?.Invoke(this, EventArgs.Empty));
        CloseWindowCommand = new DelegateCommand(_ => CloseWindowRequestedEvent?.Invoke(this, EventArgs.Empty));
    }
}
