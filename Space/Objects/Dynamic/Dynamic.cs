using System;
using Raylib;

namespace Space.Objects.Dynamic
{
	public class Dynamic
	{
		public Vector2 Position;
		public Guid Guid { get; } = new Guid();
		public string Text { get; set; } = "";
		public int CurrentSpeed { get; set; }
		public string Name { get; set; }

		public override string ToString()
		{
			return $"Text : ID [{Guid}]";
		}

	}
}
							
