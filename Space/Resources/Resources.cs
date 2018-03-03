using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using SFML.Graphics;

namespace Space.Resources
{
	static public class Resources
	{

		static public Random rng = new Random();
		static public List<string> Names = File.ReadAllLines("./Resources/Names.txt").ToList();
		static public Font Font = new Font("./Resources/Barrio-Regular.otf");

	}
}
