namespace Mandelbrot.UI.Actions
{
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Rendering.Shaders;

	public class ParameterActionFactory : IParameterActionFactory
	{
		public IAction CreateChangeAlgorithmAction(IApplicationContext context, IFractalAlgorithm algorithm)
		{
			return new ChangeParameterSelectionAction<IFractalAlgorithm>(context, c => c.CurrentAlgorithm, algorithm);
		}

		public IAction CreateChangeShaderAction(IApplicationContext context, IShader shader)
		{
			return new ChangeParameterSelectionAction<IShader>(context, c => c.CurrentShader, shader);
		}

		public IAction CreateChangeIterationsAction(IApplicationContext context, int iterations)
		{
			return new ChangeParameterSelectionAction<int>(context, c => c.MaxIterations, iterations);
		}
	}
}
