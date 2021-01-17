using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Raylib;
using R = Raylib.Raylib;

namespace Space.Resources
{
	static public class Resources
	{
		static public Random rng = new Random();
		static public List<string> Names = File.ReadAllLines("./Src/Names.txt").ToList();
		static public Font Font = R.LoadFont("./Src/Barrio-Regular.otf");
	}
}
