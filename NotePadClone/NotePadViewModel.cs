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
    public ICommand SaveFileCommand { get; }
    public ICommand SaveAsFileCommand { get; }

    public NotePadViewModel()
    {
        NewFileCommand = new DelegateCommand(_ => NewFile());
        SaveFileCommand = new DelegateCommand(_ => SaveFile(), _ => CanSaveFile());
        SaveAsFileCommand = new DelegateCommand(_ => SaveAsFile());
    }

    private void NewFile()
    {
        TextContent = string.Empty;
        _currentFilePath = null;
    }

    private void SaveFile()
    {
        if (!string.IsNullOrEmpty(_currentFilePath))
        {
            File.WriteAllText(_currentFilePath, TextContent);
        }
        else
        {
            SaveAsFile();
        }
    }

    private void SaveAsFile()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            _currentFilePath = saveFileDialog.FileName;
            File.WriteAllText(_currentFilePath, TextContent);
        }
    }

    private bool CanSaveFile() => !string.IsNullOrEmpty(TextContent);

}
