namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	/// <summary>
	/// A render specification based on an already calculated fractal part. Only applies shading to it.
	/// </summary>
	public class ShadingOnlyRenderSpecification : RenderSpecificationBase
	{
		public ShadingOnlyRenderSpecification(CalculatedFractalPart calculatedFractalPart, IFractalSettings settings, IShader shader)
			: base(settings, shader)
		{
			this.CalculatedFractalPart = calculatedFractalPart;
		}

		public CalculatedFractalPart CalculatedFractalPart { get; }
	}
}
