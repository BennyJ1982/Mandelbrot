namespace Mandelbrot.Domain.Calculation
{
	public interface ICalculationSpecification
	{
		bool HasScaledResult(Rectangle<int> destinationRectangle);
	}
}
