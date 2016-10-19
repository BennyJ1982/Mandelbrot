namespace Mandelbrot.Domain.Fascade
{
	using Mandelbrot.Domain.Jobs;
	using Mandelbrot.Domain.Rendering.Output;

	public class FractalContextFactory : IFractalContextFactory
	{
		private readonly IJobFactory jobFactory;

		private readonly IJobRunner jobRunner;

		public FractalContextFactory(IJobFactory jobFactory, IJobRunner jobRunner)
		{
			this.jobFactory = jobFactory;
			this.jobRunner = jobRunner;
		}

		public IFractalContext Create(IScreen screen)
		{
			return new FractalContext(this.jobFactory, this.jobRunner, screen, maxDegreeOfParalellelism: 8);
		}
	}
}