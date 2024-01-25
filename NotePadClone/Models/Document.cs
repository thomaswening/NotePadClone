using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WpfEssentials.Base;

namespace NotePadClone.Models;
public class Document : ObservableObject, IDocument
{
    private string _content = string.Empty;
    public string? _filePath;

    public Document() { }
    public Document(string? filePath, string content)
    {
        FilePath = filePath;
        Content = content;
    }

    public string Content 
    { 
        get => _content; 
        set
        {
            if (!SetField(ref _content, value)) return;

            OnPropertyChanged(nameof(NumberOfCharacters));
            OnPropertyChanged(nameof(NumberOfLines));
            OnPropertyChanged(nameof(FileSizeInBytes));
        }
    }

    public string? FilePath
    {
        get => _filePath; 
        set => SetField(ref _filePath, value);
    }

    public int NumberOfCharacters => GetNumberOfVisibleCharacters();

    public int NumberOfLines => GetNumberOfLines();

    public int FileSizeInBytes => GetFileSizeInBytes();

    private int GetNumberOfVisibleCharacters()
    {
        if (string.IsNullOrEmpty(Content))
        {
            return 0;
        }

        const string visibleCharactersRegexStr = @"[^\p{Cc}^\p{Cn}^\p{Cs}]";
        return Regex.Matches(Content, visibleCharactersRegexStr).Count;
    }

    private int NumberOfNewLines => Regex.Matches(Content ?? string.Empty, Environment.NewLine).Count;

    private int GetNumberOfLines()
    {
        if (string.IsNullOrEmpty(Content)
            || (NumberOfNewLines == 0 && NumberOfCharacters == 0))
        {
            return 0;
        }

        return NumberOfNewLines + 1;
    }

    private int GetFileSizeInBytes()
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            return 0;
        }

        return (int)Convert.ToInt64(new FileInfo(FilePath).Length);
    }
}
