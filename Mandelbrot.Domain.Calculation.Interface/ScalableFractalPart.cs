namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;

	public class ScalableFractalPart : CalculatedFractalPart
	{
		public ScalableFractalPart(Rectangle<int> screenPosition, IEnumerable<FractalPath> paths, Rectangle<int> scaledScreenPosition)
			: base(screenPosition, paths)
		{
			this.ScaledScreenPosition = scaledScreenPosition;
		}

		public Rectangle<int> ScaledScreenPosition { get; private set; }
	}
}