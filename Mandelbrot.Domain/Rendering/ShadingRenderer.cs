namespace Mandelbrot.Domain.Rendering
{
	using System.Drawing;
	using System.Threading;
	using Mandelbrot.Domain.Calculation;
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
			var shadingSpecification = (ShadingOnlyRenderSpecification)specification;
			var fractalPart = shadingSpecification.CalculatedFractalPart;

			using (var fastBitmap = this.renderer.CreateBitmap(fractalPart, specification.Settings, specification.Shader))
			{
				var scalableFractalPart = fractalPart as ScalableFractalPart;
				if (scalableFractalPart != null && !scalableFractalPart.ScaledScreenPosition.Equals(scalableFractalPart.ScreenPosition))
				{
					var targetRect = new Rectangle(
						scalableFractalPart.ScaledScreenPosition.Left,
						scalableFractalPart.ScaledScreenPosition.Top,
						scalableFractalPart.ScaledScreenPosition.Right - scalableFractalPart.ScaledScreenPosition.Left + 1,
						scalableFractalPart.ScaledScreenPosition.Bottom - scalableFractalPart.ScaledScreenPosition.Top + 1);

					context.DrawBitmap(fastBitmap.Bitmap, targetRect);
				}
				else
				{
					var targetPoint = new Point(fractalPart.ScreenPosition.Left, fractalPart.ScreenPosition.Top);
					context.DrawBitmapUnscaled(fastBitmap.Bitmap, targetPoint);
				}
			}

			return new RenderResult(shadingSpecification.CalculatedFractalPart);
		}
	}
}