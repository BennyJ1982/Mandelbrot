namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using System.Linq;

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
			var stages = ColumnCalculationOrder.GetDefault(originalRectangle.Left, originalRectangle.Right).ToArray().GroupBy((o => o.Level));

			foreach(var stage in stages)
			{

				//if (column.Size < this.minimimSizeForPreview)
				//{
				//	foreach (var remainingColumn in order.Where(o => o.Level == column.Level))
				//	{
				//		var rect = new Rectangle<int>(remainingColumn.Left, originalRectangle.Top, remainingColumn.Right, originalRectangle.Bottom);
				//		yield return new PointBasedCalculationSpecification(rect);
				//	}

				//	yield break;
				//}


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