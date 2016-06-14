namespace Mandelbrot.Domain.Rendering
{
	public interface IRendererRegistry
	{
		void RegisterRenderer(IRenderer renderer);

		bool TryGetMatchingRenderer(IRenderSpecification specification, out IRenderer renderer);
	}
}
