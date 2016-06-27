using System;
using System.Threading.Tasks;

namespace Mandelbrot.UI.Actions
{
	public class ActionDispatcher : IActionDispatcher
	{
		private readonly IUndoStack undoStack;

		public ActionDispatcher(IUndoStack undoStack)
		{
			this.undoStack = undoStack;
		}

		public async Task DispatchAsync(IAction action)
		{
			await action.DoAsync();
			this.undoStack.PushIfPossibe(action);
		}
	}
}
