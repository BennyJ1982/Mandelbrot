namespace Mandelbrot.Domain.Calculation
{
	public class PointBasedCalculationSpecification : ICalculationSpecification
	{
		public PointBasedCalculationSpecification(Rectangle<int> rectangleToCalculate)
		{
			this.RectangleToCalculate = rectangleToCalculate;
		}

		public Rectangle<int> RectangleToCalculate { get; private set; }

		public bool HasScaledResult(Rectangle<int> destinationRectangle)
		{
			return this.RectangleToCalculate.Equals(destinationRectangle);
		}
	}
}
