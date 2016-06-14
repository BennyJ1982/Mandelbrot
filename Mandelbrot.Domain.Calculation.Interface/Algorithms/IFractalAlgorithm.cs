namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System.Collections.Generic;
	using Mandelbrot.Domain.Calculation.Specifications;

	public interface IFractalAlgorithm
	{
		Rectangle<decimal> DefaultScale { get; }

		IEnumerable<ICalculationSpecification> GetCalculatableParts(IFractalSettings settings, int numberOfParts);
	}
}