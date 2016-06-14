namespace Mandelbrot.Domain.Calculation.Shaders
{
	using System;
	using System.Drawing;

	public abstract class PaletteBasedShader : IShader
	{
		private readonly Lazy<Color[]> lazyPalette;

		protected PaletteBasedShader()
		{
			this.lazyPalette= new Lazy<Color[]>(this.InitPalette);
		}

		public virtual Color GetColor(double mandelbrotValue, int maxIterations)
		{
			var factor = (double)this.lazyPalette.Value.Length / maxIterations;
			var index = (int)(mandelbrotValue * factor);

			return this.lazyPalette.Value[index];
		}

		protected abstract Color[] InitPalette();
	}
}
