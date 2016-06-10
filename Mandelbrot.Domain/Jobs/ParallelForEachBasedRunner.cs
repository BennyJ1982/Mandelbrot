namespace Mandelbrot.Domain.Jobs
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Mandelbrot.Domain.Output;
	using Mandelbrot.Domain.Rendering;

	public class ParallelForEachBasedRunner : JobRunnerBase
	{
		public ParallelForEachBasedRunner(IRendererRegistry rendererRegistry)
			: base(rendererRegistry)
		{
		}

		protected override async Task<IEnumerable<IRenderResult>> ExecuteStageAsync(
			IJob job,
			IEnumerable<IRenderSpecification> stage,
			IDrawingContext context,
			int maxDegreeOfParallelism,
			CancellationToken cancellationToken)
		{
			var forEachTask = Task.Factory.StartNew(
				() =>
					{
						var renderResults = new ConcurrentBag<IRenderResult>();
						try
						{
							Parallel.ForEach(
								stage,
								new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism, CancellationToken = cancellationToken },
								(task, state) => renderResults.Add(this.Render(job, task, context, cancellationToken)));
						}
						catch (OperationCanceledException)
						{
							// do nothing
						}

						return renderResults.ToArray();
					},
				cancellationToken);

			return await forEachTask;
		}
	}
}