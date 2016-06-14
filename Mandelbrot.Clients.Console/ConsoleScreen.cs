namespace Mandelbrot.Clients.Console
{
	using System;
	using Mandelbrot.Domain.Rendering.Output;

	public class ConsoleScreen : IScreen
	{
		public int Width => System.Console.WindowWidth;

		public int Height => System.Console.WindowHeight;

		public int CursorLeft
		{
			get
			{
				return System.Console.CursorLeft;
			}
			set
			{
				System.Console.CursorLeft = value;
			}
		}

		public int CursorTop
		{
			get
			{
				return System.Console.CursorTop;
			}
			set
			{
				System.Console.CursorTop = value;
			}
		}

		public IDrawingContext GetDrawingContext()
		{
			return new ConsoleDrawingContext();
		}

		public ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey(true);
		}
	}
}
