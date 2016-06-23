namespace Mandelbrot.UI.Actions
{
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Rendering.Shaders;

	public interface IParameterActionFactory
	{
		IAction CreateChangeAlgorithmAction(IApplicationContext context, IFractalAlgorithm algorithm);

		IAction CreateChangeShaderAction(IApplicationContext context, IShader shader);

		IAction CreateChangeIterationsAction(IApplicationContext context, int iterations);
	}
}
