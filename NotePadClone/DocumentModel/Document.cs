using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WpfEssentials.Base;

namespace NotePadClone.DocumentModel;
public class Document : ObservableObject, IDocument
{
    private string _content = string.Empty;

    public Document() { }
    public Document(string? filePath, string content)
    {
        Content = content;
        Metadata.FilePath = filePath;
    }
    public DocumentMetadata Metadata { get; } = new();

    public string Content
    {
        get => _content;
        set
        {
            if (!SetField(ref _content, value))
                return;

            Metadata.Update(Content);
        }
    }
}
