using Space.Objects.Fixed.Body;
using Space.Objects.Dynamic.Ships;
using Space.Objects.Fixed.Structure;
using System;
using System.Collections.Generic;

namespace Space.Managers
{
	/// <summary>
	/// This holds the data for all bodies and structures visible when not in map mode. for the current ID system, if one is selected
	/// </summary>
	public class SolarSystem
	{
		public Int32 Id { get; set; }
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
		}

		public override string ToString()
		{
			return $"System : {Id}";
		}

	}
}
