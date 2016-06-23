namespace Mandelbrot.UI.Actions
{
	using System.Threading.Tasks;

	public interface IAction
	{
		string Name { get; }

		bool CanUndo { get; }

		Task DoAsync();

		Task UndoAsync();
	}
}
