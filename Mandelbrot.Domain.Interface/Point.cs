namespace Mandelbrot.Domain
{
	public struct Point<T>
	{
		public Point(T x, T y)
		{
			this.X = x;
			this.Y = y;
		}

		public T X { get; private set; }
		public T Y { get; private set; }
	}
}
