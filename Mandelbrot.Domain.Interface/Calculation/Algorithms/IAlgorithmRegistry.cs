namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;

	public interface IAlgorithmRegistry
	{
		void RegisterFractalAlgorithm(IFractalAlgorithm algorithm);

		IEnumerable<IFractalAlgorithm> GetAll();
	}
}
