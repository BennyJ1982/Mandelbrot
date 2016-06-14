namespace Mandelbrot.Clients.WinForms
{
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using Mandelbrot.Domain.Rendering.Output;

	/// <summary>
	/// A PictureBox that implements the IScreen interface
	/// </summary>
	public class PictureBoxScreen : PictureBox, IScreen
	{
		public IDrawingContext GetDrawingContext()
		{
			var graphics = this.CreateGraphics();
			graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			graphics.PixelOffsetMode = PixelOffsetMode.Half;

			return new GraphicsDrawingContext(graphics);
		}
	}
}
