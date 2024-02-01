using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.ReversibleActions;

/// <summary>
/// Represents an action that can be historized for undo/redo.
/// </summary>
public interface IReversibleAction
{   
    void Execute();
    void Undo();
}
