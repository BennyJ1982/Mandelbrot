namespace Mandelbrot.Domain.Rendering.Shaders
{
	using System.Collections.Generic;

	public interface IShaderRegistry
	{
		void RegisterShader(IShader shader);

		IEnumerable<IShader> GetAll();
	}
}
