namespace Mandelbrot.Domain.Rendering.Shaders
{
	using System.Collections.Generic;
	using System.Drawing;

	public class FireShader : PaletteBasedShader
	{
		public override string ToString()
		{
			return "Fire";
		}

		protected override Color[] InitPalette()
		{
			var colors = new List<Color>();
			var image = Resources.FirePalette;
			for (var a = 0; a < image.Width; a++)
			{
				colors.Add(image.GetPixel(a,0));
			}

			return colors.ToArray();
		}
	}
}
