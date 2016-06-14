
namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;

	public class CalculatedFractalPart
	{
		public CalculatedFractalPart(Rectangle<int> screenPosition, IEnumerable<FractalPath> paths)
		{
			this.ScreenPosition = screenPosition;
			this.Paths = paths;
		}

		public Rectangle<int> ScreenPosition { get; private set; }

		public IEnumerable<FractalPath> Paths { get; private set; }
	}
}
