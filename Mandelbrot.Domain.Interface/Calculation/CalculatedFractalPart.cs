
namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;

	public class CalculatedFractalPart
	{
		public CalculatedFractalPart(
			Rectangle<int> screenPosition,
			IEnumerable<FractalPath> paths,
			ICalculationSpecification originalSpecification)
		{
			this.ScreenPosition = screenPosition;
			this.Paths = paths;
			this.OriginalSpecification = originalSpecification;
		}

		public Rectangle<int> ScreenPosition { get; private set; }

		public IEnumerable<FractalPath> Paths { get; private set; }

		public ICalculationSpecification OriginalSpecification { get; private set; }
	}
}
