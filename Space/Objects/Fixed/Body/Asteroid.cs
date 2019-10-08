using System.Collections.Generic;
using Raylib;
using Space.Logic;

namespace Space.Objects.Fixed.Body
{
	public class Asteroid : Fixed
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
}
