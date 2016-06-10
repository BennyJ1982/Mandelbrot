namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Algorithms;

	public class PointBasedFractalCalculator : IFractalCalculator
	{
		public CalculatedFractalPart CalculatePart(
			ICalculationSpecification specification,
			IFractalSettings settings,
			CancellationToken cancellationToken)
		{
			return this.CalculatePart(specification, settings, cancellationToken, null);
		}

		public CalculatedFractalPart CalculatePart(
			ICalculationSpecification specification,
			IFractalSettings settings,
			CancellationToken cancellationToken,
			PreviewDelegate previewDelegate)
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

				ProvidePreview(previewDelegate, x, rectangleToCalculate, points, specification);
			}

			return new CalculatedFractalPart(rectangleToCalculate, points, specification);
		}

		public bool CanCalculatePart(ICalculationSpecification specification)
		{
			return specification is PointBasedCalculationSpecification;
		}

		private static void ProvidePreview(
			PreviewDelegate previewDelegate,
			int currentColumn,
			Rectangle<int> rect,
			IEnumerable<FractalPath> points,
			ICalculationSpecification specification)
		{
			if (previewDelegate == null)
			{
				return;
			}

			if (currentColumn == rect.Left)
			{
				var previewPoints =
					points.Where(p => (int)p.GetFirstPoint().X == currentColumn - rect.Left)
					.Select(p => new FractalPath(0, p.GetFirstPoint().Y, p.Value));

				var screenPosition = new Rectangle<int>(currentColumn, rect.Top, currentColumn, rect.Bottom);
				previewDelegate(new ScalableFractalPart(screenPosition, previewPoints, specification, rect));
			}
			else if (currentColumn == (rect.Right - rect.Left + 1) / 2 + rect.Left)
			{
				// preview first 50%
				var previewPoints =
					points.Where(p => (int)p.GetFirstPoint().X <= currentColumn - rect.Left)
						.Select(p => new FractalPath(p.GetFirstPoint().X, p.GetFirstPoint().Y, p.Value));

				var screenPosition = new Rectangle<int>(rect.Left, rect.Top, currentColumn, rect.Bottom);
				previewDelegate(new ScalableFractalPart(screenPosition, previewPoints, specification, screenPosition));

				// extrapolate remaining
				previewPoints =
					points.Where(p => (int)p.GetFirstPoint().X == currentColumn - rect.Left)
						.Select(p => new FractalPath(0, p.GetFirstPoint().Y, p.Value));

				screenPosition = new Rectangle<int>(currentColumn + 1, rect.Top, currentColumn + 1, rect.Bottom);
				var scaledPosition = new Rectangle<int>(screenPosition.Left, screenPosition.Top, rect.Right, rect.Bottom);
				previewDelegate(new ScalableFractalPart(screenPosition, previewPoints, specification, scaledPosition));
			}
		}

		//private IEnumerable<ColumnInfo> GetColumnDistributions(int left, int right)
		//{
		//	// TODO implement
		//}

		private class ColumnInfo
		{
			public ColumnInfo(int x)
			{
				this.X = x;
			}

			public int X { get; private set; }
		}
	}
}