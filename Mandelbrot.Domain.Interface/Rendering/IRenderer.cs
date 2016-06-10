namespace Mandelbrot.Domain.Rendering
{
	using System.Threading;
	using Mandelbrot.Domain.Output;

	public interface IRenderer
	{
		bool CanRender(IRenderSpecification specification);

		IRenderResult Render(IRenderSpecification specification, IDrawingContext context, CancellationToken cancellationToken);
	}
}
