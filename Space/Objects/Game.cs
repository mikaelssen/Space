using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.Diagnostics;
namespace Space.Objects
{
	public static class Game
	{
		private static List<SolarSystem> systems = new List<SolarSystem>();
		public static Random rng = new Random();

		public static LiteDatabase liteDB = new LiteDatabase("./Database.db");

		internal static List<SolarSystem> Systems { get => systems; set => systems = value; }

		public static void NewGame()
		{
			Systems = new List<SolarSystem>();
			liteDB.DropCollection("systems");
			var col = liteDB.GetCollection<SolarSystem>("systems");
			//col.DropIndex("systems");

			Systems.Add(NewSystem());
			col.Insert(systems);
			LoadGame();
			foreach (var item in Systems)
			{
				foreach (var planet in item.Planets)
				{
					Debug.WriteLine(planet.Name);
				}
			}
			//col.EnsureIndex(x => x.Id);
		}

		public static void LoadGame()
		{
			Systems = new List<SolarSystem>();
			var col = liteDB.GetCollection<SolarSystem>("systems");
			Systems = col.FindAll().ToList();

			Debug.WriteLine($"Loaded systems {col.Count()}");
			
		}

		public static void UpdateData()
		{
			var col = liteDB.GetCollection<SolarSystem>("systems");
			col.Update(systems);
		}

		internal static void Update(int simticks = 5, int RenderSize = 10000)
		{
			foreach (var Sys in systems)
			{
				for (int i = 1; i <= simticks; i++)
				{
					foreach (var planet in Sys.Planets)
					{
						double bearing = planet.Bearing;
						double velocity = planet.Velocity;
						double orbit = planet.DistanceFromStar;
						byte direction = planet.OrbialDirection;

						double circumference = (2 * orbit * Math.PI);
						
						bearing = (bearing + (((circumference * (velocity / 8000000)) / circumference) * 360));
						if (bearing > 360) { bearing = bearing - 360; }
						planet.Bearing = bearing;

						planet.Position[0] = (float)(RenderSize / 2 - orbit * Math.Sin(bearing * (Math.PI / 180.0))); //x
						planet.Position[1] = (float)(RenderSize / 2 - orbit * Math.Cos(bearing * (Math.PI / 180.0))); //y

						
					}
				}
				Console.WriteLine(Sys.Planets[0].Position[0]);
			}
			
		}
	
		/// <summary>
		/// Generation of systems
		/// TODO move this function to a sensible place
		/// </summary>
		/// <returns>System</returns>
		private static SolarSystem NewSystem()
		{

			SolarSystem system = new SolarSystem();

			//Star generation
			system.Star = new Star();

			//planet generation
			for (int i = 0; i < rng.Next(4, 10); i++)
			{
				//planet
				Planet planet = new Planet
				{
					Name = Names.GetRandomName(),
					Size = rng.Next(400, 7000),
					Resources = new List<Resource>(),
					Bearing = RandomRange(0, 360),
					DistanceFromStar =  RandomRange(2, 10),
				};
				//this one is special, leave outside initial generation
				planet.Velocity = Math.Sqrt((6.67408 / 2) * (system.Star.Mass * 10000000) * (2 / planet.DistanceFromStar));

				//moons
				List<Moon> moonsperplanet = new List<Moon>();
				for (int m = 0; m < rng.Next(0, 8); m++)
				{
					Moon moon = new Moon()
					{
						Name = Names.GetRandomName(),
						Size = (planet.Size % rng.Next(1, 4)) * 40,
						Position = new int[]{ rng.Next(-5, 5), rng.Next(-5, 5)}
					};
					moonsperplanet.Add(moon);
				}
				planet.Moons.AddRange(moonsperplanet); //add moons to planet
			
				system.Planets.Add(planet);
			}


			//asteroid generation
			for (int i = 0; i < rng.Next(100,300); i++)
			{
				Asteroid aster = new Asteroid
				{
					Name = Names.GetRandomName(),					
					Size = rng.Next(1, 20),
					Position = new int[] { rng.Next(-500, 500), rng.Next(-500, 500) }
				};
				system.Asteroids.Add(aster);
			}
			
			return system;
		}

		/// <summary>
		/// Generate a jump point between Sys A and Sys B
		/// </summary>
		/// <param name="a">Starting System</param>
		/// <param name="b">Ending System</param>
		private static void NewJumpoints(SolarSystem a, SolarSystem b)
		{
			JumpPoint point = new JumpPoint()
			{
				Name = $"< {a.Id} >-< {b.Id} >",
				Position = new int[] { rng.Next(-2000, 2000), rng.Next(-2000, 2000) },
				DestinationIDs = new Int32[] { a.Id, b.Id }
			};
			a.JumpPoints.Add(point);
			b.JumpPoints.Add(point);
		}

		public static int RandomRange(int min, int max)
		{
			return Math.Abs(rng.Next() * (max - min) + min);
		}

		public static double RandomRange(double min, double max)
		{
			return Math.Abs(rng.NextDouble() * (max - min) + min);
		}
	}
}
