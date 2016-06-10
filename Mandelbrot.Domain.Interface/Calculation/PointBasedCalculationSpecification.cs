namespace Mandelbrot.Domain.Calculation
{
	public class PointBasedCalculationSpecification : ICalculationSpecification
	{
		public PointBasedCalculationSpecification(Rectangle<int> rectangleToCalculate)
		{
			this.RectangleToCalculate = rectangleToCalculate;
		}

		public Rectangle<int> RectangleToCalculate { get; private set; }
	}
}
