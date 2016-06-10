using System;

namespace Mandelbrot.Domain.Rendering
{
	using System.Collections.Generic;
	using System.Drawing;

	public class PenCache : IDisposable
	{
		private readonly Dictionary<Color, Pen> pens=new Dictionary<Color, Pen>();

		public Pen GetOrCreatePen(Color color)
		{
			Pen pen;
			if (!this.pens.TryGetValue(color, out pen))
			{
				this.pens[color] = pen = new Pen(color);
			}

			return pen;
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
				foreach (var pen in this.pens.Values)
				{
					pen.Dispose();
				}

				this.pens.Clear();
			}		
		}
	}
}
