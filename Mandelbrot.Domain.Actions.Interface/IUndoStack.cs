namespace Mandelbrot.Domain.Actions
{
	public interface IUndoStack
	{
		void PushIfPossibe(IAction action);

		void UndoLatest();

		void RedoLatest();

		bool CanUndo();

		bool CanRedo();
	}
}
