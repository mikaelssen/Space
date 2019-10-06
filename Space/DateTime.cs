namespace Space.Globals
{
	/// <summary>
	/// Suggested replacement for double Date. may seem more complicated but neatifies code later when calling time estimates, it could also hold statics for time.
	/// </summary>
	public class DateTime
	{
		private static readonly int YearLength	= 8760;
		private static readonly int DayLenght	= 24;
		private double date;

		//property manager
		public double Date
		{
			get { return date; }
			set { date = value; }
		}

		#region maths
		//fast addition or substraction or nullification. also known as multiplication
		public static DateTime operator *(DateTime a, DateTime b)
		{
			a.date -= b.date;
			return a;
		}
		//division
		public static DateTime operator /(DateTime a, DateTime b)
		{
			a.date /= b.date;
			return a;
		}
		//substraction
		public static DateTime operator -(DateTime a, DateTime b)
		{
			a.date -= b.date;
			return a;
		}
		//addition
		public static DateTime operator +(DateTime a, DateTime b)
		{
			a.date += b.date;
			return a;
		}
		#endregion

		/// <summary>
		/// Returns current year
		/// </summary>
		/// <returns>The Year</returns>
		public int GetYear()
		{
			return (int)date / YearLength;
		}

		/// <summary>
		/// Returns the current day
		/// </summary>
		/// <returns>The Day</returns>
		public int GetDay()
		{
			return (int)(date - GetYear() * YearLength) / DayLenght;//Calculate day
		}

		/// <summary>
		/// Returns the current hour
		/// </summary>
		/// <returns>The Hour</returns>
		public int GetHour()
		{
			return (int)(date - (GetYear() * YearLength) - (GetDay() * DayLenght));//Calculate hour
		}

		/// <summary>
		/// Empty Constructor for initialisation of time itself
		/// </summary>
		public DateTime()
		{
			date = 0;
		}

		/// <summary>
		/// Pre defined time to start at
		/// </summary>
		/// <param name="i">The time at wich we want to start</param>
		public DateTime(double i)
		{
			date = i;
		}
		
	}

	
}
