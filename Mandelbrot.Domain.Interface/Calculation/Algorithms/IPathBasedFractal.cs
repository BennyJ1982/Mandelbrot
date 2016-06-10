namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;
	using System.Threading;

	public interface IPathBasedFractal : IFractalAlgorithm
	{
		IEnumerable<FractalPath> CalculatePaths(FractalPath initialPath, IFractalSettings settings, CancellationToken cancellationToken);
	}
}