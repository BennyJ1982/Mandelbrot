namespace Mandelbrot.Domain.Calculation
{
	public class FractalPoint : IFractalPrimitive
	{
		public FractalPoint(int screenX, int screenY, decimal value)
		{
			this.ScreenX = screenX;
			this.ScreenY = screenY;
			this.Value = value;
		}

		public int ScreenX { get; private set; }

		public int ScreenY { get; private set; }

		public decimal Value { get; private set; }
	}
}
