namespace Mandelbrot.Domain.Jobs
{
	using System.Collections.Generic;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Rendering;

	public interface IJob
	{
		IFractalSettings Settings { get; }

		IEnumerable<IRenderSpecification>[] Stages { get; }
	}
}
