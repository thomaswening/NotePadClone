using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WpfEssentials.Base;

namespace NotePadClone.DocumentModel;

/// <summary>
/// Represents a document.
/// </summary>
public class Document : ObservableObject, IDocument
{
    private string _content = string.Empty;

    public Document() { }
    public Document(string? filePath, string content)
    {
        Content = content;
        Metadata.FilePath = filePath;
        Metadata.Update(Content);
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

    /// <summary>
    /// Deletes a substring from the document.
    /// </summary>
    /// <param name="position">Starting index of the substring to be deleted.</param>
    /// <param name="length">Length of substring to be deleted.</param>
    public void Delete(int position, int length)
    {
        Content = Content.Remove(position, length);
    }

    /// <summary>
    /// Inserts a substring into the document.
    /// </summary>
    /// <param name="position">Index at which to insert the string.</param>
    /// <param name="text">String to insert.</param>
    public void Insert(int position, string text) 
    {
        Content = Content.Insert(position, text);
    }
}
