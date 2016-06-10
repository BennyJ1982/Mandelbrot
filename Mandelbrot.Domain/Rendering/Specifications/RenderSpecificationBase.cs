namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public abstract class RenderSpecificationBase : IRenderSpecification
	{
		protected RenderSpecificationBase(
			ICalculationSpecification calculationSpecification,
			Rectangle<int> destinationRectangle,
			IFractalSettings settings,
			IShader shader)
		{
			this.CalculationSpecification = calculationSpecification;
			this.DestinationRectangle = destinationRectangle;
			this.Settings = settings;
			this.Shader = shader;
		}

		public ICalculationSpecification CalculationSpecification { get; private set; }

		public Rectangle<int> DestinationRectangle { get; private set; }

		public bool RenderScaled => this.CalculationSpecification.HasScaledResult(this.DestinationRectangle);

		public IFractalSettings Settings { get; private set; }

		public IShader Shader { get; private set; }
	}
}
