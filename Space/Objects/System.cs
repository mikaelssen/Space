using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using LiteDB;

namespace Space.Objects
{
	
	
	class SolarSystem
	{
		public Int32 Id { get; set; }
		public List<JumpPoint> JumpPoints {get; set;}
		public List<Asteroid> Asteroids { get; set; }
		public List<Planet> Planets { get; set; }
		public List<Ship> Ships { get; set; }
		public List<Structure> Structures { get; set; }

		public SolarSystem()
		{
			Planets = new List<Planet>();
			Asteroids = new List<Asteroid>();
			Ships = new List<Ship>();
			Structures = new List<Structure>();
			JumpPoints = new List<JumpPoint>();
		}
	}

	public class Structure
	{
		public Dictionary<Resource,int> Price { get; set; }
		public Effect Effect { get; set; }

		public Structure()
		{
			Price = new Dictionary<Resource, int>();
			Effect = new Effect();
		}
	}

	public class Effect
	{
		//TODO write effects
	}

	public class Ship
	{
		public int[] Position { get; set; }
		public int CurrentSpeed { get; set; }
		public string ShipName { get; set; }
		public Ship()
		{
			Position =  Array.Empty<int>();
			CurrentSpeed = 0;
			ShipName = string.Empty;
		}
	}

	public class Planet
	{
		public int[] Position { get; set; }
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public string Name { get; set; }
		public List<Moon> Moons { get; set; }

		public Planet()
		{
			Position = Array.Empty<int>();
			Resources = new List<Resource>();
			Size = 0;
			Name = string.Empty;
			Moons = new List<Moon>();
		}
	}

	public class Moon
	{
		public int[] Position { get; set; }
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public string Name { get; set; }

		public Moon()
		{
			Position = Array.Empty<int>();
			Resources = new List<Resource>();
			Size = 0;
			Name = string.Empty;
		}
	}

	public class Resource
	{
		public RESOURCETYPE Resourcename { get; set; }
		public int Quantity { get; set; }
		public float Acsessibility { get; set; }


		#region maths
		public static Resource operator +(Resource a, Resource b)
		{
			a.Quantity += b.Quantity;
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

	public class Asteroid
	{
		public int[] Position { get; set; }
		public List<Resource> Resources { get; set; }
		public int Size { get; set; }
		public string Name { get; set; }

		public Asteroid()
		{
			Position = Array.Empty<int>();
			Resources = new List<Resource>();
			Size = 0;
			Name = string.Empty;
		}
	}

	public class JumpPoint
	{
		[BsonId]
		public Int32 Id { get; set; }

		public int[] Position { get; set; }
		public string Name { get; set; }
		public Int32[] DestinationIDs { get; set; }
		//TODO Implement jump point Discovery, somewhere else ofc

		public JumpPoint()
		{
			Position = Array.Empty<int>();
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
	

	public enum RESOURCETYPE{Critonite, Arkminium, Greberium, Cactuium, Quantinite, Entihnite, Houfium, Katerium, Mikerium, Peterium, Rigelium, Nuterite, zunbillium}
						   //Crito		Arkentosh  Greb		  Cactus	Quantum		Entih	   houfnice Kat		  Mikaelssen Pizzapete rigel	Coconutsales Zunbil
}
