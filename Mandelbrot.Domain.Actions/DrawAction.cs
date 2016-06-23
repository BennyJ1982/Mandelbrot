using System;
using System.Threading.Tasks;

namespace Mandelbrot.UI.Actions
{
	using Mandelbrot.Domain.Fascade;
	using Mandelbrot.Domain.Rendering.Output;

	public class DrawAction : IAction
	{
		private readonly IFractalContext context;

		private readonly IScreen screen;

		public DrawAction(IFractalContext context, IScreen screen)
		{
			this.context = context;
			this.screen = screen;
		}

		public string Name => "Draw";

		public bool CanUndo => false;

		public async Task DoAsync()
		{
			await this.context.DrawFractalAsync(this.screen);
		}

		public Task UndoAsync()
		{
			throw new InvalidOperationException();
		}
	}
}
