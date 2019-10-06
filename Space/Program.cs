using System;
using System.Windows.Forms;

namespace Space
{
	static class Program
	{
		static void Main()
		{
			//setup the non static game enviorment.
			Game g = new Game();
			g.Run();
		}
	}
}
