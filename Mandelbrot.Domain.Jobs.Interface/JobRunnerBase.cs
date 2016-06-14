namespace Mandelbrot.Domain.Jobs
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Mandelbrot.Domain.Rendering;
	using Mandelbrot.Domain.Rendering.Output;

	public abstract class JobRunnerBase : IJobRunner
	{
		private readonly IRendererRegistry rendererRegistry;

		protected JobRunnerBase(IRendererRegistry rendererRegistry)
		{
			this.rendererRegistry = rendererRegistry;
		}

		public async Task<IJobResult> ExecuteAsync(IJob job, IScreen screen, int maxDegreeOfParallelism, CancellationToken cancellationToken)
		{
			using (var context = screen.GetDrawingContext())
			{
				var results = await this.ExecuteJobAsync(job, maxDegreeOfParallelism, cancellationToken, context);
				return new JobResult(job, results, cancellationToken.IsCancellationRequested);
			}
		}

		protected virtual async Task<List<IRenderResult>> ExecuteJobAsync(
			IJob job,
			int maxDegreeOfParallelism,
			CancellationToken cancellationToken,
			IDrawingContext context)
		{
			var results = new List<IRenderResult>();

			foreach (var stage in job.Stages)
			{
				var stageResults = await this.ExecuteStageAsync(job, stage, context, maxDegreeOfParallelism, cancellationToken);
				results.AddRange(stageResults);
			}

			return results;
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
