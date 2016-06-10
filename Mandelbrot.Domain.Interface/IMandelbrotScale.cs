namespace Mandelbrot.Domain
{
	public interface IMandelbrotScale
	{
		int Left { get; }
		int Top { get; }
		int Right { get; }
		int Bottom { get; }
	}
}
