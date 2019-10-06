using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using System.Diagnostics;
using Raylib;
using R = Raylib.Raylib;

namespace Space.Objects
{
	public class SystemManager
	{
		public static Random rng = new Random();

		public static LiteDatabase liteDB = new LiteDatabase("./Database.db");

		internal static List<SolarSystem> Systems { get; set; } = new List<SolarSystem>();

		public static void NewGame()
		{
			Systems = new List<SolarSystem>
			{
				NewSystem()
			};
			liteDB.DropCollection("systems");
			liteDB.GetCollection<SolarSystem>("systems").Insert(Systems);


			//LoadGame(); 
		
			//col.EnsureIndex(x => x.Id); //sets ID to track
		}

		public static void LoadGame()
		{

			//TODO Seperate drawing and objects. so objects don't contain color data and what not, and this is fine.
			Systems = new List<SolarSystem>();
			Systems =  liteDB.GetCollection<SolarSystem>("systems").FindAll().ToList();
			Debug.WriteLine($"Loaded systems {Systems.Count()}");

		}

		public static void UpdateData()
		{
			var col = liteDB.GetCollection<SolarSystem>("systems");
			col.Update(Systems);
		}

		internal static void Update(int ticksize = 1)
		{
			foreach (var Sys in Systems)
			{
				
				foreach (var planet in Sys.Planets)
				{
					double bearing = planet.Bearing;
					double velocity = planet.Velocity;
    				double orbit = planet.DistanceFromStar;
                    int direction = planet.OrbialDirection;
                    //TODO implement direction, so they go the other way sometimes
                    //byte direction = planet.OrbialDirection;

                    double circumference = planet.Circumference;

                    bearing = bearing + planet.BearingDV * ticksize;
					if (bearing > 360)
                    {
                        bearing = bearing%360;
                    }
					planet.Bearing = bearing;
                
					planet.Position.x = (float)(orbit/100 * Math.Sin(bearing * (Math.PI / 180.0))); //x
					planet.Position.y = (float)(orbit/100 * Math.Cos(bearing * (Math.PI / 180.0))); //y
                    //Console.WriteLine(planet.Position[0]);

                    foreach (var moon in planet.Moons)
					{
						double moonbearing = moon.Bearing;
						double moonvelocity = moon.Velocity;
						double moonorbit = moon.DistanceFromPlanet;
						//byte moondirection = moon.OrbialDirection;
						double mooncircumference = moon.Circumference;

                        moonbearing = moonbearing + moon.BearingDV * ticksize;
						if (moonbearing > 360) { moonbearing =moonbearing%360; }
						moon.Bearing = moonbearing;

						moon.Position.x = planet.Position.x + (float)(moonorbit/2500 * Math.Sin(moonbearing * (Math.PI / 180.0))); //x
						moon.Position.y = planet.Position.y + (float)(moonorbit/2500 * Math.Cos(moonbearing * (Math.PI / 180.0))); //y
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
            Globals.Globals.Date = new Globals.DateTime();
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
                planet.Circumference = Math.Round(2 * planet.DistanceFromStar * Math.PI);
                planet.Mass = Math.Round(Math.Pow(planet.Size / 2, 3) * 314 * 75 / 5500000000 * planet.Density);
				planet.Velocity = Math.Round(Math.Sqrt((10 * (system.Star.Mass + planet.Mass)) / (planet.DistanceFromStar * 10)));
                planet.BearingDV = (360 / (planet.Circumference * 1000 / (planet.Velocity * 3.6)));

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
                    moon.Circumference = Math.Round(2 * moon.DistanceFromPlanet * Math.PI);
                    moon.BearingDV = (360 / (moon.Circumference / (moon.Velocity * 3.6)));
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
				Position = new Vector2(rng.Next(-2000, 2000), rng.Next(-2000, 2000)),
				DestinationIDs = new Int32[] { a.Id, b.Id }
			};
			a.JumpPoints.Add(point);
			b.JumpPoints.Add(point);
		}

	}
}
