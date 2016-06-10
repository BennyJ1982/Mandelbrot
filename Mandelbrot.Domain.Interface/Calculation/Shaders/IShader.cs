namespace Mandelbrot.Domain.Calculation.Shaders
{
	using System.Drawing;

	public interface IShader
	{
		Color GetColor(decimal mandelbrotValue, int maxIterations);
	}
}
