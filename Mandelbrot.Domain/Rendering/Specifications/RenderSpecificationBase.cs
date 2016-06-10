namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public abstract class RenderSpecificationBase : IRenderSpecification
	{
		protected RenderSpecificationBase(ICalculationSpecification calculationSpecification, IFractalSettings settings, IShader shader)
		{
			this.CalculationSpecification = calculationSpecification;
			this.Settings = settings;
			this.Shader = shader;
		}

		public ICalculationSpecification CalculationSpecification { get; private set; }

		public IFractalSettings Settings { get; private set; }

		public IShader Shader { get; private set; }
	}
}
