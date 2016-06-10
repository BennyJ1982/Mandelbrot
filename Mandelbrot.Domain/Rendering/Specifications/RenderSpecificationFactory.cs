namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public class RenderSpecificationFactory : IRenderSpecificationFactory
	{
		public IRenderSpecification CreateDefault(
			ICalculationSpecification calculationSpecification,
			Rectangle<int> destinationRectangle,
			IFractalSettings settings,
			IShader shader)
		{
			return new CalculatingRenderSpecification(calculationSpecification, destinationRectangle, settings, shader);
		}

		public IRenderSpecification CreateFromCalculatedPart(
			CalculatedFractalPart part,
			Rectangle<int> destinationRectangle,
			IFractalSettings settings,
			IShader shader)
		{
			return new ShadingOnlyRenderSpecification(part, part.OriginalSpecification, destinationRectangle, settings, shader);
		}
	}
}