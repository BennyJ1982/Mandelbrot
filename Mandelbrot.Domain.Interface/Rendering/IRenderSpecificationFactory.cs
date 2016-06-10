namespace Mandelbrot.Domain.Rendering
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IRenderSpecificationFactory
	{
		IRenderSpecification CreateDefault(ICalculationSpecification calculationSpecification, IFractalSettings settings, IShader shader);

		IRenderSpecification CreateFromCalculatedPart(CalculatedFractalPart part, IFractalSettings settings, IShader shader);
	}
}
