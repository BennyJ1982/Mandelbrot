﻿namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using Mandelbrot.Domain.Calculation.Calculators;

	public class CalculatorRegistry : ICalculatorRegistry
	{
		private readonly HashSet<IFractalCalculator> calculators = new HashSet<IFractalCalculator>();

		public void RegisterFractalCalculator(IFractalCalculator calculator)
		{
			this.calculators.Add(calculator);
		}

		public IEnumerable<IFractalCalculator> GetAll()
		{
			return this.calculators;
		}
	}
}