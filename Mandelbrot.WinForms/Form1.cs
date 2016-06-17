namespace Mandelbrot.Clients.WinForms
{
	using System;
	using System.Windows.Forms;
	using Mandelbrot.Clients.Main;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Rendering.Shaders;

	public partial class Form1 : Form
	{
		private readonly IAlgorithmRegistry algorithmRegistry;

		private readonly IShaderRegistry shaderRegistry;

		private readonly IApplicationContext context;

		public Form1(IAlgorithmRegistry algorithmRegistry, IShaderRegistry shaderRegistry, IApplicationContextFactory contextFactory)
		{
			this.algorithmRegistry = algorithmRegistry;
			this.shaderRegistry = shaderRegistry;

			this.context = contextFactory.Create();
			this.context.StatusChanged += this.OnStatusChanged;

			this.InitializeComponent();
			this.InitializeOptions();
		}

		private void InitializeOptions()
		{
			foreach (var algorithm in this.algorithmRegistry.GetAll())
			{
				this.fractalComboBox.Items.Add(algorithm);
			}

			foreach (var shader in this.shaderRegistry.GetAll())
			{
				this.shaderComboBox.Items.Add(shader);
			}

			this.fractalComboBox.SelectedIndex = 0;
			this.shaderComboBox.SelectedIndex = 0;
			this.iterationsComboBox.SelectedIndex = 1;
		}

		private async void OnDraw(object sender, EventArgs e)
		{
			await this.context.DrawFractalAsync(this.Screen);
		}

		private void OnCancel(object sender, EventArgs e)
		{
			this.context.Cancel();
		}

		private async void OnReset(object sender, EventArgs e)
		{
			this.context.ResetFractalRect();
			await this.context.DrawFractalAsync(this.Screen);
		}

		private async void OnScreenMouseUp(object sender, MouseEventArgs e)
		{
			var frameWidth = this.Screen.Width / 4;
			var frameHeight = this.Screen.Height / 4;

			var screenSelection = new Rectangle<int>(
				e.X - frameWidth / 2,
				e.Y - frameHeight / 2,
				e.X + frameWidth / 2 - 1,
				e.Y + frameHeight / 2 - 1);

			await this.context.ZoomInAsync(this.Screen, screenSelection);
		}

		private void OnStatusChanged(object sender, StatusEventArgs e)
		{
			var isDrawing = e.IsCalculating;

			this.drawButton.Enabled = !isDrawing;
			this.resetButton.Enabled = !isDrawing;
			this.fractalComboBox.Enabled = !isDrawing;
			this.shaderComboBox.Enabled = !isDrawing;
			this.iterationsComboBox.Enabled = !isDrawing;
			this.cancelButton.Enabled = isDrawing;
		}

		private void OnFractalSelectionChanged(object sender, EventArgs e)
		{
			this.context.CurrentAlgorithm = (IFractalAlgorithm)this.fractalComboBox.SelectedItem;
			this.context.ResetFractalRect();
		}
		
		private void OnShaderChanged(object sender, EventArgs e)
		{
			this.context.CurrentShader = (IShader)this.shaderComboBox.SelectedItem;
		}

		private void OnIterationsChanged(object sender, EventArgs e)
		{
			this.context.MaxIterations = Convert.ToInt32(this.iterationsComboBox.SelectedItem.ToString()); 
		}
	}
}
