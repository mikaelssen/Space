using System;
using LiteDB;
using Raylib;

namespace Space.Logic
{

	/// <summary>
	/// Not really an entity, just a logical thing. Perhaps keep a list of types somewhere, or generate some.
	/// </summary>
	public class Resource 
	{
		public string Name;
		public int Quantity { get; set; }
		public float Acsessibility { get; set; }

		#region Resource Maths, allows for direct manipulation of objects

		public static Resource operator +(Resource a, Resource b)
		{
			a.Quantity += b.Quantity;
			return a;
		}

		public static Resource operator *(Resource a, Resource b)
		{
			a.Quantity *= b.Quantity;
			if (a.Quantity <= 0) //can't be less than 0, also prevents negative multiplication?
				a.Quantity = 0;
			return a;
		}

		public static Resource operator /(Resource a, Resource b)
		{
			a.Quantity /= b.Quantity;
			if (a.Quantity <= 0) //can't be less than 0
				a.Quantity = 0;
			return a;
		}

		public static Resource operator -(Resource a, Resource b)
		{
			a.Quantity -= b.Quantity;
			if (a.Quantity <= 0)
				a.Quantity = 0;
			return a;
		}

		#endregion

		public Resource()
		{
			Name = "Unknown resource";
			Quantity = 0;
			Acsessibility = 0;
		}
	}

	/// <summary>
	/// Name class, also got function for fetching a name
	/// </summary>
	
}
