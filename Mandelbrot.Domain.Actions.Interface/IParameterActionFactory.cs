namespace Mandelbrot.UI.Actions
{
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Fascade;
	using Mandelbrot.Domain.Rendering.Shaders;

	public interface IParameterActionFactory
	{
		IAction CreateChangeAlgorithmAction(IFractalContext context, IFractalAlgorithm algorithm);

		IAction CreateChangeShaderAction(IFractalContext context, IShader shader);

		IAction CreateChangeIterationsAction(IFractalContext context, int iterations);
	}
}
