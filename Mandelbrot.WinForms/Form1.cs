namespace Mandelbrot.Clients.WinForms
{
	using System;
	using System.Windows.Forms;
	using Mandelbrot.Domain;
	using Mandelbrot.Domain.Calculation.Algorithms;
	using Mandelbrot.Domain.Fascade;
	using Mandelbrot.Domain.Rendering.Shaders;
	using Mandelbrot.UI.Actions;

	public partial class Form1 : Form
	{
		private readonly IAlgorithmRegistry algorithmRegistry;

		private readonly IShaderRegistry shaderRegistry;

		private readonly IUndoStack undoStack;

		private readonly IParameterActionFactory parameterActionFactory;

		private readonly IFractalContext context;

		public Form1(
			IAlgorithmRegistry algorithmRegistry,
			IShaderRegistry shaderRegistry,
			IFractalContextFactory contextFactory,
			IUndoStack undoStack,
			IParameterActionFactory parameterActionFactory)
		{
			this.algorithmRegistry = algorithmRegistry;
			this.shaderRegistry = shaderRegistry;
			this.undoStack = undoStack;
			this.parameterActionFactory = parameterActionFactory;
			this.undoStack.StackChanged += this.OnUndoSackChanged;

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
			var drawAction = new DrawAction(this.context, this.Screen);
			await drawAction.DoAsync();
			this.undoStack.PushIfPossibe(drawAction);
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

			var zoomInAction = new ZoomInAction(this.context, screenSelection, this.Screen);
			await zoomInAction.DoAsync();
			this.undoStack.PushIfPossibe(zoomInAction);
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

		private async void OnUndo(object sender, EventArgs e)
		{
			await this.undoStack.UndoLatest();
		}

		private async void OnRedo(object sender, EventArgs e)
		{
			await this.undoStack.RedoLatest();
		}

		private void OnUndoSackChanged(object sender, EventArgs e)
		{
			this.undoButton.Enabled = this.undoStack.CanUndo();
			this.redoButton.Enabled = this.undoStack.CanRedo();
		}

		private async void OnFractalSelectionChanged(object sender, EventArgs e)
		{
			var action = this.parameterActionFactory.CreateChangeAlgorithmAction(this.context, (IFractalAlgorithm)this.fractalComboBox.SelectedItem);
			await action.DoAsync();
			this.undoStack.PushIfPossibe(action);
			this.context.ResetFractalRect();
		}
		
		private async void OnShaderChanged(object sender, EventArgs e)
		{
			var action = this.parameterActionFactory.CreateChangeShaderAction(this.context, (IShader)this.shaderComboBox.SelectedItem);
			await action.DoAsync();
			this.undoStack.PushIfPossibe(action);
		}

		private async void OnIterationsChanged(object sender, EventArgs e)
		{
			var action = this.parameterActionFactory.CreateChangeIterationsAction(
				this.context,
				Convert.ToInt32(this.iterationsComboBox.SelectedItem.ToString()));
			await action.DoAsync();
			this.undoStack.PushIfPossibe(action);
		}
	}
}
