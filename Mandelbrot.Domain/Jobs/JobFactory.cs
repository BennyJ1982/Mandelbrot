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
			var stages = this.SplitAndGroupIntoStages(parts);

			var renderSpecs = stages.Select(s => s.Select(part => this.renderSpecificationFactory.CreateDefault(part, settings, shader))).ToArray();
			return new Job(settings, renderSpecs);
		}

		public IJob CreateFromResult(IJobResult result, IFractalSettings settings, IShader shader, int numberOfSectors)
		{
			// TODO: this should use the same grouping logic as the algorithm
			var allParts = result.RenderResults.Select(r => r.CalculatedFractalPart).OrderBy(r => r.ScreenPosition.Left).ToArray();
			var sizePerSector = settings.ScreenWidth / numberOfSectors;

			var combinedParts = new List<CalculatedFractalPart>();
			for (var sector = 0; sector < numberOfSectors; sector++)
			{
				var left = sizePerSector * sector;
				var right = sector == numberOfSectors - 1 ? settings.ScreenWidth - 1 : left + sizePerSector - 1;

				var relevantParts = allParts.Where(part => part.ScreenPosition.Left >= left && part.ScreenPosition.Right <= right);
				var paths = relevantParts.SelectMany(part => part.Paths.Select(path => path.Adjust(part.ScreenPosition.Left - left, 0)));
				combinedParts.Add(new CalculatedFractalPart(new Rectangle<int>(left, 0, right, settings.ScreenHeight - 1), paths));
			}

			var renderSpecs = combinedParts.Select(part => this.renderSpecificationFactory.CreateFromCalculatedPart(part, settings, shader));
			return new Job(settings, renderSpecs.ToArray());
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
 