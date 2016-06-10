namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public interface IPointBasedFractal : IFractalAlgorithm
	{
		decimal CalculateSinglePoint(int x, int y, IFractalSettings settings);
	}
}