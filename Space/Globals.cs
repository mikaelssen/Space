using System.Collections.Generic;
namespace Space.Globals
{
	static public class Globals
	{
		//static public double Date; //In hours
		public static DateTime Date = new DateTime(); //In hours
		public static List<Objects.ResourceType> ResourceTypes = new List<Objects.ResourceType>();

	}

	public enum Ownership {NEUTRAL, PLAYER, ENEMIES, FRIENDLY}

}
