namespace Mandelbrot.UI.Actions
{
	using System.Threading.Tasks;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Rendering.Output;

	public class ZoomInAction : IAction
	{
		private readonly IApplicationContext context;

		private readonly Rectangle<int> selection;

		private readonly IScreen screen;

		private Rectangle<decimal> oldFractalRect;

		public ZoomInAction(IApplicationContext context, Rectangle<int> selection, IScreen screen)
		{
			this.context = context;
			this.selection = selection;
			this.screen = screen;
		}

		public string Name => "Zoom in";

		public bool CanUndo => true;

		public async Task DoAsync()
		{
			this.oldFractalRect = this.context.CurrentFractalRect;
			await this.context.ZoomInAsync(this.screen, this.selection);
		}

		public async Task UndoAsync()
		{
			this.context.CurrentFractalRect = this.oldFractalRect;
			await this.context.DrawFractalAsync(this.screen);
		}
	}
}
