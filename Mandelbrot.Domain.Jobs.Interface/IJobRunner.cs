namespace Mandelbrot.Domain.Jobs
{
	using System.Threading;
	using System.Threading.Tasks;
	using Mandelbrot.Domain.Rendering.Output;

	public interface IJobRunner
	{
		Task<IJobResult> ExecuteAsync(IJob job, IScreen screen, int maxDegreeOfParallelism, CancellationToken cancellationToken);
	}
}
