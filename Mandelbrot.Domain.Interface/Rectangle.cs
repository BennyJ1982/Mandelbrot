namespace Mandelbrot.Domain
{
	public struct Rectangle<T> where T: struct 
	{
		public Rectangle(T left, T top, T right, T bottom) 
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}

		public T Left { get; private set; }
		public T Top { get; private set; }
		public T Right { get; private set; }
		public T Bottom { get; private set; }
	}
}
