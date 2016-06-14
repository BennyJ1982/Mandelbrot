namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;

	public abstract class PointBasedAlgorithmBase : AlgorithmBase, IPointBasedFractal
	{
		public abstract double CalculateSinglePoint(int x, int y, IFractalSettings settings);

		public IEnumerable<ICalculationSpecification> GetCalculatableParts(IFractalSettings settings, int numberOfParts)
		{
			var sizePerSector = settings.ScreenWidth / numberOfParts;
			for (var sector = 0; sector < numberOfParts; sector++)
			{
				var left = sizePerSector * sector;
				var right = sector == numberOfParts - 1 ? settings.ScreenWidth - 1 : left + sizePerSector - 1;

				var fractionBounds = new Rectangle<int>(left, 0, right, settings.ScreenHeight - 1);
				yield return new PointBasedCalculationSpecification(fractionBounds, 0);
			}
		}
	}
}
