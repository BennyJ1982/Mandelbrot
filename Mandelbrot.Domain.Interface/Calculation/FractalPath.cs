namespace Mandelbrot.Domain.Calculation
{
	using System.Linq;

	public struct FractalPath
	{
		public FractalPath(double x, double y, double value)
			: this(value, new Point<double>(x, y))
		{
		}

		public FractalPath(double x1, double y1, double x2, double y2, double value)
			: this(value, new Point<double>(x1, y1), new Point<double>(x2, y2))
		{
		}

		public FractalPath(double value, params Point<int>[] points)
			: this(value, points.Select(p => new Point<double>(p.X, p.Y)).ToArray())
		{
		}

		public FractalPath(double value, params Point<double>[] points)
		{
			this.Value = value;
			this.Points = points;
		}
	
		public Point<double>[] Points { get; private set; }

		public double Value { get; private set; }

		public bool IsPixel => this.Points.Length == 1;

		public bool IsLine => this.Points.Length == 2;

		public FractalPath Adjust(double offsetX, double offsetY)
		{
			var copy = this;
			copy.Points=new Point<double>[this.Points.Length];
			this.Points.CopyTo(copy.Points, 0);

			for (var i = 0; i < copy.Points.Length; i++)
			{
				copy.Points[i]= new Point<double>(copy.Points[i].X + offsetX, copy.Points[i].Y + offsetY);
			}

			return copy;
		}
	}
}
