namespace Mandelbrot.Clients.WinForms
{
	using System;
	using System.Windows.Forms;
	using Mandelbrot.Clients.Main;
	using Ninject;

	static class Program
	{
		private static IKernel ninjectKernel;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ninjectKernel = CreateKernel();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(ninjectKernel.Get<Form1>());
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel(new DefaultDependencyModule());
			return kernel;
		}
	}
}
