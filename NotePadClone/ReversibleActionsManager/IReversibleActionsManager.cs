using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.ReversibleActions;

/// <summary>
/// Historizes actions for and manages their undo/redo execution.
/// </summary>
public interface IReversibleActionsManager
{
    /// <summary>
    /// Executes an action and historizes it for undo/redo.
    /// </summary>
    /// <param name="action">Action to be historized.</param>
    void ExecuteAction(IReversibleAction action);

    /// <summary>
    /// Undoes the last action.
    /// </summary>
    void UndoLastAction();

    /// <summary>
    /// Redoes the last action.
    /// </summary>
    void RedoLastAction();
}
