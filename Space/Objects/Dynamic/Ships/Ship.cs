using Raylib;

namespace Space.Objects.Dynamic.Ships
{
	//Base class for ships
	public class Ship : Dynamic
	{ 

		public Ship()
		{
			Position = new Vector2();
			CurrentSpeed = 0;
			Text = Name = $"Ship: {Globals.Globals.GetRandomName()}";
		}


	}
}
