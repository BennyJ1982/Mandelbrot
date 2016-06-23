﻿namespace Mandelbrot.Domain.Fascade
{
	using System;
	using System.Threading.Tasks;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Jobs;
	using Mandelbrot.Domain.Rendering.Output;
	using Mandelbrot.Domain.Rendering.Shaders;

	public interface IFractalContext
	{
		Rectangle<decimal> CurrentFractalRect { get; set; }

		IJobResult LastJobResult { get; }

		IFractalAlgorithm CurrentAlgorithm { get; set; }

		IShader CurrentShader { get; set; }

		int MaxIterations { get; set; }

		event EventHandler<StatusEventArgs> StatusChanged;

		Task DrawFractalAsync(IScreen screen);

		Task ZoomInAsync(IScreen screen, Rectangle<int> screenSelection);

		void Cancel ();

		void ResetFractalRect();
	}
}