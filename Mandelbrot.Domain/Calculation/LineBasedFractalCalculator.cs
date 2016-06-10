namespace Mandelbrot.Domain.Calculation
{
	using System.Linq;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Algorithms;

	public class LineBasedFractalCalculator : IFractalCalculator
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

			return new CalculatedFractalPart(new Rectangle<int>(0, 0, settings.ScreenWidth-1, settings.ScreenHeight-1), lines, specification);
		}

		public bool CanCalculatePart(ICalculationSpecification specification)
		{
			return specification is PathBasedCalculationSpecification;
		}
	}
}