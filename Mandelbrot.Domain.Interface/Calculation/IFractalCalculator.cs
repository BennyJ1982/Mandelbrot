namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using System.Threading;

	public interface IFractalCalculator
	{
		CalculatedFractalPart CalculatePart(ICalculationSpecification specification, IFractalSettings settings, CancellationToken cancellationToken);

		bool CanCalculatePart(ICalculationSpecification specification);

		IEnumerable<ICalculationSpecification> SplitIntoPreviewParts(ICalculationSpecification specification);
	}
}
