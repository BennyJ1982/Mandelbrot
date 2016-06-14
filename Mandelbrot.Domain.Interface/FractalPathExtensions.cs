namespace Mandelbrot.Domain
{
	public static class FractalPathExtensions
	{
		public static Point<double> GetFirstPoint(this FractalPath path)
		{
			return path.Points[0];
		}

		public static Point<double> GetSecondPoint(this FractalPath path)
		{
			return path.Points[1];
		}
	}
}
