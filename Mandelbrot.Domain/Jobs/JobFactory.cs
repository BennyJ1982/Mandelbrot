namespace Mandelbrot.Domain.Jobs
{
	using System.Linq;
	using Mandelbrot.Domain.Calculation.Shaders;
	using Mandelbrot.Domain.Rendering;

	public class JobFactory : IJobFactory
	{
		private readonly IRenderSpecificationFactory renderSpecificationFactory;

		public JobFactory(IRenderSpecificationFactory renderSpecificationFactory)
		{
			this.renderSpecificationFactory = renderSpecificationFactory;
		}

		public IJob Create(IFractalSettings settings, IShader shader, int numberOfSectors)
		{
			var sectors = settings.Algorithm.GetCalculatableParts(settings, numberOfSectors);
			var renderSpecs = sectors.Select(sector => this.renderSpecificationFactory.CreateDefault(sector, settings, shader)).ToArray();

			return new Job(settings, renderSpecs);
		}

		public IJob CreateFromResult(IJobResult result, IFractalSettings settings, IShader shader)
		{
			var specs =
				result.RenderResults.Select(
					renderResult => this.renderSpecificationFactory.CreateFromCalculatedPart(renderResult.CalculatedFractalPart, settings, shader));

			return new Job(settings, specs.ToArray());
		}
	}
}
 