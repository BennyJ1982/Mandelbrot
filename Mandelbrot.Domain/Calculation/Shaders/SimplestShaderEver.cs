namespace Mandelbrot.Domain.Calculation.Shaders
{
	using System.Drawing;

	public class SimplestShaderEver : IShader
	{
		public Color GetColor(double mandelbrotValue, int maxIterations)
		{
			var i = (double)255 / maxIterations;
			var colorValue = (int)(mandelbrotValue * i);
			return Color.FromArgb(colorValue, colorValue, colorValue);
		}

		public override string ToString()
		{
			return "Simplest Shader Ever!!";
		}

	}
}