namespace Mandelbrot.Domain.Rendering
{
	using Mandelbrot.Domain.Calculation;

	public interface IRenderResult
	{
		CalculatedFractalPart CalculatedFractalPart { get; }
	}
}
