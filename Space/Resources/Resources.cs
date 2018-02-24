using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;

namespace Space.Resources
{
	static class Resources
	{

		public static Random rng = new Random();
		static public List<string> Names = File.ReadAllLines("./Resources/Names.txt").ToList();
		static public Font font = new Font("./Resources/Barrio-Regular.otf");

	}
}
