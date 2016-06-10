namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public static class DecimalExtension
	{
		public static decimal ScaleXToFractal(this decimal screenX, IFractalSettings settings)
		{
			return screenX.ScaleXToFractal(settings.FractalRect, settings.ScreenWidth);
		}

		public static decimal ScaleXToFractal(this decimal screenX, Rectangle<decimal> fractalRect, int screenWidth)
		{
			var fractalWidth = fractalRect.Right - fractalRect.Left;
			return fractalWidth / screenWidth * screenX + fractalRect.Left;
		}

		public static decimal ScaleYToFractal(this decimal screenY, IFractalSettings settings)
		{
			return screenY.ScaleYToFractal(settings.FractalRect, settings.ScreenHeight);
		}

		public static decimal ScaleYToFractal(this decimal screenY, Rectangle<decimal> fractalRect, int screenHeight)
		{
			var fractalHeight = fractalRect.Bottom- fractalRect.Top;
			return fractalHeight / screenHeight * screenY + fractalRect.Top;
		}

	}
}
