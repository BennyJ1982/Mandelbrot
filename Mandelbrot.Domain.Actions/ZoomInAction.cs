namespace Mandelbrot.UI.Actions
{
	using System.Threading.Tasks;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Facade;

	public class ZoomInAction : IAction
	{
		private readonly IFractalContext context;

		private readonly Rectangle<int> selection;

		private Rectangle<double> oldFractalRect;

		public ZoomInAction(IFractalContext context, Rectangle<int> selection)
		{
			this.context = context;
			this.selection = selection;
		}

		public string Name => "Zoom in";

		public bool CanUndo => true;

		public async Task DoAsync()
		{
			this.oldFractalRect = this.context.CurrentFractalRect;
			await this.context.ZoomInAsync(this.selection);
		}

		public async Task UndoAsync()
		{
			this.context.CurrentFractalRect = this.oldFractalRect;
			await this.context.DrawFractalAsync();
		}
	}
}
