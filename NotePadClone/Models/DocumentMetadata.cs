using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.Models;
public class DocumentMetadata
{
    public string? FilePath { get; set; }
    public int NumberOfCharacters { get; set; }
    public int NumberOfLines { get; set; }
    public int FileSizeInBytes { get; set; }
}
