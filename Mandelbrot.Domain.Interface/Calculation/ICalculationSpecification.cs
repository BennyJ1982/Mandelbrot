namespace Mandelbrot.Domain.Calculation
{
	using System.Security.Cryptography.X509Certificates;

	public interface ICalculationSpecification
	{
		int DesiredExecutionRank { get; }
	}
}
