namespace Mandelbrot.Domain.Calculation
{
	public class FractalPath : IFractalPrimitive
	{
		public FractalPath(decimal value, params Point<double>[] points)
		{
			this.Value = value;
			this.Points = points;
		}

		public Point<double>[] Points { get; private set; }

		public decimal Value { get; private set; }
	}
}
