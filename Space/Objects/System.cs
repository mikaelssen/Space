using System;
using System.Collections.Generic;
using SFML.System;
using LiteDB;
using SFML.Graphics;

namespace Space.Objects
{

	public class Entity
	{
		public Guid Guid { get; } = new Guid();
		public Text Text { get; set; } = new Text("", Resources.Resources.Font);
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
			Text = new Text($"System {Id}", Resources.Resources.Font);
		}

	}

	public class Star : Entity
	{

		public string Name { get; set; }
		public int Size { get; set; }
		public double Mass { get; set; }
		public CircleShape Shape { get; set; }

		//TODO Random this a bit more.
		public Star()
		{
			Size = 1391000; //In Km
			Mass = 200000000000000; //In trillion tonnes
			Shape = new CircleShape()
			{
				FillColor = Color.Yellow,
				Position = new Vector2f(0, 0),
			};
			Name = Names.GetRandomName();
			Text = new Text($"{Name}", Resources.Resources.Font);
		}

		public Drawable GetDrawable()
		{
			float Radius = Size / 5000;
			Shape.Radius = Radius;
			Shape.Origin = new Vector2f(Radius, Radius);
			return Shape;
		}

		internal static void Click()
		{
			Game.UpdateData();
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
		public int[] Position { get; set; }
		public int CurrentSpeed { get; set; }
		public string ShipName { get; set; }
		public CircleShape Shape { get; set; }
		public string Name { get; set; }

		public Ship()
		{
			Position = Array.Empty<int>();
			CurrentSpeed = 0;
			ShipName = string.Empty;
			Shape = new CircleShape();
			Name = $"Ship: {Names.GetRandomName()}";
			Text = new Text($"{Name}", Resources.Resources.Font);
		}
	}

	public class Planet : Entity
	{
		public float[] Position { get; set; }
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public double Mass { get; set; } //Star mass should be about 330 000 times more than planet mass for sun earth ratio 
		public int Density { get; set; }//Object density earth is about 5515, juptier 1326, our moon is 3344 (kg/m^3)
		public string Name { get; set; }
        public List<Moon> Moons { get; set; }
		public double Velocity { get; set; } //In M/s
		public double Bearing { get; set; }
		public float DistanceFromStar { get; set; }
		public byte OrbialDirection { get; set; }
		public CircleShape Shape { get; set; }

		internal void Update()
		{

		}

		internal void Click()
		{
			Velocity *= 2;
		}

		public Planet()
		{
			Position = new float[] { 0, 0 };
			Resources = new List<Resource>();
			Size = 0; //In Km Diameter
			Density = 1300;
			Mass = 600000000; //In trillion tonnes
			Velocity = 30000;
			Bearing = 5000;
			DistanceFromStar = 150000;//in *1000km
			Name = Names.GetRandomName();
			Text = new Text($"{Name}", Space.Resources.Resources.Font);
			Moons = new List<Moon>();
			OrbialDirection = (byte)Game.rng.Next(0, 1);

			Shape = new CircleShape()
			{
				FillColor = Color.Red
			};
		}

		public Drawable GetDrawable()
		{
			float Radius = Size / 2500;
			Shape.Origin = new Vector2f(Radius, Radius);
			Shape.Position = new Vector2f(Position[0], Position[1]);
			Shape.Radius = Radius;
			return Shape;
		}
	}

	public class Moon : Entity
	{
		public float[] Position { get; set; }
		public float DistanceFromPlanet { get; set; }
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public int Density { get; set; }//Object density earth is about 5515, juptier 1326, our moon is 3344 (kg/m^3)
		public double Mass { get; set; }
		public string Name { get; set; }
		public double Bearing { get; internal set; }
		public double Velocity { get; internal set; }
		public byte OrbialDirection { get; internal set; }
		public CircleShape Shape { get; set; }

		public Moon()
		{
			Position = new float[] { 0, 0 };
			Resources = new List<Resource>();
			Velocity = 300000;
			Bearing = 5000;
			Density = 3344;
			Mass = 70000000; //In trillion tonnes
			Size = 0;
			Name = Names.GetRandomName();
			Text = new Text($"{Name}", Space.Resources.Resources.Font);

			OrbialDirection = (byte)Game.rng.Next(0, 1);
			Shape = new CircleShape()
			{
				FillColor = Color.Blue
			};
		}

		public Drawable GetDrawable(float MoonRadius)
		{
			Shape.Origin = new Vector2f(MoonRadius, MoonRadius);
			Shape.Radius = MoonRadius;
			Shape.Position = new Vector2f(Position[0], Position[1]);
			return Shape;
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
		public int[] Position { get; set; }
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public string Name { get; set; }
		public CircleShape Shape { get; set; }

		public Asteroid()
		{
			Position = Array.Empty<int>();
			Resources = new List<Resource>();
			Size = 0;
			Name = string.Empty;
			Shape = new CircleShape();
		}
	}

	public class JumpPoint : Entity
	{
		[BsonId]
		public Int32 Id { get; set; }

		public int[] Position { get; set; }
		public string Name { get; set; }
		public Int32[] DestinationIDs { get; set; }
		public CircleShape Shape { get; set; }
		//TODO Implement jump point Discovery, somewhere else ofc

		public JumpPoint()
		{
			Position = Array.Empty<int>();
			Name = string.Empty;
			DestinationIDs = Array.Empty<Int32>();
			Shape = new CircleShape();
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
