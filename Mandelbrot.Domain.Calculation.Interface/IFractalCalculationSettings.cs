namespace Mandelbrot.Domain.Calculation
{
	public interface IFractalCalculationSettings
	{
		Rectangle<double> FractalRect { get; }

		int ScreenWidth { get; }
		int ScreenHeight { get; }

		int MaxIterations { get; }
	}
}