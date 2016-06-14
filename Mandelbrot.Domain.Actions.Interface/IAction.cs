namespace Mandelbrot.Domain.Actions
{
	public interface IAction
	{
		string Name { get; }

		bool CanUndo { get; }

		void Do();

		void Undo();
	}
}
