namespace Mandelbrot.Domain.Rendering.Shaders
{
	using System.Drawing;

	public interface IShader
	{
		Color GetColor(double mandelbrotValue, int maxIterations);
	}
}
