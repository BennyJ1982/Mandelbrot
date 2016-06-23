using System;
using System.Threading.Tasks;

namespace Mandelbrot.UI.Actions
{
	using Mandelbrot.Domain.Rendering.Output;

	public class DrawAction : IAction
	{
		private readonly IApplicationContext context;

		private readonly IScreen screen;

		public DrawAction(IApplicationContext context, IScreen screen)
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
