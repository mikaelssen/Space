using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Space.Globals
{
	static public class Globals
	{
		//static public double Date; //In hours
		static public DateTime Date = new DateTime(); //In hours


		public static string GetRandomName()
		{
			int r = Resources.Resources.rng.Next(Resources.Resources.Names.Count);
			return Resources.Resources.Names[r];
		}
		
	}

	
}
