using System.Threading.Tasks;

namespace Mandelbrot.UI.Actions
{
	public interface IActionDispatcher
	{
		Task DispatchAsync(IAction action);
	}

}
