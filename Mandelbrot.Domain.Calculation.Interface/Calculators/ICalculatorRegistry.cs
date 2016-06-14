namespace Mandelbrot.Domain.Calculation.Calculators
{
	using System.Collections.Generic;

	public interface ICalculatorRegistry
	{
		void RegisterFractalCalculator(IFractalCalculator calculator);

		IEnumerable<IFractalCalculator> GetAll();
	}
}
