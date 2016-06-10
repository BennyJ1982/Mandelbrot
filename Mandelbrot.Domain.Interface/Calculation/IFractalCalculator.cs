namespace Mandelbrot.Domain.Calculation
{
	using System.Threading;

	public interface IFractalCalculator
	{
		CalculatedFractalPart CalculatePart(ICalculationSpecification specification, IFractalSettings settings, CancellationToken cancellationToken);

		CalculatedFractalPart CalculatePart(
			ICalculationSpecification specification,
			IFractalSettings settings,
			CancellationToken cancellationToken,
			PreviewDelegate previewDelegate);

		bool CanCalculatePart(ICalculationSpecification specification);
	}
}
