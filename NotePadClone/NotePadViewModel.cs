using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    private int _numberOfCharacters;
    private int _numberOfLines;
    private int _fileSizeInBytes;

    public string? TextContent
    {
        get => _textContent;
        set
        {
            if (!SetField(ref _textContent, value)) return;
            UpdateDocumentInfo();
        }
    }

    public int NumberOfCharacters
    {
        get => _numberOfCharacters;
        set => SetField(ref _numberOfCharacters, value);
    }

    public int NumberOfLines
    {
        get => _numberOfLines;
        set => SetField(ref _numberOfLines, value);
    }

    public int FileSizeInBytes
    {
        get => _fileSizeInBytes;
        set => SetField(ref _fileSizeInBytes, value);
    }

    public ICommand NewFileCommand { get; }
    public ICommand OpenFileCommand { get; }
    public ICommand SaveFileCommand { get; }
    public ICommand SaveAsFileCommand { get; }
    public ICommand CloseCommand { get; }

    public NotePadViewModel()
    {
        NewFileCommand = new DelegateCommand(_ => NewFile());
        OpenFileCommand = new DelegateCommand(_ => OpenFile());
        SaveFileCommand = new DelegateCommand(_ => SaveFile(), _ => CanSaveFile());
        SaveAsFileCommand = new DelegateCommand(_ => SaveAsFile());
        CloseCommand = new DelegateCommand(_ => CloseApplication());

        UpdateDocumentInfo();
    }

    private void UpdateDocumentInfo()
    {
        NumberOfCharacters = GetNumberOfVisibleCharacters();
        NumberOfLines = GetNumberOfLines();
        FileSizeInBytes = GetFileSizeInBytes();
    }

    private int GetNumberOfVisibleCharacters()
    {
        if (NumberOfNewLines == 0)
        {
            return TextContent?.Length - NumberOfNewLines ?? 0;
        }

        return TextContent!.Length - NumberOfNewLines - 1;
    }

    private int NumberOfNewLines => Regex.Matches(TextContent ?? string.Empty, Environment.NewLine).Count;

    private int GetFileSizeInBytes() 
    {
        if (string.IsNullOrEmpty(_currentFilePath))
        {
            return 0;
        }

        return (int)Convert.ToInt64(new FileInfo(_currentFilePath).Length);
    }

    private int GetNumberOfLines()
    {
        if (NumberOfNewLines == 0 && NumberOfCharacters == 0)
        {
            return 0;
        }

        return NumberOfNewLines + 1;
    }

    private void NewFile()
    {
        TextContent = string.Empty;
        _currentFilePath = null;
    }

    private void OpenFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            _currentFilePath = openFileDialog.FileName;
            TextContent = File.ReadAllText(_currentFilePath);
        }
    }

    private void SaveFile()
    {
        if (!string.IsNullOrEmpty(_currentFilePath))
        {
            File.WriteAllText(_currentFilePath, TextContent);
            FileSizeInBytes = GetFileSizeInBytes();
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
            FileSizeInBytes = GetFileSizeInBytes();
        }
    }

    private bool CanSaveFile() => !string.IsNullOrEmpty(TextContent);
    private static void CloseApplication() => Application.Current.Shutdown();
}
