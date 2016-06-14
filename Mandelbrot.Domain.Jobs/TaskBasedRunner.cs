namespace Mandelbrot.Domain.Jobs
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Mandelbrot.Domain.Rendering;
	using Mandelbrot.Domain.Rendering.Output;

	public class TaskBasedRunner : JobRunnerBase
	{
		public TaskBasedRunner(IRendererRegistry rendererRegistry)
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
			var enumerator = stage.GetEnumerator();
			var runningTasks = new List<Task<IRenderResult>>();
			var renderResults = new List<IRenderResult>();
			var cancel = false;

			while (true)
			{
				while (runningTasks.Count < maxDegreeOfParallelism)
				{
					if (!enumerator.MoveNext())
					{
						cancel = true;
						break;
					}

					var specification= enumerator.Current;
					var task = Task.Factory.StartNew(() => this.Render(job, specification, context, cancellationToken), cancellationToken);
					runningTasks.Add(task);
				}

				if (cancel)
				{
					break;
				}

				var firstFinishedTask = await Task.WhenAny(runningTasks);
				renderResults.Add(firstFinishedTask.Result);
				runningTasks.Remove(firstFinishedTask);
			}

			var results = await Task.WhenAll(runningTasks);
			renderResults.AddRange(results);

			return renderResults;
		}
	}
}