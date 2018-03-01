using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFML.Graphics;
using SFML.Window;
using SFML;
using SFML.System;
using Space.Objects;

namespace Space
{
	public partial class MainForm : Form
	{
		SFMLWindow window;

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
	}
}
