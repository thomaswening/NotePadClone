using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.ReversibleActions;
public class ReversibleActionsManager : IReversibleActionsManager
{
    private readonly Stack<IReversibleAction> _undoStack = new();
    private readonly Stack<IReversibleAction> _redoStack = new();

    public void ExecuteAction(IReversibleAction action)
    {
        action.Execute();
        _undoStack.Push(action);
        _redoStack.Clear();
    }

    public void UndoLastAction()
    {
        if (_undoStack.Count == 0)
        {
            return;
        }

        var action = _undoStack.Pop();
        action.Undo();
        _redoStack.Push(action);
    }

    public void RedoLastAction()
    {
        if (_redoStack.Count == 0)
        {
            return;
        }

        var action = _redoStack.Pop();
        action.Execute();
        _undoStack.Push(action);
    }
}
