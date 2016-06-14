using System;

namespace Mandelbrot.Domain.Rendering
{
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Runtime.InteropServices;

	public class FastBitmap : IDisposable
	{
		public Bitmap Bitmap { get; private set; }

		public byte[] Bits { get; private set; }

		public bool Disposed { get; private set; }

		public int Height { get; private set; }

		public int Width { get; private set; }

		protected GCHandle BitsHandle { get; private set; }

		public FastBitmap(int width, int height)
		{
			this.Width = width;
			this.Height = height;
			this.Bits = new byte[width * height * 4];
			this.BitsHandle = GCHandle.Alloc(this.Bits, GCHandleType.Pinned);
			this.Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, this.BitsHandle.AddrOfPinnedObject());
		}

		public void SetPixel(int x, int y, Color color)
		{
			var index = (y * this.Width+ x) * 4;

			this.Bits[index] = color.B;
			this.Bits[index + 1] = color.G;
			this.Bits[index + 2] = color.R;
			this.Bits[index + 3] = color.A;
		}

		public void Dispose()
		{
			if (this.Disposed) return;
			this.Disposed = true;
			this.Bitmap.Dispose();
			this.BitsHandle.Free();
		}
	}

}
