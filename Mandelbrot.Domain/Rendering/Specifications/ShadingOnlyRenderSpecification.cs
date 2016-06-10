namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	/// <summary>
	/// A render specification based on an already calculated fractal part. Only applies shading to it.
	/// </summary>
	public class ShadingOnlyRenderSpecification : RenderSpecificationBase
	{
		public ShadingOnlyRenderSpecification(
			CalculatedFractalPart calculatedFractalPart,
			ICalculationSpecification calculationSpecification,
			IFractalSettings settings,
			IShader shader)
			: base(calculationSpecification, settings, shader)
		{
			this.CalculatedFractalPart = calculatedFractalPart;
		}

		public ShadingOnlyRenderSpecification(CalculatedFractalPart calculatedFractalPart, IRenderSpecification originalSpecification)
			: this(
				calculatedFractalPart,
				originalSpecification.CalculationSpecification,
				originalSpecification.Settings,
				originalSpecification.Shader)
		{
		}

		public CalculatedFractalPart CalculatedFractalPart { get; }
	}
}
