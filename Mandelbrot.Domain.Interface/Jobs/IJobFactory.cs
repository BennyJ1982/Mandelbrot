namespace Mandelbrot.Domain.Jobs
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IJobFactory
	{
		IJob Create(IFractalSettings settings, IShader shader, int sectorWidth);

		IJob CreateWithPreview(IFractalSettings settings, IShader shader, int sectorWidth);

		IJob CreateFromResult(IJobResult result, IFractalSettings settings, IShader shader);
	}
}
