namespace Mandelbrot.Clients.Console
{
	using System.Linq;
	using System.Threading.Tasks;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Rendering.Shaders;
	using Mandelbrot.UI;

	public class ConsoleRenderer
	{
		private readonly ConsoleScreen screen;

		private readonly IApplicationContext context;

		public ConsoleRenderer(
			IAlgorithmRegistry algorithmRegistry,
			IShaderRegistry shaderRegistry,
			IApplicationContextFactory contextFactory,
			ConsoleScreen screen)
		{
			this.screen = screen;
			this.context = contextFactory.Create();

			// TODO: make configurable
			this.context.CurrentAlgorithm = algorithmRegistry.GetAll().Reverse().Skip(1).First();
			this.context.CurrentShader = shaderRegistry.GetAll().First();
			this.context.MaxIterations = 8;
		}

		public void Cancel()
		{
			this.context.Cancel();
		}

		public async Task Render()
		{
			await this.context.DrawFractalAsync(this.screen);
			System.Console.SetCursorPosition(0, 0);
		}

		public async Task ZoomIn(int centerX, int centerY, int width, int height)
		{
			var screenSelection = new Rectangle<int>(centerX - width / 2, centerY - height / 2, centerX + width / 2 - 1, centerY + height / 2 - 1);
			await this.context.ZoomInAsync(this.screen, screenSelection);
			System.Console.SetCursorPosition(0, 0);
		}
	}
}
