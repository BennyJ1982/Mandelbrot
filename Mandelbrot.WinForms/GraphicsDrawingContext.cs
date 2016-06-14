namespace Mandelbrot.Clients.WinForms
{
	using System;
	using System.Drawing;
	using Mandelbrot.Domain.Rendering.Output;

	public class GraphicsDrawingContext : IDrawingContext
	{
		private Graphics graphics;

		public GraphicsDrawingContext(Graphics graphics)
		{
			this.graphics = graphics;
			graphics.Clear(SystemColors.Control);
		}

		public void DrawBitmap(Bitmap bitmap, Rectangle targetRect)
		{
			lock (this.graphics)
			{
				this.graphics.DrawImage(bitmap, targetRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel);
			}
		}

		public void DrawBitmapUnscaled(Bitmap bitmap, Point targetPosition)
		{
			lock (this.graphics)
			{
				this.graphics.DrawImageUnscaled(bitmap, targetPosition.X, targetPosition.Y);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources
				if (this.graphics != null)
				{
					this.graphics.Dispose();
					this.graphics = null;
				}
			}
		}
	}
}
