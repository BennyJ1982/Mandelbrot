namespace Mandelbrot.Domain.Jobs
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;
	using Mandelbrot.Domain.Rendering;

	public class JobFactory : IJobFactory
	{
		private readonly IRenderSpecificationFactory renderSpecificationFactory;

		public JobFactory(IRenderSpecificationFactory renderSpecificationFactory)
		{
			this.renderSpecificationFactory = renderSpecificationFactory;
		}

		public IJob Create(IFractalSettings settings, IShader shader, int sectorWidth)
		{
			var screenDimensions = new Rectangle<int>(0, 0, settings.ScreenWidth-1, settings.ScreenHeight-1);
			var numberOfSectors = settings.ScreenWidth % sectorWidth > 0
									? settings.ScreenWidth / sectorWidth + 1
									: settings.ScreenWidth / sectorWidth;

			var sectors = settings.Algorithm.GetCalculatableParts(settings, numberOfSectors);
			var renderSpecs = new List<IRenderSpecification>();

			foreach (var sector in sectors)
			{
				// TODO
				var pointBasedSector = sector as PointBasedCalculationSpecification;
				if (pointBasedSector != null)
				{
					renderSpecs.Add(this.renderSpecificationFactory.CreateDefault(sector, pointBasedSector.RectangleToCalculate, settings, shader));
				}
				else
				{
					var lineBasedSector = (PathBasedCalculationSpecification)sector;
					renderSpecs.Add(this.renderSpecificationFactory.CreateDefault(sector, screenDimensions, settings, shader));

				}
			}
			 
			return new Job(settings, renderSpecs);
		}

		public IJob CreateWithPreview(IFractalSettings settings, IShader shader, int sectorWidth)
		{
			throw new NotImplementedException();

			//var previewSectors = GetPreviewSectorBoundaries(settings.ScreenWidth, settings.ScreenHeight, sectorWidth, 1);
			//var mainSectors = GetSectorBoundaries(settings.ScreenWidth, settings.ScreenHeight, sectorWidth, 1);

			//var previewsSpecs =
			//	previewSectors.Select(sector => this.renderSpecificationFactory.CreateDefault(TODO, sector.Item2, settings, shader));

			//var mainSpecs = mainSectors.Select(sector => this.renderSpecificationFactory.CreateDefault(TODO, sector.Item2, settings, shader));

			//return new Job(settings, previewsSpecs.ToArray(), mainSpecs.ToArray());
		}

		public IJob CreateFromResult(IJobResult result, IFractalSettings settings, IShader shader)
		{
			var specs =
				result.RenderResults.Select(
					renderResult =>
					this.renderSpecificationFactory.CreateFromCalculatedPart(
						renderResult.CalculatedFractalPart,
						renderResult.CalculatedFractalPart.ScreenPosition,
						settings,
						shader));

			return new Job(settings, specs.ToArray());
		}

		private static IEnumerable<Tuple<Rectangle<int>, Rectangle<int>>> GetPreviewSectorBoundaries(
			int screenWidth,
			int screenHeight,
			int sectorWidth,
			int previewWidth)
		{
			for (var x = 0; x < screenWidth; x += sectorWidth)
			{
				var fractionBounds = new Rectangle<int>(x, 0, x + previewWidth - 1, screenHeight - 1);
				var targetBounds = new Rectangle<int>(x, 0, x + sectorWidth - previewWidth, screenHeight - 1);
				yield return Tuple.Create(fractionBounds, targetBounds);
			}
		}

		private static IEnumerable<Tuple<Rectangle<int>, Rectangle<int>>> GetSectorBoundaries(
			int screenWidth,
			int screenHeight,
			int sectorWidth,
			int previewWidth)
		{
			for (var x = 0; x < screenWidth; x += sectorWidth)
			{
				var right = x + sectorWidth > screenWidth - 1 ? screenWidth - 1 - previewWidth : x + sectorWidth - 1;
				var fractionBounds = new Rectangle<int>(x + previewWidth, 0, right, screenHeight - 1);
				yield return Tuple.Create(fractionBounds, fractionBounds);
			}
		}
	}
}
 