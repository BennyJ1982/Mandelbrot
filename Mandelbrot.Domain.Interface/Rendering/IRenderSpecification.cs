namespace Mandelbrot.Domain.Rendering
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IRenderSpecification
	{
		IFractalSettings Settings { get; }

		IShader Shader { get; }
	}
}