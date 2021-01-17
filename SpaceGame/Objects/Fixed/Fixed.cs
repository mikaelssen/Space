using System;
using Raylib;

namespace Space.Objects.Fixed
{
	public class Fixed
	{
		public Vector2 Position;
		public Guid Guid { get; } = new Guid();
		public string Text { get; set; } = "";

		public override string ToString()
		{
			return $"Text : ID [{Guid}]";
		}

	}
}


