namespace Mandelbrot.Domain.Rendering.Output
{
	public static class ScreenExtension
	{
		public static Rectangle<double> ScaleFractalRectToScreenSelection(
			this IScreen screen,
			Rectangle<double> fractalRect,
			Rectangle<int> screenSelection)
		{
			var left = ScaleScreenValue(screenSelection.Left, screen.Width, fractalRect.Left, fractalRect.Right);
			var right = ScaleScreenValue(screenSelection.Right, screen.Width, fractalRect.Left, fractalRect.Right);
			var top = ScaleScreenValue(screenSelection.Top, screen.Height, fractalRect.Top, fractalRect.Bottom);
			var bottom = ScaleScreenValue(screenSelection.Bottom, screen.Height, fractalRect.Top, fractalRect.Bottom);

			return new Rectangle<double>(left, top, right, bottom);
		}

		private static double ScaleScreenValue(double screenValue, double screenSize, double fractalStart, double fractalEnd)
		{
			var screenPercentage = screenValue / screenSize;
			var scaleSize = fractalEnd - fractalStart;

			return screenPercentage * scaleSize + fractalStart;
		}
	}
}
