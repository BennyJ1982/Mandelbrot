namespace Mandelbrot.Domain.Calculation
{
	public class FractalLine : FractalPath
	{
		public FractalLine(double x1, double y1, double x2, double y2, decimal value)
			: base(value, new Point<double>(x1, y1), new Point<double>(x2, y2))
		{
		}

		public Point<double> Source => this.Points[0];

		public Point<double> Destination => this.Points[1];

		public double X1 => this.Source.X;

		public double Y1 => this.Source.Y;

		public double X2 => this.Destination.X;

		public double Y2 => this.Destination.Y;
	}
}
