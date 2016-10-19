namespace Mandelbrot.Domain.Fascade
{
	using Mandelbrot.Domain.Rendering.Output;

	public interface IFractalContextFactory
	{
		IFractalContext Create(IScreen screen);
	}
}
