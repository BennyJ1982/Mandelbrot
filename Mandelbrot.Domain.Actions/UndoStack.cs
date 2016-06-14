namespace Mandelbrot.Domain.Actions
{
	using System.Collections.Generic;

	public class UndoStack : IUndoStack
	{
		private readonly Stack<IAction> undoStack = new Stack<IAction>();
		private readonly Stack<IAction> redoStack = new Stack<IAction>();

		public void PushIfPossibe(IAction action)
		{
			if (action.CanUndo)
			{
				this.undoStack.Push(action);
				this.redoStack.Clear();
			}
		}

		public void UndoLatest()
		{
			var action = this.undoStack.Pop();
			action.Undo();
			this.redoStack.Push(action);
		}

		public void RedoLatest()
		{
			var action = this.redoStack.Pop();
			action.Do();
			this.undoStack.Push(action);
		}

		public bool CanUndo() => this.undoStack.Count > 0;

		public bool CanRedo() => this.redoStack.Count > 0;	
	}
}
