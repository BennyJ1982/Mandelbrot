using System;
using System.Linq;

namespace Mandelbrot.Domain.Jobs
{
	using System.Collections.Concurrent;

	public static class Extensions
	{
		public static void ForAllInApproximateOrder<TSource>(this ParallelQuery<TSource> source, Action<TSource> action)
		{

			Partitioner.Create(source).AsParallel().AsOrdered().ForAll(action);
		}
	}
}
