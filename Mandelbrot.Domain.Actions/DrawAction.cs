using System;
using System.Threading.Tasks;

namespace Mandelbrot.UI.Actions
{
	using Mandelbrot.Domain.Facade;

	public class DrawAction : IAction
	{
		private readonly IFractalContext context;

		public DrawAction(IFractalContext context)
		{
			this.context = context;
		}

		public string Name => "Draw";

		public bool CanUndo => false;

		public async Task DoAsync()
		{
			await this.context.DrawFractalAsync();
		}

		public Task UndoAsync()
		{
			throw new InvalidOperationException();
		}
	}
}
