using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.DocumentModel;

/// <summary>
/// Represents a document.
/// </summary>
public interface IDocument
{
    string Content { get; set; }
    DocumentMetadata Metadata { get; }
    void Insert(int position, string text);
    void Delete(int position, int length);
}
