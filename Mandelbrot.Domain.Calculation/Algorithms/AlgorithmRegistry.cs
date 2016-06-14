namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;

	public class AlgorithmRegistry : IAlgorithmRegistry
	{
		private readonly HashSet<IFractalAlgorithm> algorithms = new HashSet<IFractalAlgorithm>();

		public void RegisterFractalAlgorithm(IFractalAlgorithm algorithm)
		{
			this.algorithms.Add(algorithm);
		}

		public IEnumerable<IFractalAlgorithm> GetAll()
		{
			return this.algorithms;
		}
	}
}