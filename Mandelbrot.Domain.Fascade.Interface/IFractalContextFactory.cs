namespace Mandelbrot.Domain.Facade
{
	using Mandelbrot.Domain.Rendering.Output;

	public interface IFractalContextFactory
	{
		IFractalContext Create(IScreen screen);
	}
}
