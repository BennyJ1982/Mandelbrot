namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Rendering.Shaders;

	public abstract class RenderSpecificationBase : IRenderSpecification
	{
		protected RenderSpecificationBase(IFractalSettings settings, IShader shader)
		{
			this.Settings = settings;
			this.Shader = shader;
		}

		public IFractalSettings Settings { get; private set; }

		public IShader Shader { get; private set; }
	}
}
