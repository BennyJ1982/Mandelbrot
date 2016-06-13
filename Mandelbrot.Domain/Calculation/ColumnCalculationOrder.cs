namespace Mandelbrot.Domain.Calculation
{
	using System.Collections.Generic;
	using System.Linq;

	public class ColumnCalculationOrder
	{
		public ColumnCalculationOrder(int x, int level, int left, int right)
		{
			this.X = x;
			this.Level = level;
			this.Left = left;
			this.Right = right;
		}

		public int X { get; private set; }

		public int Left { get; private set; }

		public int Right { get; private set; }

		public int Size => this.Right - this.Left + 1;

		public int Level { get; private set; }

		public static IEnumerable<ColumnCalculationOrder> GetDefault(int left, int right)
		{
			return InternalGetDefault(left, right, 0).OrderBy(c => c.Level);
		}

		private static IEnumerable<ColumnCalculationOrder> InternalGetDefault(int left, int right, int level)
		{
			var count = right - left + 1;
			if (count == 1)
			{
				yield return new ColumnCalculationOrder(left, level, left, right);
			}
			else if (count == 2)
			{
				yield return new ColumnCalculationOrder(left, level, left, right);
				yield return new ColumnCalculationOrder(right, level, left, right);
			}
			else
			{
				var middle = count / 2 + left;
				yield return new ColumnCalculationOrder(middle, level, left, right);

				level++;

				foreach (var subElement in InternalGetDefault(left, middle - 1, level))
				{
					yield return subElement;
				}

				foreach (var subElement in InternalGetDefault(middle + 1, right, level))
				{
					yield return subElement;
				}
			}
		}
	}
}