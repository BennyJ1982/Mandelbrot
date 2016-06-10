namespace Mandelbrot.Domain.Rendering
{
	using System;
	using System.Drawing;

	public class LazyGraphics : IDisposable
	{
		private Lazy<Graphics> graphics;

		public LazyGraphics(Image bitmap)
		{
			this.graphics = new Lazy<Graphics>(() => Graphics.FromImage(bitmap));
		}

		public Graphics Value => this.graphics.Value;

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.graphics != null && this.graphics.IsValueCreated)
				{
					this.graphics.Value.Dispose();
					this.graphics = null;
				}
			}
		}
	}
}
