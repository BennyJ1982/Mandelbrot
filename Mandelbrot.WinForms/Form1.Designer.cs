namespace Mandelbrot.Clients.WinForms
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.Screen = new Mandelbrot.Clients.WinForms.PictureBoxScreen();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.drawButton = new System.Windows.Forms.ToolStripButton();
			this.cancelButton = new System.Windows.Forms.ToolStripButton();
			this.resetButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.fractalComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.shaderComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.iterationsComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Screen)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.Screen, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(937, 561);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// Screen
			// 
			this.Screen.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Screen.Location = new System.Drawing.Point(3, 28);
			this.Screen.Name = "Screen";
			this.Screen.Size = new System.Drawing.Size(931, 530);
			this.Screen.TabIndex = 1;
			this.Screen.TabStop = false;
			this.Screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnScreenMouseUp);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawButton,
            this.cancelButton,
            this.resetButton,
            this.toolStripSeparator1,
            this.toolStripLabel3,
            this.fractalComboBox,
            this.toolStripLabel1,
            this.shaderComboBox,
            this.toolStripLabel2,
            this.iterationsComboBox});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(937, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// drawButton
			// 
			this.drawButton.Image = ((System.Drawing.Image)(resources.GetObject("drawButton.Image")));
			this.drawButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.drawButton.Name = "drawButton";
			this.drawButton.Size = new System.Drawing.Size(54, 22);
			this.drawButton.Text = "Draw";
			this.drawButton.Click += new System.EventHandler(this.OnDraw);
			// 
			// cancelButton
			// 
			this.cancelButton.Enabled = false;
			this.cancelButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelButton.Image")));
			this.cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(63, 22);
			this.cancelButton.Text = "Cancel";
			this.cancelButton.Click += new System.EventHandler(this.OnCancel);
			// 
			// resetButton
			// 
			this.resetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.resetButton.Image = ((System.Drawing.Image)(resources.GetObject("resetButton.Image")));
			this.resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(39, 22);
			this.resetButton.Text = "Reset";
			this.resetButton.Click += new System.EventHandler(this.OnReset);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(45, 22);
			this.toolStripLabel3.Text = "Fractal:";
			// 
			// fractalComboBox
			// 
			this.fractalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fractalComboBox.Name = "fractalComboBox";
			this.fractalComboBox.Size = new System.Drawing.Size(121, 25);
			this.fractalComboBox.SelectedIndexChanged += new System.EventHandler(this.OnFractalSelectionChanged);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(46, 22);
			this.toolStripLabel1.Text = "Shader:";
			// 
			// shaderComboBox
			// 
			this.shaderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.shaderComboBox.Name = "shaderComboBox";
			this.shaderComboBox.Size = new System.Drawing.Size(121, 25);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
			this.toolStripLabel2.Text = "Iterations";
			// 
			// iterationsComboBox
			// 
			this.iterationsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iterationsComboBox.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "100",
            "200",
            "500",
            "1000",
            "5000",
            "10000"});
			this.iterationsComboBox.Name = "iterationsComboBox";
			this.iterationsComboBox.Size = new System.Drawing.Size(121, 25);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(937, 561);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Screen)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private PictureBoxScreen Screen;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton drawButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox shaderComboBox;
		private System.Windows.Forms.ToolStripButton resetButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripComboBox iterationsComboBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripComboBox fractalComboBox;
		private System.Windows.Forms.ToolStripButton cancelButton;
	}
}

