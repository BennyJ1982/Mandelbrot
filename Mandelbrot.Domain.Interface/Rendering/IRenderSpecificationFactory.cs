namespace Mandelbrot.Domain.Rendering
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IRenderSpecificationFactory
	{
		IRenderSpecification CreateDefault(
			ICalculationSpecification calculationSpecification,
			Rectangle<int> destinationRectangle,
			IFractalSettings settings,
			IShader shader);

		IRenderSpecification CreateFromCalculatedPart(
			CalculatedFractalPart part,
			Rectangle<int> destinationRectangle,
			IFractalSettings settings,
			IShader shader);
	}
}
