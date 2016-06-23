namespace Mandelbrot.Domain.Fascade
{
	using Mandelbrot.Domain.Jobs;

	public class FractalContextFactory : IFractalContextFactory
	{
		private readonly IJobFactory jobFactory;

		private readonly IJobRunner jobRunner;

		public FractalContextFactory(IJobFactory jobFactory, IJobRunner jobRunner)
		{
			this.jobFactory = jobFactory;
			this.jobRunner = jobRunner;
		}

		public IFractalContext Create()
		{
			return new FractalContext(this.jobFactory, this.jobRunner, maxDegreeOfParalellelism: 8);
		}
	}
}