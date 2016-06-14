namespace Mandelbrot.Domain.Rendering.Bitmaps
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Rendering.Shaders;

	public interface IBitmapWriter
	{
		FastBitmap CreateBitmap(CalculatedFractalPart calculatedFractalPart, IFractalSettings settings, IShader shader);
	}
}
