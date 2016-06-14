namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using System.Linq;
	using Mandelbrot.Domain.Calculation.Calculators;
	using Mandelbrot.Domain.Calculation.Specifications;

	public class PointBasedFractalCalculator : PointBasedFractalCalculatorBase
	{
		private readonly int minimimSizeForPreview;

		public PointBasedFractalCalculator(int minimimSizeForPreview)
		{
			this.minimimSizeForPreview = minimimSizeForPreview;
		}

		public override bool CanCalculatePart(ICalculationSpecification specification)
		{
			return specification is PointBasedCalculationSpecification && !(specification is ScaledPointBasedCalculationSpecification);
		}

		public override IEnumerable<ICalculationSpecification> SplitIntoPreviewParts(ICalculationSpecification specification)
		{
			var originalRectangle = ((PointBasedCalculationSpecification)specification).RectangleToCalculate;
			var order = ColumnCalculationOrder.GetDefault(originalRectangle.Left, originalRectangle.Right).ToArray();
			var stages = order.GroupBy((o => o.Level)).ToArray();

			var firstStageWithNoPreviewColumns = stages.FirstOrDefault(s => s.Any(c => c.Size < this.minimimSizeForPreview));
			foreach (var stage in stages)
			{
				if (stage == firstStageWithNoPreviewColumns)
				{
					// columns in this stage are too small, hence ignore all further child stages (no more previews)
					foreach (var column in stage)
					{
						var rect = new Rectangle<int>(column.Left, originalRectangle.Top, column.Right , originalRectangle.Bottom);
						yield return new PointBasedCalculationSpecification(rect, stage.Key);
					}

					yield break;
				}

				foreach (var column in stage)
				{
					var calculationRect = new Rectangle<int>(column.X, originalRectangle.Top, column.X, originalRectangle.Bottom);
					var targetRect = new Rectangle<int>(column.Left, originalRectangle.Top, column.Right, originalRectangle.Bottom);
					yield return new ScaledPointBasedCalculationSpecification(calculationRect, stage.Key, targetRect);
				}
			}
		}
	}
}