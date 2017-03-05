namespace Mandelbrot.Clients.Console
{
	using Mandelbrot.Clients.Main;
    using Mandelbrot.Domain.Rendering.Output;
    using Ninject;

	internal class Program
	{
		private static IKernel ninjectKernel;

		private static ConsoleRenderer renderer;

		private static void Main(string[] args)
		{
			ninjectKernel = CreateKernel();

			System.Console.WindowWidth = 200;
			System.Console.WindowHeight = 80;
			System.Console.CursorSize = 100;

			renderer = ninjectKernel.Get<ConsoleRenderer>();
			renderer.Render();

			var inputController = ninjectKernel.Get<InputController>();
			inputController.OnEnterPressed += OnZoomIn;
			inputController.EnterLoop();
		}

		private static async void OnZoomIn(object sender, System.EventArgs e)
		{
			var screen = ninjectKernel.Get<ConsoleScreen>();
			var frameWidth = screen.Width / 4;
			var frameHeight = screen.Height / 4;

			await renderer.ZoomIn(screen.CursorLeft, screen.CursorTop, frameWidth, frameHeight);
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel(new DefaultDependencyModule());
			kernel.Bind<ConsoleDrawingContext>().ToSelf().InSingletonScope();
            kernel.Bind<ConsoleScreen, IScreen>().To<ConsoleScreen>().InSingletonScope();
			kernel.Bind<InputController>().ToSelf().InSingletonScope();
            kernel.Bind<ConsoleRenderer>().ToSelf().InSingletonScope();

            return kernel;
		}
	}
}
