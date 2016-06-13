namespace Mandelbrot.Domain.Jobs
{
	using System.Collections.Generic;
	using System.Linq;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;
	using Mandelbrot.Domain.Rendering;

	public class JobFactory : IJobFactory
	{
		private readonly IRenderSpecificationFactory renderSpecificationFactory;

		private readonly ICalculatorRegistry calculatorRegistry;

		public JobFactory(IRenderSpecificationFactory renderSpecificationFactory, ICalculatorRegistry calculatorRegistry)
		{
			this.renderSpecificationFactory = renderSpecificationFactory;
			this.calculatorRegistry = calculatorRegistry;
		}

		public IJob Create(IFractalSettings settings, IShader shader, int numberOfSectors)
		{
			var parts = settings.Algorithm.GetCalculatableParts(settings, numberOfSectors);
			var stages=this.SplitAndGroupIntoStages(parts);

			var renderSpecs = stages.Select(s => s.Select(part => this.renderSpecificationFactory.CreateDefault(part, settings, shader))).ToArray();
			return new Job(settings, renderSpecs);
		}

		public IJob CreateFromResult(IJobResult result, IFractalSettings settings, IShader shader)
		{
			var specs =
				result.RenderResults.Select(
					renderResult => this.renderSpecificationFactory.CreateFromCalculatedPart(renderResult.CalculatedFractalPart, settings, shader));

			return new Job(settings, specs.ToArray());
		}

		private IEnumerable<IEnumerable<ICalculationSpecification>> SplitAndGroupIntoStages(IEnumerable<ICalculationSpecification> parts)
		{
			var specs = new List<ICalculationSpecification>();
			foreach (var part in parts)
			{
				var calculator = this.calculatorRegistry.GetAll().First(c => c.CanCalculatePart(part));
				var childParts = calculator.SplitIntoPreviewParts(part).ToArray();

				specs.AddRange(childParts);

			}

			return specs.GroupBy(s => s.DesiredExecutionRank).OrderBy(g => g.Key);
		}
	}
}
 