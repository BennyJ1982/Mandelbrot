namespace Mandelbrot.Domain.Rendering
{
	using System.Drawing;

	public static class PointExtension
	{
		public static Point ToDrawingPoint(this Point<double> point)
		{
			return new Point((int)point.X, (int)point.Y);
		}
	}
}
