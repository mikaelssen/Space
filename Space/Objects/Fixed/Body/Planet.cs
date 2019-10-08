using System.Collections.Generic;
using Raylib;
using R = Raylib.Raylib;
using Space.Logic;
using Space.Managers;

namespace Space.Objects.Fixed.Body
{
	public class Planet : Fixed
	{
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public double Mass { get; set; } //Star mass should be about 330 000 times more than planet mass for sun earth ratio 
		public int Density { get; set; }//Object density earth is about 5515, juptier 1326, our moon is 3344 (kg/m^3)
		public string Name { get; set; }
        public List<Moon> Moons { get; set; }
		public double Velocity { get; set; } //In M/s
		public double Bearing { get; set; }
        public double BearingDV { get; set; }
        public float DistanceFromStar { get; set; }
        public double Circumference { get; set; }
        public byte OrbialDirection { get; set; }

		internal void Update()
		{

		}

		internal void Click()
		{
			Velocity *= 2;
		}

		public Planet()
		{
			Position = new Vector2(0,0);
			Resources = new List<Resource>();
			Size = 0; //In Km Diameter
			Density = 1300;
			Mass = 600000000; //In trillion tonnes
			Velocity = 30000;
			Bearing = 5000;
			DistanceFromStar = 150000;//in *1000km
			Text = Name = Globals.Globals.GetRandomName();
			Moons = new List<Moon>();
			OrbialDirection = (byte)SystemManager.rng.Next(0, 1);
		}
	}
}
