namespace Mandelbrot.Domain.Rendering
{
	using System.Drawing;
	using System.Threading;
	using Mandelbrot.Domain.Output;
	using Mandelbrot.Domain.Rendering.Specifications;

	public class ShadingRenderer : IRenderer
	{
		private readonly IBitmapWriter renderer;

		public ShadingRenderer(IBitmapWriter renderer)
		{
			this.renderer = renderer;
		}

		public bool CanRender(IRenderSpecification specification)
		{
			return specification is ShadingOnlyRenderSpecification;
		}

		public IRenderResult Render(IRenderSpecification specification, IDrawingContext context, CancellationToken cancellationToken)
		{
			var calculatedPart = ((ShadingOnlyRenderSpecification)specification).CalculatedFractalPart;
			using (var bitmap = this.renderer.CreateBitmap(calculatedPart, specification.Settings, specification.Shader))
			{
				if (specification.RenderScaled)
				{
					var targetRect = new Rectangle(
						specification.DestinationRectangle.Left,
						specification.DestinationRectangle.Top,
						specification.DestinationRectangle.Right - specification.DestinationRectangle.Left + 1,
						specification.DestinationRectangle.Bottom - specification.DestinationRectangle.Top + 1);

					context.DrawBitmap(bitmap, targetRect);
				}
				else
				{
					var targetPoint = new Point(specification.DestinationRectangle.Left, specification.DestinationRectangle.Top);
					context.DrawBitmapUnscaled(bitmap, targetPoint);
				}
			}

			return new RenderResult(calculatedPart);
		}
	}
}