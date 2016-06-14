namespace Mandelbrot.Domain.Calculation.Specifications
{
	public class ScaledPointBasedCalculationSpecification : PointBasedCalculationSpecification
	{
		public Rectangle<int> DesinationRectangle { get; private set; }

		public ScaledPointBasedCalculationSpecification(
			Rectangle<int> rectangleToCalculate,
			int desiredExecutionRank,
			Rectangle<int> desinationRectangle)
			: base(rectangleToCalculate, desiredExecutionRank)
		{
			this.DesinationRectangle = desinationRectangle;
		}
	}
}