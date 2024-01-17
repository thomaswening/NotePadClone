using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Microsoft.Win32;

using WpfEssentials.Base;

namespace NotePadClone;
internal class NotePadViewModel : ObservableObject
{
    private string? _textContent;
    private string? _currentFilePath;

    public string? TextContent
    {
        get => _textContent;
        set
        {
            if (_textContent != value)
            {
                _textContent = value;
                OnPropertyChanged(nameof(TextContent));
            }
        }
    }

    public ICommand NewFileCommand { get; }

    public NotePadViewModel()
    {
        NewFileCommand = new DelegateCommand(_ => NewFile());
    }

    private void NewFile()
    {
        TextContent = string.Empty;
        _currentFilePath = null;
    }
}
