namespace Mandelbrot.UI.Actions
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

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
				this.StackChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public async Task UndoLatest()
		{
			var action = this.undoStack.Pop();
			await action.UndoAsync();
			this.redoStack.Push(action);
			this.StackChanged?.Invoke(this, EventArgs.Empty);
		}

		public async Task RedoLatest()
		{
			var action = this.redoStack.Pop();
			await action.DoAsync();
			this.undoStack.Push(action);
			this.StackChanged?.Invoke(this, EventArgs.Empty);
		}

		public bool CanUndo() => this.undoStack.Count > 0;

		public bool CanRedo() => this.redoStack.Count > 0;

		public event EventHandler<EventArgs> StackChanged;
	}
}
