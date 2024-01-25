using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WpfEssentials.Base;

namespace NotePadClone.DocumentModel;
public class DocumentMetadata : ObservableObject
{
    public string? _filePath;
    public int _numberOfCharacters;
    public int _numberOfLines;
    public int _fileSizeInBytes;

    public string? FilePath
    {
        get => _filePath;
        set => SetField(ref _filePath, value);
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

    public void Update(string documentContent)
    {
        NumberOfCharacters = GetNumberOfVisibleCharacters(documentContent);
        NumberOfLines = GetNumberOfLines(documentContent);
        FileSizeInBytes = GetFileSizeInBytes();
    }

    private static int GetNumberOfVisibleCharacters(string documentContent)
    {
        if (string.IsNullOrEmpty(documentContent))
        {
            return 0;
        }

        const string visibleCharactersRegexStr = @"[^\p{Cc}^\p{Cn}^\p{Cs}]";
        return Regex.Matches(documentContent, visibleCharactersRegexStr).Count;
    }

    private static int GetNumberOfNewLines(string documentContent) => Regex.Matches(documentContent ?? string.Empty, Environment.NewLine).Count;

    private int GetNumberOfLines(string documentContent)
    {
        var numberOfNewLines = GetNumberOfNewLines(documentContent);

        if (string.IsNullOrEmpty(documentContent)
            || (numberOfNewLines == 0 && NumberOfCharacters == 0))
        {
            return 0;
        }

        return numberOfNewLines + 1;
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
