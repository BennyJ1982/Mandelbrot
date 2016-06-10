namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;

	public interface IFractalAlgorithm
	{
		Rectangle<decimal> DefaultScale { get; }

		IEnumerable<ICalculationSpecification> GetCalculatableParts(IFractalSettings settings, int numberOfParts);
	}
}