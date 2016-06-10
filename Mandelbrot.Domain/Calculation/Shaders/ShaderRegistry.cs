namespace Mandelbrot.Domain.Calculation.Shaders
{
	using System.Collections.Generic;

	public class ShaderRegistry : IShaderRegistry
	{
		private readonly HashSet<IShader> shaders = new HashSet<IShader>();

		public void RegisterShader(IShader shader)
		{
			this.shaders.Add(shader);
		}

		public IEnumerable<IShader> GetAll()
		{
			return this.shaders;
		}
	}
}