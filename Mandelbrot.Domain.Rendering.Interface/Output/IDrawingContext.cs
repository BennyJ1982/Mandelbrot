namespace Mandelbrot.Domain.Rendering.Output
{
	using System;
	using System.Drawing;

	public interface IDrawingContext : IDisposable
	{
		void DrawBitmap(Bitmap bitmap, Rectangle targetRect);
		void DrawBitmapUnscaled(Bitmap bitmap, Point targetPosition);
	}
}
