namespace Mandelbrot.Domain.Rendering.Output
{
	public interface IScreen
	{
		int Width { get; }
		int Height { get; }

		IDrawingContext GetDrawingContext();
	}
}
