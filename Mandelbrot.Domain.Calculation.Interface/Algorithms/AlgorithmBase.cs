namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public abstract class AlgorithmBase 
	{
		public abstract Rectangle<double> DefaultScale { get; }

		protected virtual double ScaleX(double x, IFractalSettings settings)
		{
			return x.ScaleXToFractal(settings);
		}

		protected virtual double ScaleY(double y, IFractalSettings settings)
		{
			return y.ScaleYToFractal(settings);
		}

		protected virtual Point<double> ScalePoint(Point<double> point, IFractalSettings settings)
		{
			return new Point<double>(this.ScaleX(point.X, settings), this.ScaleY(point.Y, settings));
		}

		protected abstract string Name { get; }

		public override string ToString()
		{
			return this.Name;
		}
	}
}
