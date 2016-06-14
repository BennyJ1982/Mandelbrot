namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public class JuliaAlgorithm : PointBasedAlgorithmBase
	{
		private const decimal ConstantRealPart = -0.7m;

		private const decimal ConstantImaginaryPart = 0.27015m;

		private const decimal Cutoff = 4m; // circle with radius 2

		public override Rectangle<decimal> DefaultScale => new Rectangle<decimal>(-1.5m, -1, 1.5m, 1);

		public override double CalculateSinglePoint(int x, int y, IFractalSettings settings)
		{
			var scaledX = this.ScaleX(x, settings);
			var scaledY = this.ScaleY(y, settings);

			var iteration = RunJuliaLoop(scaledX, scaledY, settings.MaxIterations);
			return iteration;
		}

		protected override string Name => "Julia Set";

		private static int RunJuliaLoop(decimal scaledX, decimal scaledY, int maxIterations)
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
