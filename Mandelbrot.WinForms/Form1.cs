namespace Mandelbrot.Clients.WinForms
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Calculation;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Jobs;
	using Mandelbrot.Domain.Rendering.Output;
	using Mandelbrot.Domain.Rendering.Shaders;

	public partial class Form1 : Form
	{
		private const int NumberOfSectors = 8;
		private readonly IAlgorithmRegistry algorithmRegistry;

		private readonly IShaderRegistry shaderRegistry;

		private readonly IJobFactory jobFactory;

		private readonly IJobRunner jobRunner;

		private Rectangle<decimal> currentFractalRect;

		private IJobResult lastResult;

		private CancellationTokenSource cancellationTokenSource;

		public Form1(IAlgorithmRegistry algorithmRegistry, IShaderRegistry shaderRegistry, IJobFactory jobFactory, IJobRunner jobRunner)
		{
			this.algorithmRegistry = algorithmRegistry;
			this.shaderRegistry = shaderRegistry;
			this.jobFactory = jobFactory;
			this.jobRunner = jobRunner;

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
			this.Reset();
		}

		private void Reset()
		{
			this.currentFractalRect = ((IFractalAlgorithm)this.fractalComboBox.SelectedItem).DefaultScale;
		}

		private async Task DrawAsync()
		{
			this.cancellationTokenSource = new CancellationTokenSource();
			this.UpdateToolbarState();

			try
			{
				var shader = (IShader)this.shaderComboBox.SelectedItem;
				var job = this.CreateJob(this.GetCurrentFractalSettings(), shader);
				this.lastResult = await this.jobRunner.ExecuteAsync(job, this.Screen, 8, this.cancellationTokenSource.Token);
			}
			catch (OperationCanceledException)
			{
				// do nothing
			}
			finally
			{
				this.cancellationTokenSource = null;
				this.UpdateToolbarState();
			}
		}

		private FractalSettings GetCurrentFractalSettings()
		{
			var iterations = Convert.ToInt32(this.iterationsComboBox.SelectedItem.ToString());
			var algorithm = (IFractalAlgorithm)this.fractalComboBox.SelectedItem;
			return new FractalSettings(algorithm, this.Screen.Width, this.Screen.Height, this.currentFractalRect, iterations);
		}

		private IJob CreateJob(IFractalSettings settings, IShader shader)
		{
			if (this.lastResult != null && !this.lastResult.Cancelled && this.lastResult.Job.Settings.Equals(settings))
			{
				return this.jobFactory.CreateFromResult(this.lastResult, settings, shader, NumberOfSectors);
			}

			return this.jobFactory.Create(settings, shader, NumberOfSectors);
		}

		private async void OnDraw(object sender, EventArgs e)
		{
			await this.DrawAsync();
		}

		private void OnCancel(object sender, EventArgs e)
		{
			this.cancellationTokenSource.Cancel();
		}

		private async void OnReset(object sender, EventArgs e)
		{
			this.Reset();
			await this.DrawAsync();
		}

		private void OnFractalSelectionChanged(object sender, EventArgs e)
		{
			this.Reset();
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

			this.currentFractalRect = this.Screen.ScaleFractalRectToScreenSelection(this.currentFractalRect, screenSelection);
			await this.DrawAsync();

		}

		private void UpdateToolbarState()
		{
			var isDrawing = this.cancellationTokenSource != null;

			this.drawButton.Enabled = !isDrawing;
			this.resetButton.Enabled = !isDrawing;
			this.fractalComboBox.Enabled = !isDrawing;
			this.shaderComboBox.Enabled = !isDrawing;
			this.iterationsComboBox.Enabled = !isDrawing;
			this.cancelButton.Enabled = isDrawing;
		}
	}
}
