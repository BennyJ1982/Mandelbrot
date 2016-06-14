﻿namespace Mandelbrot.Domain.Rendering.Specifications
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public class CalculatingRenderSpecification : RenderSpecificationBase
	{
		public CalculatingRenderSpecification(ICalculationSpecification calculationSpecification, IFractalSettings settings, IShader shader)
			: base(settings, shader)
		{
			this.CalculationSpecification = calculationSpecification;
		}

		public ICalculationSpecification CalculationSpecification { get; private set; }
	}
}
