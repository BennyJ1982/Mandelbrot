namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public interface IPointBasedFractal : IFractalAlgorithm
	{
		double CalculateSinglePoint(int x, int y, IFractalSettings settings);
	}
}