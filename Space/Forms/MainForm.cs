using System;
using System.Windows.Forms;
using Space.Objects;
using Space.Forms;

namespace Space
{
	public partial class MainForm : Form
	{
		SFMLWindow window;
		Planet_overview overview;

		public MainForm()
		{
			InitializeComponent();
		}

		private void GameScreenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			window = new SFMLWindow();
		}

		private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Game.NewGame();
		}

		private void LoadGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Game.LoadGame();
		}

		private void ToolStripButton1_Click(object sender, EventArgs e)
		{
			overview = new Planet_overview();
			overview.Show();
		}
	}
}
