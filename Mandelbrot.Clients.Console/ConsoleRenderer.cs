namespace Mandelbrot.Clients.Console
{
	using System.Linq;
	using System.Threading.Tasks;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Facade;
	using Mandelbrot.Domain.Rendering.Output;
	using Mandelbrot.Domain.Rendering.Shaders;

	public class ConsoleRenderer
	{
		private readonly IFractalContext context;

		public ConsoleRenderer(
			IAlgorithmRegistry algorithmRegistry,
			IShaderRegistry shaderRegistry,
			IFractalContextFactory contextFactory,
			IScreen screen)
		{
			this.context = contextFactory.Create(screen);

            // TODO: make configurable
            this.context.CurrentAlgorithm = algorithmRegistry.GetAll().First(r => r.ToString().StartsWith("Julia"));
			this.context.CurrentShader = shaderRegistry.GetAll().First();
			this.context.MaxIterations = 1500;
		}
		
		public void Cancel()
		{
			this.context.Cancel();
		}

		public async Task Render()
		{
			await this.context.DrawFractalAsync();
			System.Console.SetCursorPosition(0, 0);
		}

		public async Task ZoomIn(int centerX, int centerY, int width, int height)
		{
			var screenSelection = new Rectangle<int>(centerX - width / 2, centerY - height / 2, centerX + width / 2 - 1, centerY + height / 2 - 1);
			await this.context.ZoomInAsync(screenSelection);
			System.Console.SetCursorPosition(0, 0);
		}
	}
}
