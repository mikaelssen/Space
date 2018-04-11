using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Objects
{
	class ResourceManager
	{
		//TODO: write parser for this
		//load buildings
		//load recepies
		//load effects
	}

	/// <summary>
	/// Building object structure
	/// </summary>
	class Building
	{
		public string BuildingName { get; set; }
		public List<CraftingRecepie> Products { get; set; }
		public List<Effects> Effects{ get; set; }
	}

	/// <summary>
	/// The effect class will be made later, it's for defining a permament bonus from a building, like +1megawatt of power or such.
	/// TODO: write this stuff
	/// </summary>
	public class Effects
	{
	}

	/// <summary>
	/// basic recepie template
	/// </summary>
	class CraftingRecepie
	{
		public Dictionary<string,int> Inputs { get; set; }
		public Dictionary<string,int> Outputs { get; set; }
		public string RecepieName { get; set; }
	}
}
