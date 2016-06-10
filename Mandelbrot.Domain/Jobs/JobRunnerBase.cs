namespace Mandelbrot.Domain.Jobs
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Mandelbrot.Domain.Output;
	using Mandelbrot.Domain.Rendering;

	public abstract class JobRunnerBase : IJobRunner
	{
		private readonly IRendererRegistry rendererRegistry;

		protected JobRunnerBase(IRendererRegistry rendererRegistry)
		{
			this.rendererRegistry = rendererRegistry;
		}

		public async Task<IJobResult> ExecuteAsync(IJob job, IScreen screen, int maxDegreeOfParallelism, CancellationToken cancellationToken)
		{
			var results = new List<IRenderResult>();

			using (var context = screen.GetDrawingContext())
			{
				foreach (var stage in job.Stages)
				{
					var stageResults = await this.ExecuteStageAsync(job, stage, context, maxDegreeOfParallelism, cancellationToken);
					results.AddRange(stageResults);
				}
			}

			return new JobResult(job, results, cancellationToken.IsCancellationRequested);
		}

		protected abstract Task<IEnumerable<IRenderResult>> ExecuteStageAsync(
			IJob job,
			IEnumerable<IRenderSpecification> stage,
			IDrawingContext context,
			int maxDegreeOfParallelism,
			CancellationToken cancellationToken);


		protected IRenderResult Render(IJob job, IRenderSpecification specification, IDrawingContext context, CancellationToken cancellationToken)
		{
			IRenderer renderer;
			if (this.rendererRegistry.TryGetMatchingRenderer(specification, out renderer))
			{
				return renderer.Render(specification, context, cancellationToken);
			}

			throw new InvalidOperationException("No renderer found for type " + specification.GetType());
		}
	}
}
