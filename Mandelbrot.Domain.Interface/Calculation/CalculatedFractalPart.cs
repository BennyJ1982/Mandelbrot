
namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;

	public class CalculatedFractalPart
	{
		public CalculatedFractalPart(
			Rectangle<int> screenPosition,
			IEnumerable<IFractalPrimitive> primitives,
			ICalculationSpecification originalSpecification)
		{
			this.ScreenPosition = screenPosition;
			this.Primitives = primitives;
			this.OriginalSpecification = originalSpecification;
		}

		public Rectangle<int> ScreenPosition { get; private set; }

		public IEnumerable<IFractalPrimitive> Primitives { get; private set; }

		public ICalculationSpecification OriginalSpecification { get; private set; }
	}
}
