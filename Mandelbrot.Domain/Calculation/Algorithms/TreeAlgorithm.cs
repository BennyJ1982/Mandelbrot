namespace Mandelbrot.Domain.Calculation.Algorithms
{
	using System;
	using System.Collections.Generic;
	using System.Threading;

	public class TreeAlgorithm : PathBasedAlgorithmBase
	{
		private readonly float branchAngle;

		private readonly int numberOfBranches;

		public TreeAlgorithm(float branchAngle, int numberOfBranches)
		{
			this.numberOfBranches = numberOfBranches;
			this.branchAngle = branchAngle;
		}

		public override IEnumerable<ICalculationSpecification> GetCalculatableParts(IFractalSettings settings, int numberOfParts)
		{
			var initialLine = this.GetInitialLine(settings);

			var parts = new List<FractalPath>();
			this.AddBranches(parts, initialLine.Item1, initialLine.Item2, 0, 2, new CancellationToken());
			return CreateCalculationSpecifications(parts, true);
		}

		protected override string Name => "Tree";

		protected override void CalculatePaths(
			FractalPath initialPath,
			IList<FractalPath> paths,
			IFractalSettings settings,
			CancellationToken cancellationToken)
		{
			this.AddBranches(
				paths,
				initialPath.GetFirstPoint(),
				initialPath.GetSecondPoint(),
				(int)initialPath.Value,
				settings.MaxIterations,
				cancellationToken);
		}

		private void AddBranches(
			ICollection<FractalPath> paths,
			Point<double> source,
			Point<double> destination,
			int iteration,
			int maxIterations,
			CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return;
			}

			paths.Add(new FractalPath(iteration, source, destination));

			if (++iteration >= maxIterations)
			{
				return;
			}

			var baseLength = Geometry.GetLineLength(source, destination);
			var baseAngle = Geometry.GetLineAngle(destination, source);

			var anglePerBranch = this.branchAngle / (this.numberOfBranches - 1);
			var currentAngle = baseAngle - this.branchAngle / 2;

			for (var branch = 0; branch < this.numberOfBranches; branch++)
			{
				var nextDestination = Geometry.GetPointFromAngle(destination, baseLength * 0.66666, currentAngle);
				this.AddBranches(paths, destination, nextDestination, iteration, maxIterations, cancellationToken);
				currentAngle += anglePerBranch;
			}
		}

		private Tuple<Point<double>, Point<double>> GetInitialLine(IFractalSettings settings)
		{
			var xPos1 = (double)this.ScaleX(settings.ScreenWidth / 2m, settings);
			var xPos2 = xPos1;
			var yPos1 = (double)this.ScaleY(settings.ScreenHeight * 0.9m, settings);
			var yPos2 = (double)this.ScaleY(settings.ScreenHeight * 0.6m, settings);

			return Tuple.Create(new Point<double>(xPos1, yPos1), new Point<double>(xPos2, yPos2));
		}
	}
}
