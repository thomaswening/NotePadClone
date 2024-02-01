using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NotePadClone.DocumentModel;

namespace NotePadClone.ReversibleActions;
public class InsertTextAction(IDocument document, string text, int position) : IReversibleAction
{
    private readonly IDocument _document = document;
    private readonly string _text = text;
    private readonly int _position = position;

    public void Execute()
    {
        _document.Insert(_position, _text);
    }

    public void Undo()
    {
        _document.Delete(_position, _text.Length);
    }
}
