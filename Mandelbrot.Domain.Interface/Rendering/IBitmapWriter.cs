﻿namespace Mandelbrot.Domain.Rendering
{
	using System.Drawing;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Shaders;

	public interface IBitmapWriter
	{
		Bitmap CreateBitmap(CalculatedFractalPart calculatedFractalPart, IFractalSettings settings, IShader shader);
	}
}