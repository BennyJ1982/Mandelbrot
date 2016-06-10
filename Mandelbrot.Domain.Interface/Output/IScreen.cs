namespace Mandelbrot.Domain.Output
{
	public interface IScreen
	{
		int Width { get; }
		int Height { get; }

		IDrawingContext GetDrawingContext();
	}
}
