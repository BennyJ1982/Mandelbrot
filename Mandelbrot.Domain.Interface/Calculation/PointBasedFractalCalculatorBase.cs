namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Algorithms;

	public abstract class PointBasedFractalCalculatorBase : IFractalCalculator
	{
		public CalculatedFractalPart CalculatePart(
			ICalculationSpecification specification,
			IFractalSettings settings,
			CancellationToken cancellationToken)
		{
			var rectangleToCalculate = ((PointBasedCalculationSpecification)specification).RectangleToCalculate;
			var algorithm = (IPointBasedFractal)settings.Algorithm;
			var points = new Collection<FractalPath>();

			for (var x = rectangleToCalculate.Left; x <= rectangleToCalculate.Right; x++)
			{
				for (var y = rectangleToCalculate.Top; y <= rectangleToCalculate.Bottom; y++)
				{
					var value = algorithm.CalculateSinglePoint(x, y, settings);
					points.Add(new FractalPath(x - rectangleToCalculate.Left, y - rectangleToCalculate.Top, value));
				}

				if (cancellationToken.IsCancellationRequested)
				{
					break;
				}
			}

			return this.CreateResult(specification, rectangleToCalculate, points);
		}

		public abstract bool CanCalculatePart(ICalculationSpecification specification);

		public abstract IEnumerable<ICalculationSpecification> SplitIntoPreviewParts(ICalculationSpecification specification);

		protected virtual CalculatedFractalPart CreateResult(
			ICalculationSpecification specification,
			Rectangle<int> rectangleToCalculate,
			Collection<FractalPath> points)
		{
			return new CalculatedFractalPart(rectangleToCalculate, points, specification);
		}
	}
}