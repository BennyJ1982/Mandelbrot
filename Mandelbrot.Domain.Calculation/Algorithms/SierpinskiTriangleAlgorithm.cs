namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using Mandelbrot.Domain.Calculation.Specifications;

	public class SierpinskiTriangleAlgorithm : PathBasedAlgorithmBase
	{
		private readonly float initialAngle;

		public SierpinskiTriangleAlgorithm(float initialAngle)
		{
			this.initialAngle = initialAngle;
		}

		public override IEnumerable<ICalculationSpecification> GetCalculatableParts(IFractalSettings settings, int numberOfParts)
		{
			var radius = Math.Min(settings.ScreenWidth, settings.ScreenHeight) / 3;
			var centerPoint = new Point<double>((double)settings.ScreenWidth / 2, (double)settings.ScreenHeight / 2);

			var a = this.ScalePoint(Geometry.GetPointFromAngle(centerPoint, radius, this.initialAngle), settings);
			var b = this.ScalePoint(Geometry.GetPointFromAngle(centerPoint, radius, this.initialAngle + 120), settings);
			var c = this.ScalePoint(Geometry.GetPointFromAngle(centerPoint, radius, this.initialAngle + 240), settings);

			var parts = new List<FractalPath>();
			this.AddTriangles(parts, a, b, c, 0, 2, new CancellationToken());
			return CreateCalculationSpecifications(parts, true);
		}

		protected override string Name => "Sierpinski Triangle";

		protected override void CalculatePaths(
			FractalPath initialPath,
			IList<FractalPath> paths,
			IFractalSettings settings,
			CancellationToken cancellationToken)
		{
			this.AddTriangles(
				paths,
				initialPath.Points[0],
				initialPath.Points[1],
				initialPath.Points[2],
				(int)initialPath.Value,
				settings.MaxIterations,
				cancellationToken);
		}

		public void AddTriangles(
			ICollection<FractalPath> paths,
			Point<double> a,
			Point<double> b,
			Point<double> c,
			int iteration,
			int maxIterations,
			CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}

			paths.Add(new FractalPath(iteration, a, b, c));

			if (++iteration >= maxIterations)
			{
				return;
			}

			var centerA = Geometry.GetLineCenterPoint(b, c);
			var centerB = Geometry.GetLineCenterPoint(a, c);
			var centerC = Geometry.GetLineCenterPoint(a, b);

			this.AddTriangles(paths, centerA, b, centerC, iteration, maxIterations, cancellationToken);
			this.AddTriangles(paths, centerC, a, centerB, iteration, maxIterations, cancellationToken);
			this.AddTriangles(paths, centerB, c, centerA, iteration, maxIterations, cancellationToken);
		}
	}
}
