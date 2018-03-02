using Space.Objects;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class SFMLWindow
{

	public DrawingSurface rendersurface;
	RenderWindow renderwindow;
	System.Windows.Forms.Form form;
	View view;
	int tickspeed = 1;

#if DEBUG
	VertexArray moonpathline = new VertexArray(PrimitiveType.LinesStrip, 0);
#endif

	public SFMLWindow()
	{
		// initialize the form
		form = new System.Windows.Forms.Form // create our form
		{
			Size = new System.Drawing.Size(800, 800),
			AutoSize = true
		};
		form.Show();

		//event listeners
		form.Resize += Form_Resize;

		//make sure there's a game. or something

		if (Game.Systems.Count <= 0)
			Game.LoadGame();
		if (Game.Systems.Count <= 0) //if no game to load we make a game
			Game.NewGame();


		rendersurface = new DrawingSurface// our control for SFML to draw on
		{
			Size = form.Size, // set our SFML surface control size to be 500 width & 500 height
		};
		form.Controls.Add(rendersurface); // add the SFML surface control to our form

		rendersurface.Focus();
		rendersurface.Select(); //this forcuses and selects the SFML instance.

		// initialize sfml
		renderwindow = new RenderWindow(rendersurface.Handle); // creates our SFML RenderWindow on our surface control
															   //set view
		view = renderwindow.GetView();
		view.Center = new Vector2f(0, 0);
		//event handler for keys
		renderwindow.KeyPressed += Renderwindow_KeyPressed;

		// drawing loop
		while (form.Visible) // loop while the window is open
		{
			System.Windows.Forms.Application.DoEvents(); // handle form events
			renderwindow.DispatchEvents(); // handle SFML events - NOTE this is still required when SFML is hosted in another window
			renderwindow.Clear(Color.Black); // clear our SFML RenderWindow
			Game.Update(tickspeed);
			Draw();
			renderwindow.Display(); // display what SFML has drawn to the screen
		}
	}

	private void Form_Resize(object sender, EventArgs e)
	{
		rendersurface.Size = form.Size;
		if (renderwindow != null)
		{
			renderwindow.Size = new Vector2u((uint)form.Size.Width, (uint)form.Size.Height);
		}
	}

	private void Renderwindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
	{
		int speed = 50;
		//move the view
		if (e.Code == Keyboard.Key.A)
			view.Move(new Vector2f(-speed, 0));
		if (e.Code == Keyboard.Key.D)
			view.Move(new Vector2f(speed, 0));
		if (e.Code == Keyboard.Key.W)
			view.Move(new Vector2f(0, -speed));
		if (e.Code == Keyboard.Key.S)
			view.Move(new Vector2f(0, speed));
		if (e.Code == Keyboard.Key.R)
			view.Zoom(2f);
		if (e.Code == Keyboard.Key.F)
			view.Zoom(0.5f);
		if (e.Code == Keyboard.Key.T)
			tickspeed++;
		if (e.Code == Keyboard.Key.G)
			tickspeed--;
	}

	public void Draw()
	{
		//set view
		renderwindow.SetView(view);

		SolarSystem sys = Game.Systems[0];

		float star_size = 0;
		float system_scale = 1;
		
		star_size = sys.Star.Size / 20000;
		
		//sun
		renderwindow.Draw(new CircleShape(star_size)
		{
			FillColor = Color.Yellow,
			Radius = star_size,
			Position = new Vector2f(0, 0),
			Origin = new Vector2f(star_size, star_size) //Center it
		});

		foreach (var planet in sys.Planets)
		{

			float Radius = planet.Size * system_scale / 500;

			//orbit path
			renderwindow.Draw(new CircleShape(planet.DistanceFromStar / 100)
			{
				Position = new Vector2f(0, 0),
				Origin = new Vector2f(planet.DistanceFromStar / 100, planet.DistanceFromStar / 100), //center of point
				OutlineColor = Color.Green,
				FillColor = Color.Transparent,
				OutlineThickness = 10
			});

			//planet
			renderwindow.Draw(new CircleShape(Radius)
			{
				Origin = new Vector2f(Radius, Radius),
				FillColor = Color.Red,
				Position = new Vector2f(planet.Position[0], planet.Position[1])
			});


			foreach (var moon in planet.Moons)
			{
				float MoonRadius = planet.Size * system_scale / 500;

				//moon
				renderwindow.Draw(new CircleShape(MoonRadius )
				{
					FillColor = Color.Blue,
					Origin = new Vector2f(MoonRadius, MoonRadius ),
					Position = new Vector2f(moon.Position[0], moon.Position[1])
				});

				//moon orbits
				renderwindow.Draw(new CircleShape(moon.DistanceFromPlanet / 100)
				{
					Position = new Vector2f(planet.Position[0], planet.Position[1]),
					Origin = new Vector2f(moon.DistanceFromPlanet / 100 , moon.DistanceFromPlanet / 100), //center of point
					OutlineColor = Color.Yellow,
					FillColor = Color.Transparent,
					OutlineThickness = 10
				});

#if DEBUG
				//Draw moon relation lines
				//VertexArray moontoplanetline = new VertexArray(PrimitiveType.LinesStrip, 0);
				//moontoplanetline.Append(new Vertex(new Vector2f(planet.Position[0], planet.Position[1])));
				//moontoplanetline.Append(new Vertex(new Vector2f(moon.Position[0],moon.Position[1])));
				//renderwindow.Draw(moontoplanetline);
#endif
			}

#if DEBUG
			//lines to planets, THis is only compiled to debug version
			//VertexArray planetcenterline = new VertexArray(PrimitiveType.LinesStrip, 0);
			//planetcenterline.Append(new Vertex(new Vector2f(planet.Position[0], planet.Position[1])));
			//planetcenterline.Append(new Vertex(new Vector2f(0,0)));
			//renderwindow.Draw(planetcenterline);
#endif
		}
#if DEBUG
		//tracks one moon may crash if there is no moon
		try
		{
			moonpathline.Append(new Vertex(new Vector2f(Game.Systems[0].Planets[0].Moons[0].Position[0], Game.Systems[0].Planets[0].Moons[0].Position[1])));
			renderwindow.Draw(moonpathline);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message + "Making a new game 'cause fuck it");
			Game.NewGame();
		}
#endif


		/* TODO implement asteroids properly and render them in nice belts or something.
		foreach (var asteroid in sys.Asteroids)
		{
			renderwindow.Draw(new CircleShape(8)
			{
				FillColor = Color.Green,
				Position = new Vector2f(asteroid.Position[0], asteroid.Position[1]),
				Origin = new Vector2f(asteroid.Position[0], asteroid.Position[1])
			});
		}*/


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