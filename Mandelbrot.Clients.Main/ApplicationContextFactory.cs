namespace Mandelbrot.Clients.Main
{
	using Mandelbrot.Domain.Jobs;

	public class ApplicationContextFactory : IApplicationContextFactory
	{
		private readonly IJobFactory jobFactory;

		private readonly IJobRunner jobRunner;

		public ApplicationContextFactory(IJobFactory jobFactory, IJobRunner jobRunner)
		{
			this.jobFactory = jobFactory;
			this.jobRunner = jobRunner;
		}

		public IApplicationContext Create()
		{
			return new ApplicationContext(this.jobFactory, this.jobRunner, maxDegreeOfParalellelism: 8);
		}
	}
}