using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

using WpfEssentials.Base;

namespace NotePadClone.ViewModels;
public class MainWindowVm : WindowVm
{
    private string? _textContent;
    private string? _currentFilePath;
    private int _numberOfCharacters;
    private int _numberOfLines;
    private int _fileSizeInBytes;
    private bool _isDarkTheme = true;

    public Func<string?>? OpenFileFunc { get; set; }
    public Func<string?>? SaveFileFunc { get; set; }

    public string? TextContent
    {
        get => _textContent;
        set
        {
            if (!SetField(ref _textContent, value)) return;

            SaveFileCommand.OnCanExecuteChanged();
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

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (!SetField(ref _isDarkTheme, value)) return;
            SwitchTheme();
        }
    }

    public DelegateCommand NewDocumentCommand { get; }
    public DelegateCommand OpenFileCommand { get; }
    public DelegateCommand SaveFileCommand { get; }
    public DelegateCommand SaveAsFileCommand { get; }

    public MainWindowVm()
    {
        NewDocumentCommand = new DelegateCommand(_ => CreateNewDocument());
        OpenFileCommand = new DelegateCommand(_ => OpenFile());
        SaveFileCommand = new DelegateCommand(_ => SaveFile(), _ => CanSaveFile());
        SaveAsFileCommand = new DelegateCommand(_ => SaveAsFile());

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
        if (string.IsNullOrEmpty(TextContent))
        {
            return 0;
        }

        const string visibleCharactersRegexStr = @"[^\p{Cc}^\p{Cn}^\p{Cs}]";
        return Regex.Matches(TextContent, visibleCharactersRegexStr).Count;
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
        if (string.IsNullOrEmpty(TextContent)
            || (NumberOfNewLines == 0 && NumberOfCharacters == 0))
        {
            return 0;
        }

        return NumberOfNewLines + 1;
    }

    private void CreateNewDocument()
    {
        _currentFilePath = null;
        TextContent = string.Empty;        
    }

    private void OpenFile()
    {
        string? filePath = OpenFileFunc?.Invoke();

        if (filePath is null) return;

        _currentFilePath = filePath;
        TextContent = File.ReadAllText(_currentFilePath);
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
        string? filePath = SaveFileFunc?.Invoke();

        if (filePath is null) return;

        _currentFilePath = filePath;
        File.WriteAllText(_currentFilePath, TextContent);
        FileSizeInBytes = GetFileSizeInBytes();
    }

    private void SwitchTheme()
    {
        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();
        var baseTheme = IsDarkTheme
            ? (IBaseTheme)new MaterialDesignDarkTheme()
            : (IBaseTheme)new MaterialDesignLightTheme();

        theme.SetBaseTheme(baseTheme);
        paletteHelper.SetTheme(theme);
    }

    private bool CanSaveFile() => !string.IsNullOrEmpty(TextContent);
}
