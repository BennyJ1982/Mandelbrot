namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;

	public abstract class PointBasedAlgorithmBase : AlgorithmBase, IPointBasedFractal
	{
		public abstract decimal CalculateSinglePoint(int x, int y, IFractalSettings settings);

		public IEnumerable<ICalculationSpecification> GetCalculatableParts(IFractalSettings settings, int numberOfParts)
		{
			var screenWidth = settings.ScreenWidth;
			var sectorWidth = screenWidth % numberOfParts > 0 ? screenWidth / numberOfParts - 1 : screenWidth / numberOfParts;

			for (var x = 0; x < screenWidth; x += sectorWidth)
			{
				var right = x + sectorWidth > screenWidth - 1 ? screenWidth - 1 : x + sectorWidth - 1;
				var fractionBounds = new Rectangle<int>(x, 0, right, settings.ScreenHeight - 1);
				yield return new PointBasedCalculationSpecification(fractionBounds);
			}
		}
	}
}
