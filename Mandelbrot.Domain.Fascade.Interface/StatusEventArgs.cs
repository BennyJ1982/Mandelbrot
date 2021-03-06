﻿namespace Mandelbrot.Domain.Facade
{
	using System;

	public class StatusEventArgs : EventArgs
	{
		public StatusEventArgs(bool isCalculating)
		{
			this.IsCalculating = isCalculating;
		}

		public bool IsCalculating { get; private set; }
	}
}
