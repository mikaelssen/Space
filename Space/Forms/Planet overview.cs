using Space.Objects;
using System.Windows.Forms;

namespace Space.Forms
{
	public partial class Planet_overview : Form
	{

		public Planet_overview()
		{
			InitializeComponent();
		}

		private void Planet_overview_Load(object sender, System.EventArgs e)
		{
			foreach (var Sys in Game.Systems)
			{
				TreeNode Systemnode = new TreeNode("System ID: " + Sys.Id)
				{
					Tag = Sys
				};

				TreeNode planets = new TreeNode("Planets");

				foreach (var planet in Sys.Planets)
				{
					TreeNode Nodeplanet = new TreeNode(planet.Name)
					{
						Tag = planet
					};

					TreeNode moons = new TreeNode("Moons");

					foreach (var moon in planet.Moons)
					{
						TreeNode themoon = new TreeNode(moon.Name)
						{
							Tag = moon
						};
						moons.Nodes.Add(themoon);
					}

					Nodeplanet.Nodes.Add(moons);
					planets.Nodes.Add(Nodeplanet);
				}

				Systemnode.Nodes.Add(planets);
				treeView1.Nodes.Add(Systemnode);

			}
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag != null)
			{
				if (e.Node.Tag.GetType() == typeof(Moon))
				{

					Moon moon = (Moon)e.Node.Tag;

					dataGridView1.Columns.Clear();
					dataGridView1.Columns.Add("Moon", "Name");
					dataGridView1.Columns.Add("Moon", "Population");
					dataGridView1.Columns.Add("Moon", "Size");
					dataGridView1.Columns.Add("Moon", "Mass");
					dataGridView1.Columns.Add("Moon", "Owner");
					dataGridView1.Columns.Add("Moon", "Guid");
					dataGridView1.Rows.Add(new string[] { moon.Name, string.Format("{0:0}", moon.Population), "" + moon.Size, "" + moon.Mass, "" + moon.Owner, "" + moon.Guid });

					System.Diagnostics.Debug.WriteLine(moon.Name);
				}

				if (e.Node.Tag.GetType() == typeof(Planet))
				{
					Planet planet = (Planet)e.Node.Tag;

					dataGridView1.Columns.Clear();
					dataGridView1.Columns.Add("Planet", "Name");
					dataGridView1.Columns.Add("Planet", "Population");
					dataGridView1.Columns.Add("Planet", "Size");
					dataGridView1.Columns.Add("Planet", "Mass");
					dataGridView1.Columns.Add("Planet", "Owner");
					dataGridView1.Columns.Add("Planet", "Guid");
					dataGridView1.Rows.Add(new string[] { planet.Name, string.Format("{0:0}", planet.Population), "" + planet.Size, "" + planet.Mass, "" + planet.Owner, "" + planet.Guid });

					System.Diagnostics.Debug.WriteLine(planet.Name);
				}

				if (e.Node.Tag.GetType() == typeof(SolarSystem))
				{
					SolarSystem sys = (SolarSystem)e.Node.Tag;
					System.Diagnostics.Debug.WriteLine(sys.Guid);
				}

				if (e.Node.Text == "Planets")
				{
					string s = (string)e.Node.Tag;


					dataGridView1.Columns.Clear();
					dataGridView1.Columns.Add("Planet", "Name");
					dataGridView1.Columns.Add("Planet", "Population");
					dataGridView1.Columns.Add("Planet", "Size");
					dataGridView1.Columns.Add("Planet", "Mass");
					dataGridView1.Columns.Add("Planet", "Owner");
					dataGridView1.Columns.Add("Planet", "Guid");

					if (s == "planets")
					{
						foreach (var system in Game.Systems)
						{
							foreach (var planet in system.Planets)
							{
								dataGridView1.Rows.Add(new string[] { planet.Name, string.Format("{0:0}", planet.Population), "" + planet.Size, "" + planet.Mass, "" + planet.Owner, "" + planet.Guid });

							}
						}
					}
				}

			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
