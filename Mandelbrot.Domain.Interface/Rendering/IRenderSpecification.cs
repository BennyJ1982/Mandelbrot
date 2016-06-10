namespace Mandelbrot.Domain.Rendering
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IRenderSpecification
	{
		ICalculationSpecification CalculationSpecification{ get; }

		Rectangle<int> DestinationRectangle { get; }

		bool RenderScaled { get; }

		IFractalSettings Settings { get; }

		IShader Shader { get; }
	}
}