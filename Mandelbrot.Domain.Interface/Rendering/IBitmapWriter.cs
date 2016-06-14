namespace Mandelbrot.Domain.Rendering
{
	using System.Drawing;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IBitmapWriter
	{
		FastBitmap CreateBitmap(CalculatedFractalPart calculatedFractalPart, IFractalSettings settings, IShader shader);
	}
}
