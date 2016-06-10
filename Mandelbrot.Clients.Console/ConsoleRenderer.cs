namespace Mandelbrot.Clients.Console
{
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Calculation.Shaders;
	using Mandelbrot.Domain.Jobs;
	using Mandelbrot.Domain.Output;

	public class ConsoleRenderer
	{
		private const int Iterations = 8;

		private readonly IFractalAlgorithm algorithm;

		private readonly IShader shader;

		private readonly IJobFactory jobFactory;

		private readonly IJobRunner jobRunner;

		private readonly ConsoleScreen screen;

		private CancellationTokenSource cancellationTokenSource;

		private Rectangle<decimal> currentFractalRect;

		public ConsoleRenderer(
			IAlgorithmRegistry algorithmRegistry,
			IShaderRegistry shaderRegistry,
			IJobFactory jobFactory,
			IJobRunner jobRunner,
			ConsoleScreen screen)
		{
			this.algorithm = algorithmRegistry.GetAll().Reverse().Skip(1).First();
			this.shader = shaderRegistry.GetAll().First();
			this.jobFactory = jobFactory;
			this.jobRunner = jobRunner;
			this.screen = screen;
		}


		public bool IsDrawing => this.cancellationTokenSource != null;

		public void Cancel()
		{
			var source = this.cancellationTokenSource;
			source?.Cancel();
		}

		public async Task Render()
		{
			this.currentFractalRect = this.algorithm.DefaultScale;
			await this.RenderUsingCurrentFractalRect();
		}

		public async Task ZoomIn(int centerX, int centerY, int width, int height)
		{
			var screenSelection = new Rectangle<int>(centerX - width / 2, centerY - height / 2, centerX + width / 2 - 1, centerY + height / 2 - 1);

			this.currentFractalRect = this.screen.ScaleFractalRectToScreenSelection(this.currentFractalRect, screenSelection);
			await this.RenderUsingCurrentFractalRect();
		}

		public async Task RenderUsingCurrentFractalRect()
		{
			var job = this.jobFactory.Create(this.GetCurrentFractalSettings(), this.shader, 10);
			try
			{

				this.cancellationTokenSource = new CancellationTokenSource();
				await this.jobRunner.ExecuteAsync(job, this.screen, 8, this.cancellationTokenSource.Token);
				System.Console.SetCursorPosition(0, 0);
			}
			finally
			{
				this.cancellationTokenSource = null;
			}
		}

		private FractalSettings GetCurrentFractalSettings()
		{
			return new FractalSettings(this.algorithm, this.screen.Width, this.screen.Height, this.currentFractalRect, Iterations);
		}
	}
}
