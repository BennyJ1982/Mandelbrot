namespace Mandelbrot.Clients.Console
{
	using System;

	public class InputController
	{
		private readonly ConsoleScreen screen;

		public InputController(ConsoleScreen screen)
		{
			this.screen = screen;
		}

		public event EventHandler OnEnterPressed;

		public void EnterLoop()
		{
			var key = this.screen.ReadKey();
			while (key.Key != ConsoleKey.Escape)
			{
				switch (key.Key)
				{
					case ConsoleKey.LeftArrow:
						this.GoLeft();
						break;
					case ConsoleKey.RightArrow:
						this.GoRight();
						break;
					case ConsoleKey.UpArrow:
						this.GoUp();
						break;
					case ConsoleKey.DownArrow:
						this.GoDown();
						break;
					case ConsoleKey.Enter:
						this.OnEnter();
						break;
				}

				key = this.screen.ReadKey();
			}
		}

		private void OnEnter()
		{
			var enterPressed = this.OnEnterPressed;
			enterPressed?.Invoke(this, EventArgs.Empty);
		}

		private void GoUp()
		{
			if (this.screen.CursorTop > 0)
			{
				this.screen.CursorTop--;
			}
		}

		private void GoDown()
		{
			if (this.screen.CursorTop < this.screen.Height - 1)
			{
				this.screen.CursorTop++;
			}
		}

		private void GoLeft()
		{
			if (this.screen.CursorLeft > 0)
			{
				this.screen.CursorLeft--;
			}
		}

		private void GoRight()
		{
			if (this.screen.CursorLeft < this.screen.Width - 1)
			{
				this.screen.CursorLeft++;
			}
		}
	}
}
