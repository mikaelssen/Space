namespace Space
{
	partial class MainForm
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
			if (disposing && (components != null))
			{
				components.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(579, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.loadGameToolStripMenuItem,
            this.gameScreenToolStripMenuItem,
            this.editorToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(47, 22);
			this.toolStripDropDownButton1.Text = "Main";
			// 
			// newGameToolStripMenuItem
			// 
			this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
			this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newGameToolStripMenuItem.Text = "New Game";
			this.newGameToolStripMenuItem.Click += new System.EventHandler(this.NewGameToolStripMenuItem_Click);
			// 
			// loadGameToolStripMenuItem
			// 
			this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
			this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.loadGameToolStripMenuItem.Text = "Load Game";
			this.loadGameToolStripMenuItem.Click += new System.EventHandler(this.LoadGameToolStripMenuItem_Click);
			// 
			// gameScreenToolStripMenuItem
			// 
			this.gameScreenToolStripMenuItem.Name = "gameScreenToolStripMenuItem";
			this.gameScreenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.gameScreenToolStripMenuItem.Text = "GameScreen";
			this.gameScreenToolStripMenuItem.Click += new System.EventHandler(this.GameScreenToolStripMenuItem_Click);
			// 
			// editorToolStripMenuItem
			// 
			this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
			this.editorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.editorToolStripMenuItem.Text = "Editor";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(49, 22);
			this.toolStripLabel1.Text = "Settings";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(61, 22);
			this.toolStripButton1.Text = "OverView";
			this.toolStripButton1.ToolTipText = "d";
			this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 23);
			this.Controls.Add(this.toolStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.Text = "Main";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem gameScreenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadGameToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
	}
}

