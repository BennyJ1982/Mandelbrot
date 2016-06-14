namespace Mandelbrot.Domain.Calculation.Calculators
{
	using System.Collections.Generic;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Specifications;

	public interface IFractalCalculator
	{
		CalculatedFractalPart CalculatePart(ICalculationSpecification specification, IFractalSettings settings, CancellationToken cancellationToken);

		bool CanCalculatePart(ICalculationSpecification specification);

		IEnumerable<ICalculationSpecification> SplitIntoPreviewParts(ICalculationSpecification specification);
	}
}
