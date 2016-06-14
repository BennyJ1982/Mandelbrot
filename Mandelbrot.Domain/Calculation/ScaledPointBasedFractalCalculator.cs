namespace Mandelbrot.Domain.Calculation
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	public class ScaledPointBasedFractalCalculator : PointBasedFractalCalculatorBase
	{
		public override bool CanCalculatePart(ICalculationSpecification specification)
		{
			return specification is ScaledPointBasedCalculationSpecification;
		}

		public override IEnumerable<ICalculationSpecification> SplitIntoPreviewParts(ICalculationSpecification specification)
		{
			throw new ArgumentException(@"Provided specification is already the result of another split operation", nameof(specification));
		}

		protected override CalculatedFractalPart CreateResult(
			ICalculationSpecification specification,
			Rectangle<int> rectangleToCalculate,
			Collection<FractalPath> points)
		{
			var destinationRect = ((ScaledPointBasedCalculationSpecification)specification).DesinationRectangle;
			return new ScalableFractalPart(rectangleToCalculate, points, destinationRect);
		}
	}
}