namespace Mandelbrot.Domain.Rendering
{
	using Mandelbrot.Domain.Calculation;

	public class RenderResult : IRenderResult
	{
		public RenderResult(CalculatedFractalPart calculatedFractalPart)
		{
			this.CalculatedFractalPart = calculatedFractalPart;
		}

		public CalculatedFractalPart CalculatedFractalPart { get; private set; }
	}
}