namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Calculation.Calculators;
	using Mandelbrot.Domain.Calculation.Specifications;

	public class PathBasedFractalCalculator : IFractalCalculator
	{
		public CalculatedFractalPart CalculatePart(
			ICalculationSpecification specification,
			IFractalSettings settings,
			CancellationToken cancellationToken)
		{
			var pathBasedSpec = ((PathBasedCalculationSpecification)specification);
			var algorithm = (IPathBasedFractal)settings.Algorithm;

			var lines = algorithm.CalculatePaths(pathBasedSpec.InitialPath, settings, cancellationToken);
			lines = lines.Union(pathBasedSpec.AdditionalOutput);

			return new CalculatedFractalPart(new Rectangle<int>(0, 0, settings.ScreenWidth - 1, settings.ScreenHeight - 1), lines);
		}

		public bool CanCalculatePart(ICalculationSpecification specification)
		{
			return specification is PathBasedCalculationSpecification;
		}

		public IEnumerable<ICalculationSpecification> SplitIntoPreviewParts(ICalculationSpecification specification)
		{
			return new[] { specification };
		}
	}
}