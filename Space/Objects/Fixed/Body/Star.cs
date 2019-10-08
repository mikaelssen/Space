using System;
using Raylib;
using R = Raylib.Raylib;

namespace Space.Objects.Fixed.Body
{
	public class Star : Fixed
	{

		public string Name { get; set; }
		public int Size { get; set; }
		public double Mass { get; set; }

		//TODO Random this a bit more.
		public Star()
		{
			Size = 1391000; //In Km
			Mass = 200000000000000; //In trillion tonnes
			Name = Globals.Globals.GetRandomName();
			Text = Name;
		}

		internal static void Click()
		{
			Console.WriteLine("I am the sun yes");
		}
	}
							 //Arkentosh  Greb		 Cactus	   Quantum	   Entih	  houfnice,Kat		 Mikaelssen Pizzapete rigel	   Coconutsales Zunbil   littleme02
}
