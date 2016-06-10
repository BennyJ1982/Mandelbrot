﻿namespace Mandelbrot.Clients.Main
{
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Calculation.Shaders;
	using Mandelbrot.Domain.Jobs;
	using Mandelbrot.Domain.Rendering;
	using Mandelbrot.Domain.Rendering.Specifications;
	using Ninject;
	using Ninject.Activation;
	using Ninject.Modules;
	
	public class DefaultDependencyModule : NinjectModule
	{
		public override void Load()
		{
			this.Bind<IAlgorithmRegistry>().To<AlgorithmRegistry>().InSingletonScope().OnActivation(RegisterFractalAlgorithms);
			this.Bind<ICalculatorRegistry>().To<CalculatorRegistry>().InSingletonScope().OnActivation(RegisterCalculators);
			this.Bind<IShaderRegistry>().To<ShaderRegistry>().InSingletonScope().OnActivation(RegisterShaders);
			this.Bind<IRendererRegistry>().To<RendererRegistry>().InSingletonScope().OnActivation(RegisterRenderers);

			this.Bind<IJobFactory>().To<JobFactory>().InSingletonScope();
			this.Bind<IJobRunner>().To<ParallelForEachBasedRunner>().InSingletonScope();
			this.Bind<IFractalCalculator>().To<PointBasedFractalCalculator>().InSingletonScope();

			this.Bind<IRenderSpecificationFactory>().To<RenderSpecificationFactory>();
			this.Bind<IBitmapWriter>().To<BitmapWriter>().InSingletonScope();
			this.Bind<IRenderer>().To<ShadingRenderer>().InSingletonScope();
			this.Bind<IRenderer>().To<CalculatingRenderer>().InSingletonScope();
		}

		private static void RegisterFractalAlgorithms(IAlgorithmRegistry registry)
		{
			registry.RegisterFractalAlgorithm(new MandelbrotAlgorithm());
			registry.RegisterFractalAlgorithm(new JuliaAlgorithm());
			registry.RegisterFractalAlgorithm(new TreeAlgorithm(90, 6));
			registry.RegisterFractalAlgorithm(new SierpinskiTriangleAlgorithm(30));
		}

		private static void RegisterCalculators(ICalculatorRegistry registry)
		{
			registry.RegisterFractalCalculator(new PointBasedFractalCalculator());
			registry.RegisterFractalCalculator(new LineBasedFractalCalculator());
		}

		private static void RegisterShaders(IShaderRegistry registry)
		{
			registry.RegisterShader(new DefaultEscapeTimeShader());
			registry.RegisterShader(new FireShader());
			registry.RegisterShader(new SimplestShaderEver());
		}

		private static void RegisterRenderers(IContext context, IRendererRegistry registry)
		{
			foreach (var renderer in context.Kernel.GetAll<IRenderer>())
			{
				registry.RegisterRenderer(renderer);
			}
		}
	}
}
