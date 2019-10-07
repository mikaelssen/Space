using System;
using System.Collections.Generic;
using LiteDB;
using Raylib;
using R = Raylib.Raylib;

namespace Space.Objects
{

	public class Entity
	{
		public Vector2 Position;
		public Guid Guid { get; } = new Guid();
		public string Text { get; set; } = "";

		public override string ToString()
		{
			return $"Text : ID [{Guid}]";
		}

		public virtual void Draw() { }
	}


	public class SolarSystem : Entity
	{
		public Int32 Id { get; set; }
		public List<JumpPoint> JumpPoints { get; set; }
		public List<Asteroid> Asteroids { get; set; }
		public List<Planet> Planets { get; set; }
		public List<Ship> Ships { get; set; }
		public List<Structure> Structures { get; set; }
		public Star Star { get; set; }


		public SolarSystem()
		{
			Star = new Star();
			Planets = new List<Planet>();
			Asteroids = new List<Asteroid>();
			Ships = new List<Ship>();
			Structures = new List<Structure>();
			JumpPoints = new List<JumpPoint>();
			Text = $"System";
		}

	}

	public class Star : Entity
	{

		public string Name { get; set; }
		public int Size { get; set; }
		public double Mass { get; set; }

		//TODO Random this a bit more.
		public Star()
		{
			Size = 1391000; //In Km
			Mass = 200000000000000; //In trillion tonnes
			Name = Names.GetRandomName();
			Text = Name;
		}

		public override void Draw()
		{
			float Radius = Size / 5000;
			R.DrawCircle(0, 0, Radius, Color.YELLOW);
		}

		internal static void Click()
		{
			Console.WriteLine("I am the sun yes");
		}
	}

	public class Structure : Entity
	{
		public Dictionary<Resource, int> Price { get; set; }
		public Effect Effect { get; set; }

		public Structure()
		{
			Price = new Dictionary<Resource, int>();
			Effect = new Effect();
		}
	}

	public class Effect : Entity
	{
		//TODO write effects
	}

	public class Ship : Entity
	{
		public int CurrentSpeed { get; set; }
		public string ShipName { get; set; }
		public string Name { get; set; }

		public Ship()
		{
			Position = new Vector2();
			CurrentSpeed = 0;
			ShipName = string.Empty;
			Name = $"Ship: {Names.GetRandomName()}";
			Text = Name;
		}


	}

	public class Planet : Entity
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
			Text = Name = Names.GetRandomName();
			Moons = new List<Moon>();
			OrbialDirection = (byte)SystemManager.rng.Next(0, 1);
		}

		public override void Draw()
		{
			float Radius = Size / 2500;
			R.DrawCircle((int)Position.x, (int)Position.y, Radius, Color.BLUE);
		}
	}

	public class Moon : Entity
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
			Name = Names.GetRandomName();
			Text = Name;

			OrbialDirection = (byte)SystemManager.rng.Next(0, 1);
			
		}

		public override void Draw()
		{
            float Radius = Size / 50;
            R.DrawCircle((int)Position.x, (int)Position.y, Radius, Color.GREEN);
		}

		internal void Click()
		{
			Velocity *= 2;
		}
	}

	public class Resource : Entity
	{
		public RESOURCETYPE Resourcename { get; set; }
		public int Quantity { get; set; }
		public float Acsessibility { get; set; }

		#region Resource Maths, allows for direct manipulation of objects

		public static Resource operator +(Resource a, Resource b)
		{
			a.Quantity += b.Quantity;
			return a;
		}

		public static Resource operator *(Resource a, Resource b)
		{
			a.Quantity *= b.Quantity;
			if (a.Quantity <= 0) //can't be less than 0, also prevents negative multiplication?
				a.Quantity = 0;
			return a;
		}

		public static Resource operator /(Resource a, Resource b)
		{
			a.Quantity /= b.Quantity;
			if (a.Quantity <= 0) //can't be less than 0
				a.Quantity = 0;
			return a;
		}

		public static Resource operator -(Resource a, Resource b)
		{
			a.Quantity -= b.Quantity;
			if (a.Quantity <= 0)
				a.Quantity = 0;
			return a;
		}

		#endregion

		public Resource()
		{
			Resourcename = 0;
			Quantity = 0;
			Acsessibility = 0;
		}
	}

	public class Asteroid : Entity
	{
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public string Name { get; set; }

		public Asteroid()
		{
			Position = new Vector2();
			Resources = new List<Resource>();
			Size = 0;
			Name = string.Empty;
		}
	}

	public class JumpPoint : Entity
	{
		[BsonId]
		public Int32 Id { get; set; }

		public string Name { get; set; }
		public Int32[] DestinationIDs { get; set; }

		//TODO Implement jump point Discovery, somewhere else ofc

		public JumpPoint()
		{
			Position = new Vector2();
			Name = string.Empty;
			DestinationIDs = Array.Empty<Int32>();
		}
	}

	/// <summary>
	/// Name class, also got function for fetching a name
	/// </summary>
	public class Names 
	{
		public string Name { get; set; }

		public static string GetRandomName()
		{
			int r = Resources.Resources.rng.Next(Resources.Resources.Names.Count);
			return Resources.Resources.Names[r];
		}

		public Names()
		{
			Name = string.Empty;
		}
	}



	public enum RESOURCETYPE { Critonite, Arkminium, Greberium, Cactuium, Quantinite, Entihnite, Houfium, Katerium, Mikerium, Peterium, Rigelium, Nuterite, zunbillium }
							//Crito		Arkentosh  Greb		  Cactus	Quantum		Entih	   houfnice Kat		  Mikaelssen Pizzapete rigel	Coconutsales Zunbil
}
