namespace Mandelbrot.Domain.Calculation
{
	using System.Linq;

	public struct FractalPath
	{
		public FractalPath(double x, double y, decimal value)
			: this(value, new Point<double>(x, y))
		{
		}

		public FractalPath(double x1, double y1, double x2, double y2, decimal value)
			: this(value, new Point<double>(x1, y1), new Point<double>(x2, y2))
		{
		}

		public FractalPath(decimal value, params Point<int>[] points)
			: this(value, points.Select(p => new Point<double>(p.X, p.Y)).ToArray())
		{
		}

		public FractalPath(decimal value, params Point<double>[] points)
		{
			this.Value = value;
			this.Points = points;
		}
	
		public Point<double>[] Points { get; private set; }

		public decimal Value { get; private set; }

		public bool IsPixel => this.Points.Length == 1;

		public bool IsLine => this.Points.Length == 2;
	}
}
