namespace Mandelbrot.Domain.Rendering
{
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

			using (var lazyGraphics = new LazyGraphics(bitmap))
			{
				using (var pens = new PenCache())
				{
					foreach (var path in calculatedFractalPart.Paths)
					{
						var color = shader.GetColor(path.Value, settings.MaxIterations);
						DrawPath(path, color, bitmap, pens, lazyGraphics);
					}
				}

				return bitmap;
			}
		}

		private static void DrawPath(FractalPath path, Color color, Bitmap bitmap, PenCache pens, LazyGraphics lazyGraphics)
		{
			if (path.IsPixel)
			{
				var point = path.GetFirstPoint().ToDrawingPoint();
				bitmap.SetPixel(point.X, point.Y, color);
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