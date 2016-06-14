namespace Mandelbrot.Clients.Console
{
	using System;
	using System.Drawing;
	using Mandelbrot.Domain.Rendering.Output;

	public class ConsoleDrawingContext : IDrawingContext
	{
		private readonly object lockObject = new object();

		public void DrawBitmap(Bitmap bitmap, Rectangle targetRect)
		{
			for (var x = 0; x < targetRect.Width / bitmap.Width; x++)
			{
				this.DrawBitmapUnscaled(bitmap, new Point(x * bitmap.Width, targetRect.Top));
			}
		}

		public void DrawBitmapUnscaled(Bitmap bitmap, Point targetPosition)
		{
			lock (this.lockObject)
			{
				for (var x = 0; x < bitmap.Width; x++)
				{
					var posX = x + targetPosition.X;
					for (var y = 0; y < bitmap.Height; y++)
					{
						var color = bitmap.GetPixel(x, y);

						if (color.A != 0)
						{
							System.Console.SetCursorPosition(posX, y + targetPosition.Y);
							System.Console.ForegroundColor = GetConsoleColor(color);
							System.Console.Write('█');
						}
					}
				}
			}
		}

		public void Dispose()
		{
			// nothing to do here
		}

		public static ConsoleColor GetConsoleColor(Color c)
		{
			int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
			index |= (c.R > 64) ? 4 : 0; // Red bit
			index |= (c.G > 64) ? 2 : 0; // Green bit
			index |= (c.B > 64) ? 1 : 0; // Blue bit
			return (ConsoleColor)index;
		}
	}
}
