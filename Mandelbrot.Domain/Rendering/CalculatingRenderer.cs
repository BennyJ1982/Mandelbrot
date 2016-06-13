namespace Mandelbrot.Domain.Rendering
{
	using System;
	using System.Linq;
	using System.Threading;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Output;
	using Mandelbrot.Domain.Rendering.Specifications;

	public class CalculatingRenderer : IRenderer
	{
		private readonly ICalculatorRegistry calculatorRegistry;

		private readonly IRenderSpecificationFactory renderSpecificationFactory;

		private readonly IRendererRegistry rendererRegistry;

		public CalculatingRenderer(
			ICalculatorRegistry calculatorRegistry,
			IRenderSpecificationFactory renderSpecificationFactory,
			IRendererRegistry rendererRegistry)
		{
			this.calculatorRegistry = calculatorRegistry;
			this.renderSpecificationFactory = renderSpecificationFactory;
			this.rendererRegistry = rendererRegistry;
		}

		public bool CanRender(IRenderSpecification specification)
		{
			return specification is CalculatingRenderSpecification;
		}

		public IRenderResult Render(IRenderSpecification specification, IDrawingContext context, CancellationToken cancellationToken)
		{
			var calculator = this.calculatorRegistry.GetAll().First(c => c.CanCalculatePart(specification.CalculationSpecification));

			// first we calculate the fractal part
			var calculatedPart = calculator.CalculatePart(specification.CalculationSpecification, specification.Settings, cancellationToken);

			// then we defer shading and further rendering to a different renderer
			var shadingSpec = this.renderSpecificationFactory.CreateFromCalculatedPart(calculatedPart, specification.Settings, specification.Shader);

			IRenderer shadingRenderer;
			if (!this.rendererRegistry.TryGetMatchingRenderer(shadingSpec, out shadingRenderer))
			{
				throw new InvalidOperationException("No shading renderer found.");
			}

			return shadingRenderer.Render(shadingSpec, context, cancellationToken);
		}
	}
}