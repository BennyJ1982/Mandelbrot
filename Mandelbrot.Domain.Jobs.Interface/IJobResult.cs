namespace Mandelbrot.Domain.Jobs
{
	using System.Collections.Generic;
	using Mandelbrot.Domain.Rendering;

	public interface IJobResult
	{
		IJob Job { get; }

		IEnumerable<IRenderResult> RenderResults{ get; }

		bool Cancelled { get; }
	}
}
