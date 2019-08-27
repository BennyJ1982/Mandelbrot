namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public class JuliaAlgorithm : PointBasedAlgorithmBase
	{
		private const double ConstantRealPart = -0.7d;

		private const double ConstantImaginaryPart = 0.27015d;

		private const double Cutoff = 4d; // circle with radius 2

		public override Rectangle<double> DefaultScale => new Rectangle<double>(-1.5d, -1, 1.5d, 1);

		public override double CalculateSinglePoint(int x, int y, IFractalSettings settings)
		{
			var scaledX = this.ScaleX(x, settings);
			var scaledY = this.ScaleY(y, settings);

			var iteration = RunJuliaLoop(scaledX, scaledY, settings.MaxIterations);
			return iteration;
		}

		protected override string Name => "Julia Set";

		private static int RunJuliaLoop(double scaledX, double scaledY, int maxIterations)
		{
			var newRealPart = scaledX;
			var newImaginaryPart = scaledY;

			int iteration;
			for (iteration = 0; iteration < maxIterations - 1; iteration++)
			{
				// remember value of previous iteration
				var oldRealPart = newRealPart;
				var oldImaginaryPart = newImaginaryPart;

				// for the actual iteration, the real and imaginary part are calculated
				newRealPart = oldRealPart * oldRealPart - oldImaginaryPart * oldImaginaryPart + ConstantRealPart;
				newImaginaryPart = 2 * oldRealPart * oldImaginaryPart + ConstantImaginaryPart;

				// if the point is outside the cutoff circle: stop
				if ((newRealPart * newRealPart + newImaginaryPart * newImaginaryPart) > Cutoff)
				{
					break;
				}
			}

			return iteration;
		}
	}
}
