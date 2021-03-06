﻿namespace Mandelbrot.Domain.Calculation.Specifications
{
	using System.Collections.Generic;

	public class PathBasedCalculationSpecification : ICalculationSpecification
	{
		private readonly List<FractalPath> additionalOutput = new List<FractalPath>();

		public PathBasedCalculationSpecification(FractalPath initialPath)
		{
			this.InitialPath = initialPath;
		}

		public FractalPath InitialPath { get; private set; }

		public IEnumerable<FractalPath> AdditionalOutput => this.additionalOutput;

		public void AddAdditionalOutput(FractalPath path)
		{
			this.additionalOutput.Add(path);
		}

		public int DesiredExecutionRank => 0;
	}
}
