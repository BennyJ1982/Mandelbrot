namespace Mandelbrot.Domain
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Algorithms;

	public interface IFractalSettings : IFractalCalculationSettings
	{		
		IFractalAlgorithm Algorithm { get; }
	}
}
