namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System;

	public class Geometry
	{
		public static Point<double> GetPointFromAngle(Point<double> sourcePoint, double radius, double degrees)
		{
			var radians = degrees * Math.PI / 180;
			var xPos = Math.Cos(radians) * radius + sourcePoint.X;
			var yPos = Math.Sin(radians) * radius + sourcePoint.Y;

			return new Point<double>(xPos, yPos);
		}

		public static Point<double> GetLineCenterPoint(Point<double> a, Point<double> b)
		{
			return new Point<double>((a.X - b.X) / 2 + b.X, (a.Y - b.Y) / 2 + b.Y);
		}

		public static double GetLineAngle(Point<double> source, Point<double> destination)
		{
			var xDiff = source.X - destination.X;
			var yDiff = source.Y - destination.Y;

			var angle = Math.Atan2(yDiff , xDiff);
			angle *= 180 / Math.PI;

			if (angle < 0)
			{
				return angle + 360;
			}

			return angle;
		}

		public static double GetLineLength(Point<double> source, Point<double> destination)
		{
			return Math.Sqrt((destination.X - source.X) * (destination.X - source.X) + (destination.Y - source.Y) * (destination.Y - source.Y));
		}
	}
}
