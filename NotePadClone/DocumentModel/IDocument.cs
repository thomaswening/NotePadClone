using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.DocumentModel;
public interface IDocument
{
    string Content { get; set; }
    DocumentMetadata Metadata { get; }
}
