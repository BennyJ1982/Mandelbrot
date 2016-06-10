namespace Mandelbrot.Domain.Rendering
{
	using System.Collections.Generic;
	using System.Drawing;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public class BitmapWriter : IBitmapWriter
	{
		public Bitmap CreateBitmap(CalculatedFractalPart calculatedFractalPart, IFractalSettings settings, IShader shader)
		{
			var bitmap = new Bitmap(
				calculatedFractalPart.ScreenPosition.Right - calculatedFractalPart.ScreenPosition.Left + 1,
				calculatedFractalPart.ScreenPosition.Bottom - calculatedFractalPart.ScreenPosition.Top + 1);

			// TODO
			using (var graphics = Graphics.FromImage(bitmap))
			{
				var pens = new Dictionary<int, Pen>();
				try
				{
					foreach (var primitive in calculatedFractalPart.Primitives)
					{
						var point = primitive as FractalPoint;
						if (point != null)
						{
							var color = shader.GetColor(point.Value, settings.MaxIterations);
							bitmap.SetPixel(point.ScreenX, point.ScreenY, color);
						}
						else
						{
							var path = (FractalPath)primitive;

							Pen pen;
							if (!pens.TryGetValue((int)path.Value, out pen))
							{
								pens[(int)path.Value] = pen = new Pen(shader.GetColor(path.Value, settings.MaxIterations));
							}

							var line = path as FractalLine;
							if (line != null)
							{
								graphics.DrawLine(pen, (int)line.X1, (int)line.Y1, (int)line.X2, (int)line.Y2);
							}
							else
							{
								for (var i = 0; i < path.Points.Length - 1; i++)
								{
									graphics.DrawLine(pen, (int)path.Points[i].X, (int)path.Points[i].Y, (int)path.Points[i + 1].X, (int)path.Points[i + 1].Y);
								}
							}
						}
					}
				}
				finally
				{
					foreach (var pen in pens.Values)
					{
						pen.Dispose();
					}
				}

				return bitmap;
			}
		}
	}
}