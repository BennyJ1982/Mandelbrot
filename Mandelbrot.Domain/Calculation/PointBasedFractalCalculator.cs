namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.ObjectModel;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Algorithms;

	public class PointBasedFractalCalculator : IFractalCalculator
	{
		public CalculatedFractalPart CalculatePart(
			ICalculationSpecification specification,
			IFractalSettings settings,
			CancellationToken cancellationToken)
		{
			var rectangleToCalculate = ((PointBasedCalculationSpecification)specification).RectangleToCalculate;
			var algorithm = (IPointBasedFractal)settings.Algorithm;
			var points = new Collection<FractalPoint>();

			for (var x = rectangleToCalculate.Left; x <= rectangleToCalculate.Right; x++)
			{
				for (var y = rectangleToCalculate.Top; y <= rectangleToCalculate.Bottom; y++)
				{
					var value = algorithm.CalculateSinglePoint(x, y, settings);
					points.Add(new FractalPoint(x - rectangleToCalculate.Left, y - rectangleToCalculate.Top, value));
				}

				if (cancellationToken.IsCancellationRequested)
				{
					break;
				}
			}

			return new CalculatedFractalPart(rectangleToCalculate, points, specification);
		}

		public bool CanCalculatePart(ICalculationSpecification specification)
		{
			return specification is PointBasedCalculationSpecification;
		}
	}
}