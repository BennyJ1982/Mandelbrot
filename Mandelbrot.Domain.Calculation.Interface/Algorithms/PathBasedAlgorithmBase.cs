namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Specifications;

	public abstract class PathBasedAlgorithmBase : AlgorithmBase, IPathBasedFractal
	{
		public override Rectangle<decimal> DefaultScale => new Rectangle<decimal>(0, 0, 1, 1);

		public abstract IEnumerable<ICalculationSpecification> GetCalculatableParts(IFractalSettings settings, int numberOfParts);

		public IEnumerable<FractalPath> CalculatePaths(FractalPath initialPath, IFractalSettings settings, CancellationToken cancellationToken)
		{
			var paths = new List<FractalPath>();
			this.CalculatePaths(initialPath, paths, settings, cancellationToken);
			return paths;
		}

		protected abstract void CalculatePaths(
			FractalPath initialPath,
			IList<FractalPath> paths,
			IFractalSettings settings,
			CancellationToken cancellationToken);

		protected override decimal ScaleX(decimal x, IFractalSettings settings)
		{
			var scaleFactor = 1 / (settings.FractalRect.Right - settings.FractalRect.Left);

			x = x.ScaleXToFractal(this.DefaultScale, settings.ScreenWidth);
			x = scaleFactor * (x - settings.FractalRect.Left) * settings.ScreenWidth;

			return x;
		}

		protected override decimal ScaleY(decimal y, IFractalSettings settings)
		{
			var scaleFactor = 1 / (settings.FractalRect.Bottom - settings.FractalRect.Top);

			y = y.ScaleYToFractal(this.DefaultScale, settings.ScreenHeight);
			y = scaleFactor * (y - settings.FractalRect.Top) * settings.ScreenHeight;

			return y;
		}

		protected static PathBasedCalculationSpecification[] CreateCalculationSpecifications(List<FractalPath> parts, bool removeRoot)
		{
			FractalPath? root = null;
			if (removeRoot)
			{
				// remove root from the parts list and simply add as additional output
				root = parts[0];
				parts.RemoveAt(0);
			}

			var specs = parts.Select(part => new PathBasedCalculationSpecification(part)).ToArray();
			if (root.HasValue)
			{
				specs.First().AddAdditionalOutput(root.Value);
			}

			return specs;
		}
	}
}
