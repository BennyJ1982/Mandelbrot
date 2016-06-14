namespace Mandelbrot.Domain.Jobs
{
	using System.Collections.Generic;
	using Mandelbrot.Domain.Rendering;

	public class JobResult : IJobResult
	{
		public JobResult(IJob job,IEnumerable<IRenderResult> renderResults, bool cancelled)
		{
			this.Job = job;
			this.RenderResults = renderResults;
			this.Cancelled = cancelled;
		}

		public IJob Job { get; }

		public IEnumerable<IRenderResult> RenderResults { get; }

		public bool Cancelled { get; }
	}
}