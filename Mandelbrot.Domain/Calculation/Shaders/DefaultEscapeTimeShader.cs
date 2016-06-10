namespace Mandelbrot.Domain.Calculation.Shaders
{
	using System.Collections.Generic;
	using System.Drawing;

	public class DefaultEscapeTimeShader : PaletteBasedShader
	{
		public override string ToString()
		{
			return "Default Escape Time";
		}

		protected override Color[] InitPalette()
		{
			var colors = new List<Color>();
			colors.AddRange(GetGradientColors(Mode.GoUp, Mode.StayDown, Mode.StayDown));
			colors.AddRange(GetGradientColors(Mode.GoDown, Mode.GoUp, Mode.StayDown));
			colors.AddRange(GetGradientColors(Mode.StayDown, Mode.GoDown, Mode.GoUp));
			colors.AddRange(GetGradientColors(Mode.GoUp, Mode.StayDown, Mode.GoDown));
			colors.AddRange(GetGradientColors(Mode.StayUp, Mode.GoUp, Mode.StayDown));
			colors.AddRange(GetGradientColors(Mode.StayUp, Mode.StayUp, Mode.GoUp));
			colors.AddRange(GetGradientColors(Mode.GoDown, Mode.StayUp, Mode.StayUp));
			colors.AddRange(GetGradientColors(Mode.StayDown, Mode.StayUp, Mode.GoDown));

			return colors.ToArray();
		}

		private static IEnumerable<Color> GetGradientColors(Mode redMode, Mode greenMode, Mode blueMode)
		{
			var red = (byte)(redMode == Mode.GoDown || redMode == Mode.StayUp ? 255 : 0);
			var green = (byte)(greenMode == Mode.GoDown || greenMode == Mode.StayUp ? 255 : 0);
			var blue = (byte)(blueMode == Mode.GoDown || blueMode == Mode.StayUp ? 255 : 0);

			for (var a = 0; a < 256; a++)
			{
				yield return Color.FromArgb(red, green, blue);
				red = GetNextColorValue(redMode, red);
				green = GetNextColorValue(greenMode, green);
				blue = GetNextColorValue(blueMode, blue);
			}
		}

		private static byte GetNextColorValue(Mode mode, byte colorValue)
		{
			switch (mode)
			{
				case Mode.GoUp:
					colorValue++;
					break;
				case Mode.GoDown:
					colorValue--;
					break;
			}

			return colorValue;
		}

		private enum Mode
		{
			GoUp,
			GoDown,
			StayUp,
			StayDown
		}
	}
}