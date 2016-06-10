namespace Mandelbrot.Domain
{
	public interface IFractalCalculationSettings
	{
		Rectangle<decimal> FractalRect { get; }

		int ScreenWidth { get; }
		int ScreenHeight { get; }

		int MaxIterations { get; }
	}
}