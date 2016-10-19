namespace Mandelbrot.Clients.WinForms.Controls
{
	using System.Collections;
	using System.Windows.Forms;

	/// <summary>
	/// A tool strit combobox that supports binding to a list of objects
	/// </summary>
	public class BindableToolStripComboBox : ToolStripComboBox
	{
		private IEnumerable itemsSource;

		public IEnumerable ItemsSource
		{
			get
			{
				return this.itemsSource;
			}
			set
			{
				this.itemsSource = value;
				this.RefreshItems();
			}
		}

		private void RefreshItems()
		{
			this.Items.Clear();
			foreach (var item in this.itemsSource)
			{
				this.Items.Add(item);
			}

		}
	}
}
