using System;

namespace Mandelbrot.Clients.Main
{
	public class StatusEventArgs : EventArgs
	{
		public StatusEventArgs(bool isCalculating)
		{
			this.IsCalculating = isCalculating;
		}

		public bool IsCalculating { get; private set; }
	}
}
