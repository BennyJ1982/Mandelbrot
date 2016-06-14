namespace Mandelbrot.Domain.Rendering
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Rendering.Shaders;

	public interface IRenderSpecification
	{
		IFractalSettings Settings { get; }

		IShader Shader { get; }
	}
}