namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public class MandelbrotAlgorithm : PointBasedAlgorithmBase
	{
		private const int CutOff = (1 << 16);

		public override Rectangle<double> DefaultScale => new Rectangle<double>(-2.5d, -1, 1, 1);

		public override double CalculateSinglePoint(int x, int y, IFractalSettings settings)
		{
			var scaledX = this.ScaleX(x, settings);
			var scaledY = this.ScaleY(y, settings);

			var iteration = RunMandelbrotLoop(scaledX, scaledY, settings.MaxIterations);
			return iteration;
		}

		protected override string Name => "Mandelbrot Set";

		private static int RunMandelbrotLoop(double scaledX, double scaledY, int maxIterations)
		{
            double currentX = 0;
            double currentY = 0;

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
