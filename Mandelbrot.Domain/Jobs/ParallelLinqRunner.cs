namespace Mandelbrot.Domain.Jobs
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Mandelbrot.Domain.Output;
	using Mandelbrot.Domain.Rendering;

	public class ParallelLinqRunner : JobRunnerBase
	{
		public ParallelLinqRunner(IRendererRegistry rendererRegistry)
			: base(rendererRegistry)
		{
		}

		protected override async Task<List<IRenderResult>> ExecuteJobAsync(
			IJob job,
			int maxDegreeOfParallelism,
			CancellationToken cancellationToken,
			IDrawingContext context)
		{
			var linqTask = Task.Factory.StartNew(
				() =>
					{
						var renderResults = new ConcurrentBag<IRenderResult>();

						try
						{
							job.Stages.SelectMany(stage => stage)
								.AsParallel()
								.WithCancellation(cancellationToken)
								.ForAllInApproximateOrder(spec => renderResults.Add(this.Render(job, spec, context, cancellationToken)));
						}
						catch (OperationCanceledException)
						{
							// do nothing
						}

						return renderResults.ToList();
					},
				cancellationToken);

			return await linqTask;
		}

		protected override async Task<IEnumerable<IRenderResult>> ExecuteStageAsync(
			IJob job,
			IEnumerable<IRenderSpecification> stage,
			IDrawingContext context,
			int maxDegreeOfParallelism,
			CancellationToken cancellationToken)
		{
			// we do't use this method
			return null;
		}
	}


}
