namespace Mandelbrot.Domain.Output
{
	public static class ScreenExtension
	{
		public static Rectangle<decimal> ScaleFractalRectToScreenSelection(
			this IScreen screen,
			Rectangle<decimal> fractalRect,
			Rectangle<int> screenSelection)
		{
			var left = ScaleScreenValue(screenSelection.Left, screen.Width, fractalRect.Left, fractalRect.Right);
			var right = ScaleScreenValue(screenSelection.Right, screen.Width, fractalRect.Left, fractalRect.Right);
			var top = ScaleScreenValue(screenSelection.Top, screen.Height, fractalRect.Top, fractalRect.Bottom);
			var bottom = ScaleScreenValue(screenSelection.Bottom, screen.Height, fractalRect.Top, fractalRect.Bottom);

			return new Rectangle<decimal>(left, top, right, bottom);
		}

		private static decimal ScaleScreenValue(decimal screenValue, decimal screenSize, decimal fractalStart, decimal fractalEnd)
		{
			var screenPercentage = screenValue / screenSize;
			var scaleSize = fractalEnd - fractalStart;

			return screenPercentage * scaleSize + fractalStart;
		}
	}
}
