namespace Mandelbrot.Domain.Calculation
{
	using System.Threading;

	public interface IFractalCalculator
	{
		CalculatedFractalPart CalculatePart(ICalculationSpecification specification, IFractalSettings settings, CancellationToken cancellationToken);

		bool CanCalculatePart(ICalculationSpecification specification);
	}
}
