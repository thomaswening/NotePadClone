using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.Models;
public interface IDocument
{
    string Content { get; set; }
    string? FilePath { get; set; }
    int NumberOfCharacters { get; }
    int NumberOfLines { get; }
    int FileSizeInBytes { get; }
}
