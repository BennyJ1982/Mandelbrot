namespace Mandelbrot.Domain.Rendering
{
	using System.Collections.Generic;

	public class RendererRegistry : IRendererRegistry
	{
		private readonly HashSet<IRenderer> renderers = new HashSet<IRenderer>();

		public void RegisterRenderer(IRenderer renderer)
		{
			this.renderers.Add(renderer);
		}

		public bool TryGetMatchingRenderer(IRenderSpecification specification, out IRenderer renderer)
		{
			foreach (var knownRenderer in this.renderers)
			{
				if (knownRenderer.CanRender(specification))
				{
					renderer = knownRenderer;
					return true;
				}
			}

			renderer = null;
			return false;
		}
	}
}