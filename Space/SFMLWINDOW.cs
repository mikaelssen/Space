using Space.Objects;
using System;
using SFML.Graphics;
using SFML.System;

class SFMLWindow
{

	public DrawingSurface rendersurface;
	RenderWindow renderwindow;
	System.Windows.Forms.Form form;
	public SFMLWindow()
	{
		// initialize the form
		form = new System.Windows.Forms.Form // create our form
		{
			Size = new System.Drawing.Size(800, 800), // set form size to 600 width & 600 height
			AutoSize = true,
		};
		form.Show(); // show our form
		form.Resize += Form_Resize;
		rendersurface = new DrawingSurface// our control for SFML to draw on
		{
			Size = form.Size, // set our SFML surface control size to be 500 width & 500 height
		}; 
		form.Controls.Add(rendersurface); // add the SFML surface control to our form

		// initialize sfml
		renderwindow = new RenderWindow(rendersurface.Handle); // creates our SFML RenderWindow on our surface control


		// drawing loop
		while (form.Visible) // loop while the window is open
		{
			System.Windows.Forms.Application.DoEvents(); // handle form events
			renderwindow.DispatchEvents(); // handle SFML events - NOTE this is still required when SFML is hosted in another window
			renderwindow.Clear(Color.Black); // clear our SFML RenderWindow
			Draw();
			renderwindow.Display(); // display what SFML has drawn to the screen
		}
	}

	private void Form_Resize(object sender, EventArgs e)
	{
		rendersurface.Size = form.Size;
	}

	public void Draw()
	{
		//TODO Only render the one active system 
		if (Game.Systems.Count <= 0)
		{
			Game.NewGame();
		}
		SolarSystem sys = Game.Systems[0];

		foreach (var planet in sys.Planets)
		{
			renderwindow.Draw(new CircleShape(4, 8)
			{ FillColor = Color.Cyan, Position = new Vector2f(planet.Position[0], planet.Position[1]) });
			foreach (var moon in planet.Moons)
			{
				renderwindow.Draw(new CircleShape(4, 8)
					{ FillColor = Color.Red, Position = new Vector2f(moon.Position[0], moon.Position[1]) });
			}
		}
		foreach (var asteroid in sys.Asteroids)
		{
			renderwindow.Draw(new CircleShape(2, 8)
			{ FillColor = Color.Green, Position = new Vector2f(asteroid.Position[0], asteroid.Position[1]) });
		}
		
		
	}
}
public class DrawingSurface : System.Windows.Forms.Control
{
	protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
	{
		// don't call base.OnPaint(e) to prevent forground painting
		// base.OnPaint(e);
	}
	protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
	{
		// don't call base.OnPaintBackground(e) to prevent background painting
		//base.OnPaintBackground(pevent);
	}
}