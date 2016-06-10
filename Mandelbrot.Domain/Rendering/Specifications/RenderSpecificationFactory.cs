namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public class RenderSpecificationFactory : IRenderSpecificationFactory
	{
		public IRenderSpecification CreateDefault(ICalculationSpecification calculationSpecification, IFractalSettings settings, IShader shader)
		{
			return new CalculatingRenderSpecification(calculationSpecification, settings, shader);
		}

		public IRenderSpecification CreateFromCalculatedPart(CalculatedFractalPart part, IFractalSettings settings, IShader shader)
		{
			return new ShadingOnlyRenderSpecification(part, part.OriginalSpecification, settings, shader);
		}
	}
}