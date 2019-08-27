namespace Mandelbrot.Domain.Calculation
{
	using Mandelbrot.Domain.Calculation.Algorithms;

	public class FractalSettings : IFractalSettings
	{
		public FractalSettings(IFractalAlgorithm algorithm, int screenWidth, int screenHeight, Rectangle<double> fractalRect, int maxIterations)
		{
			this.Algorithm = algorithm;
			this.FractalRect = fractalRect;
			this.MaxIterations = maxIterations;
			this.ScreenWidth = screenWidth;
			this.ScreenHeight = screenHeight;
		}

		public Rectangle<double> FractalRect { get; }

		public int ScreenWidth { get; }

		public int ScreenHeight { get; }

		public int MaxIterations { get; }

		public override bool Equals(object obj)
		{
			var settings = obj as FractalSettings;
			if (settings == null)
			{
				return false;
			}

			return 
				this.Algorithm.Equals(settings.Algorithm) &&
				this.FractalRect.Equals(settings.FractalRect) && 
				this.ScreenWidth.Equals(settings.ScreenWidth) && 
				this.ScreenHeight.Equals(settings.ScreenHeight) && 
				this.MaxIterations.Equals(settings.MaxIterations);
		}

		public override int GetHashCode()
		{
			return 
				this.Algorithm.GetHashCode() ^
				this.FractalRect.GetHashCode() ^
				this.ScreenWidth.GetHashCode() ^
				this.ScreenHeight.GetHashCode() ^
				this.MaxIterations.GetHashCode();
		}

		public IFractalAlgorithm Algorithm { get; }
	}
}