using System.Collections.Generic;
using Space.Logic;

namespace Space.Objects.Fixed.Structure
{

	//base structure
	//for stations and what not, static orbiting / non moving things
	public class Structure : Fixed
	{
		public Dictionary<Resource, int> Price { get; set; }

		public Structure()
		{
			Price = new Dictionary<Resource, int>();
		}
	}
}
