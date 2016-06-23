namespace Mandelbrot.UI.Actions
{
	using System;
	using System.Threading.Tasks;

	public interface IUndoStack
	{
		void PushIfPossibe(IAction action);

		Task UndoLatest();

		Task RedoLatest();

		bool CanUndo();

		bool CanRedo();

		event EventHandler<EventArgs> StackChanged;
	}
}
