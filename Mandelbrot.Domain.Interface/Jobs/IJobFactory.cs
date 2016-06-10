namespace Mandelbrot.Domain.Jobs
{
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IJobFactory
	{
		IJob Create(IFractalSettings settings, IShader shader, int numberOfSectors);

		IJob CreateFromResult(IJobResult result, IFractalSettings settings, IShader shader);
	}
}
