namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public static class DoubleExtension
	{
		public static double ScaleXToFractal(this double screenX, IFractalSettings settings)
		{
			return screenX.ScaleXToFractal(settings.FractalRect, settings.ScreenWidth);
		}

		public static double ScaleXToFractal(this double screenX, Rectangle<double> fractalRect, int screenWidth)
		{
			var fractalWidth = fractalRect.Right - fractalRect.Left;
			return fractalWidth / screenWidth * screenX + fractalRect.Left;
		}

		public static double ScaleYToFractal(this double screenY, IFractalSettings settings)
		{
			return screenY.ScaleYToFractal(settings.FractalRect, settings.ScreenHeight);
		}

		public static double ScaleYToFractal(this double screenY, Rectangle<double> fractalRect, int screenHeight)
		{
			var fractalHeight = fractalRect.Bottom - fractalRect.Top;
			return fractalHeight / screenHeight * screenY + fractalRect.Top;
		}

	}
}
