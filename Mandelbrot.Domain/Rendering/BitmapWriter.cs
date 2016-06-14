namespace Mandelbrot.Domain.Rendering
{
	using System.Drawing;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public class BitmapWriter : IBitmapWriter
	{
		public FastBitmap CreateBitmap(CalculatedFractalPart calculatedFractalPart, IFractalSettings settings, IShader shader)
		{
			var fastBitmap = new FastBitmap(
				calculatedFractalPart.ScreenPosition.Right - calculatedFractalPart.ScreenPosition.Left + 1,
				calculatedFractalPart.ScreenPosition.Bottom - calculatedFractalPart.ScreenPosition.Top + 1);

			using (var lazyGraphics = new LazyGraphics(fastBitmap.Bitmap))
			{
				using (var pens = new PenCache())
				{
					foreach (var path in calculatedFractalPart.Paths)
					{
						var color = shader.GetColor(path.Value, settings.MaxIterations);
						DrawPath(path, color, fastBitmap, pens, lazyGraphics);
					}

				}
			}

			return fastBitmap;
		}

		private static void DrawPath(FractalPath path, Color color, FastBitmap fastBitmap, PenCache pens, LazyGraphics lazyGraphics)
		{
			if (path.IsPixel)
			{
				var point = path.GetFirstPoint();
				fastBitmap.SetPixel((int)point.X, (int)point.Y, color);
				return;
			}

			var pen = pens.GetOrCreatePen(color);
			if (path.IsLine)
			{
				lazyGraphics.Value.DrawLine(pen, path.GetFirstPoint().ToDrawingPoint(), path.GetSecondPoint().ToDrawingPoint());
				return;
			}

			for (var i = 0; i < path.Points.Length - 1; i++)
			{
				lazyGraphics.Value.DrawLine(pen, path.Points[i].ToDrawingPoint(), path.Points[i + 1].ToDrawingPoint());
			}
		}
	}
}