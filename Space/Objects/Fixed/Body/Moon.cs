using System.Collections.Generic;
using Raylib;
using R = Raylib.Raylib;
using Space.Logic;
using Space.Managers;

namespace Space.Objects.Fixed.Body
{
	public class Moon : Fixed
	{
		public float DistanceFromPlanet { get; set; }
        public double Circumference { get; set; }
        public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public int Density { get; set; }//Object density earth is about 5515, juptier 1326, our moon is 3344 (kg/m^3)
		public double Mass { get; set; }
		public string Name { get; set; }
		public double Bearing { get; internal set; }
        public double BearingDV { get; set; }
        public double Velocity { get; internal set; }
		public byte OrbialDirection { get; internal set; }

		public Moon()
		{
			Position = new Vector2(0,0);
			Resources = new List<Resource>();
			Velocity = 300000;
			Bearing = 5000;
			Density = 3344;
			Mass = 70000000; //In trillion tonnes
			Size = 0;
			Name = Globals.Globals.GetRandomName();
			Text = Name;

			OrbialDirection = (byte)SystemManager.rng.Next(0, 1);
			
		}

		internal void Click()
		{
			Velocity *= 2;
		}
	}
}
