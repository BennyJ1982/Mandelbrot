namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public class CalculatingRenderSpecification : RenderSpecificationBase
	{
		public CalculatingRenderSpecification(
			ICalculationSpecification calculationSpecification,
			Rectangle<int> destinationRectangle,
			IFractalSettings settings,
			IShader shader)
			: base(calculationSpecification, destinationRectangle, settings, shader)
		{
		}
	}
}
