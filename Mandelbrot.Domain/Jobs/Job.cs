namespace Mandelbrot.Domain.Jobs
{
	using System.Collections.Generic;
	using Mandelbrot.Domain.Rendering;

	public class Job : IJob
	{
		public Job(IFractalSettings settings, params IEnumerable<IRenderSpecification>[] stages)
		{
			this.Settings = settings;
			this.Stages = stages;
		}

		public IFractalSettings Settings { get; }

		public IEnumerable<IRenderSpecification>[] Stages { get; private set; }
	}
}