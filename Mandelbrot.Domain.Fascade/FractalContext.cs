namespace Mandelbrot.Domain.Facade
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Jobs;
	using Mandelbrot.Domain.Rendering.Output;
	using Mandelbrot.Domain.Rendering.Shaders;

	public class FractalContext : IFractalContext
	{
		private readonly int maxDegreeOfParalellelism;

		private readonly IJobFactory jobFactory;

		private readonly IJobRunner jobRunner;

		private readonly IScreen screen;

		private CancellationTokenSource cancellationTokenSource;

		public FractalContext(IJobFactory jobFactory, IJobRunner jobRunner, IScreen screen, int maxDegreeOfParalellelism)
		{
			this.jobFactory = jobFactory;
			this.jobRunner = jobRunner;
			this.screen = screen;
			this.maxDegreeOfParalellelism = maxDegreeOfParalellelism;

			this.MaxIterations = 100;
		}

		public Rectangle<double> CurrentFractalRect { get; set; }

		public IJobResult LastJobResult { get; private set; }

		public IFractalAlgorithm CurrentAlgorithm { get; set; }

		public IShader CurrentShader { get; set; }

		public int MaxIterations { get; set; }

		public event EventHandler<StatusEventArgs> StatusChanged;

		public async Task DrawFractalAsync()
		{
			if (this.CurrentAlgorithm == null)
			{
				throw new InvalidOperationException("No current algorithm has been set.");
			}

			if (this.CurrentShader == null)
			{
				throw new InvalidOperationException("No current shader has been set.");
			}

			if (Equals(this.CurrentFractalRect, default(Rectangle<decimal>)))
			{
				this.ResetFractalRect();
			}
			this.cancellationTokenSource = new CancellationTokenSource();
			this.StatusChanged?.Invoke(this, new StatusEventArgs(true));

			try
			{
				var job = this.CreateJob(this.GetCurrentFractalSettings(), this.CurrentShader);
				this.LastJobResult =
					await this.jobRunner.ExecuteAsync(job, this.screen, this.maxDegreeOfParalellelism, this.cancellationTokenSource.Token);
			}
			catch (OperationCanceledException)
			{
				// do nothing
			}
			finally
			{
				this.cancellationTokenSource = null;
				this.StatusChanged?.Invoke(this, new StatusEventArgs(false));
			}
		}

		public async Task ZoomInAsync(Rectangle<int> screenSelection)
		{
			if (this.CurrentAlgorithm == null)
			{
				throw new InvalidOperationException("No current algorithm has been set.");
			}

			if (this.CurrentShader == null)
			{
				throw new InvalidOperationException("No current shader has been set.");
			}

			if (Equals(this.CurrentFractalRect, default(Rectangle<decimal>)))
			{
				throw new InvalidOperationException("Cannot zoom in before initial fractal has been drawn.");
			}

			this.CurrentFractalRect = this.screen.ScaleFractalRectToScreenSelection(this.CurrentFractalRect, screenSelection);
			await this.DrawFractalAsync();
		}

		public void Cancel()
		{
			this.cancellationTokenSource?.Cancel();
		}

		public void ResetFractalRect()
		{
			if (this.CurrentAlgorithm == null)
			{
				throw new InvalidOperationException("No current algorithm has been set.");
			}

			this.CurrentFractalRect = this.CurrentAlgorithm.DefaultScale;
		}

		private IJob CreateJob(IFractalSettings settings, IShader shader)
		{
			if (this.LastJobResult != null && !this.LastJobResult.Cancelled && this.LastJobResult.Job.Settings.Equals(settings))
			{
				return this.jobFactory.CreateFromResult(this.LastJobResult, settings, shader, this.GetNumberOfSectors());
			}

			return this.jobFactory.Create(settings, shader, this.GetNumberOfSectors());
		}

		private FractalSettings GetCurrentFractalSettings()
		{
			return new FractalSettings(this.CurrentAlgorithm, this.screen.Width, this.screen.Height, this.CurrentFractalRect, this.MaxIterations);
		}

		private int GetNumberOfSectors()
		{
			return this.maxDegreeOfParalellelism;
		}
	}
}