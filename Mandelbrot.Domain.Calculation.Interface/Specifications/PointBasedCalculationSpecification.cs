namespace Mandelbrot.Domain.Calculation.Specifications
{
	public class PointBasedCalculationSpecification : ICalculationSpecification
	{
		public PointBasedCalculationSpecification(Rectangle<int> rectangleToCalculate, int desiredExecutionRank)
		{
			this.RectangleToCalculate = rectangleToCalculate;
			this.DesiredExecutionRank = desiredExecutionRank;
		}

		public Rectangle<int> RectangleToCalculate { get; private set; }

		public int DesiredExecutionRank { get; }
	}
}
