namespace Mandelbrot.Domain.Calculation.Algorithms
{
	public abstract class AlgorithmBase 
	{
		public abstract Rectangle<decimal> DefaultScale { get; }

		protected virtual decimal ScaleX(decimal x, IFractalSettings settings)
		{
			return x.ScaleXToFractal(settings);
		}

		protected virtual decimal ScaleY(decimal y, IFractalSettings settings)
		{
			return y.ScaleYToFractal(settings);
		}

		protected virtual Point<double> ScalePoint(Point<double> point, IFractalSettings settings)
		{
			return new Point<double>((double)this.ScaleX((decimal)point.X, settings), (double)this.ScaleY((decimal)point.Y, settings));
		}

		protected abstract string Name { get; }

		public override string ToString()
		{
			return this.Name;
		}
	}
}
