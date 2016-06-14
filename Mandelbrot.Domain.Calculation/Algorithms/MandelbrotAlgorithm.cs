namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public class MandelbrotAlgorithm : PointBasedAlgorithmBase
	{
		private const int CutOff = (1 << 16);

		public override Rectangle<decimal> DefaultScale => new Rectangle<decimal>(-2.5m, -1, 1, 1);

		public override double CalculateSinglePoint(int x, int y, IFractalSettings settings)
		{
			var scaledX = this.ScaleX(x, settings);
			var scaledY = this.ScaleY(y, settings);

			var iteration = RunMandelbrotLoop(scaledX, scaledY, settings.MaxIterations);
			return iteration;
		}

		protected override string Name => "Mandelbrot Set";

		private static int RunMandelbrotLoop(decimal scaledX, decimal scaledY, int maxIterations)
		{
			decimal currentX = 0;
			decimal currentY = 0;

			int iteration;
			for (iteration = 0; iteration < maxIterations - 1; iteration++)
			{
				var currentXSquare = currentX * currentX;
				var currentYSquare = currentY * currentY;

				if (currentXSquare + currentYSquare >= CutOff)
				{
					break;
				}

				currentY = currentX * currentY;
				currentY += currentY + scaledY;
				currentX = currentXSquare - currentYSquare + scaledX;
			}

			return iteration;
		}
	}
}
