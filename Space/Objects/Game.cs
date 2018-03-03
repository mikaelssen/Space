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
			Systems = new List<SolarSystem>
			{
				NewSystem()
			};

			liteDB.DropCollection("systems");
			liteDB.GetCollection<SolarSystem>("systems").Insert(systems);


			//LoadGame(); 
		
			//col.EnsureIndex(x => x.Id); //sets ID to track
		}

		public static void LoadGame()
		{

			//TODO FIX DRAWABLE ACSESS ERROR WHEN LOADING. WILL TAKE A LOT OF WORK, MIK TASK
			Systems = new List<SolarSystem>();
			Systems =  liteDB.GetCollection<SolarSystem>("systems").FindAll().ToList();
			Debug.WriteLine($"Loaded systems {Systems.Count()}");

		}

		public static void UpdateData()
		{
			var col = liteDB.GetCollection<SolarSystem>("systems");
			col.Update(systems);
		}

		internal static void Update(int ticksize = 1)
		{
			foreach (var Sys in systems)
			{
				
				foreach (var planet in Sys.Planets)
				{
					double bearing = planet.Bearing;
					double velocity = planet.Velocity;
    				double orbit = planet.DistanceFromStar;
                    int direction = planet.OrbialDirection;
					//TODO implement direction, so they go the other way sometimes
					//byte direction = planet.OrbialDirection;

					double circumference = (2 * orbit * Math.PI);

                    bearing = bearing + (((velocity*3600)/circumference)/36000000)* ticksize;
					if (bearing > 360) { bearing = bearing - 360; }
					planet.Bearing = bearing;
                
					planet.Position[0] = (float)(orbit/100 * Math.Sin(bearing * (Math.PI / 180.0))); //x
					planet.Position[1] = (float)(orbit/100 * Math.Cos(bearing * (Math.PI / 180.0))); //y
                    Console.WriteLine(planet.Position[0]);

                    foreach (var moon in planet.Moons)
					{
						double moonbearing = moon.Bearing;
						double moonvelocity = moon.Velocity;
						double moonorbit = moon.DistanceFromPlanet;
						//byte moondirection = moon.OrbialDirection;
						double mooncircumference = (2 * moonorbit * Math.PI);

                        moonbearing = moonbearing + ((((moonvelocity * 3600) / 1) / mooncircumference) / 360)* ticksize;
						if (moonbearing > 360) { moonbearing = moonbearing - 360; }
						moon.Bearing = moonbearing;

						moon.Position[0] = planet.Position[0] + (float)(moonorbit/5000 * Math.Sin(moonbearing * (Math.PI / 180.0))); //x
						moon.Position[1] = planet.Position[1] + (float)(moonorbit/5000 * Math.Cos(moonbearing * (Math.PI / 180.0))); //y
					}
				}
				
			}

		}

		/// <summary>
		/// Generation of systems
		/// TODO move this function to a sensible place
		/// </summary>
		/// <returns>System</returns>
		private static SolarSystem NewSystem()
		{

			SolarSystem system = new SolarSystem
			{
				Star = new Star()
			};

			//planet generation
			for (int i = 0; i < rng.Next(1, 10); i++)
			{
				//planet
				Planet planet = new Planet
				{
					Name = Names.GetRandomName(),
					Size = rng.Next(2500, 140000), // Between pluto and juptier size
					Resources = new List<Resource>(),
					Bearing = rng.Next(0, 360),
					Density = rng.Next(1000, 7000),
					DistanceFromStar = rng.Next(50000, 2000000)
				};

				//this one is special, leave outside initial generation	
				planet.Mass = Math.Round(Math.Pow(planet.Size / 2, 3) * 314 * 75 / 5500000000 * planet.Density);

				planet.Velocity = Math.Round(Math.Sqrt((10 * (system.Star.Mass + planet.Mass)) / (planet.DistanceFromStar * 10)));

				//moons
				List<Moon> moonsperplanet = new List<Moon>();
				for (int m = 0; m < rng.Next(1, 10); m++)
				{
					Moon moon = new Moon()
					{
						Name = Names.GetRandomName(),
						Size = rng.Next(100, 1500),
						Density = rng.Next(3000, 5000),
						Bearing = rng.Next(0, 360)
					};
					moon.DistanceFromPlanet = moon.Size + planet.Size * 10 + rng.Next(60000, 10000000);
					moon.Mass = Math.Round(Math.Pow(moon.Size / 2, 3) * 314 * 75 / 5500000000 * moon.Density);
					moon.Velocity = Math.Round(Math.Sqrt((10 * (moon.Mass + planet.Mass)) / moon.DistanceFromPlanet));
					moonsperplanet.Add(moon);
				}
				planet.Moons.AddRange(moonsperplanet); //add moons to planet

				system.Planets.Add(planet);
			}


			//asteroid generation
			//TODO this asteroid stuff
			/*for (int i = 0; i < rng.Next(0,0); i++)
			{
				Asteroid aster = new Asteroid
				{
					Name = Names.GetRandomName(),					
					Size = rng.Next(1, 20),
					Position = new int[] { rng.Next(-500, 500), rng.Next(-500, 500) }
				};
				system.Asteroids.Add(aster);
			}*/

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

	}
}
