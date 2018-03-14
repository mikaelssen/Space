using Space.Objects;
using Space.Globals;
using System;
using System.Threading;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class SFMLWindow
{

	public DrawingSurface rendersurface;
	RenderWindow renderwindow;
	System.Windows.Forms.Form form;
	View view;
	int ticksize = 1;
    int tickrate = 10, currenttickrate; //ticks per second
    Stopwatch ticktimer = new Stopwatch();
    long tickmicros;
    Vector2f v = new Vector2f();
    Vector2f mousedrag = new Vector2f();

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
			//Game.LoadGame();
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
		renderwindow.MouseButtonPressed += Renderwindow_MousePressed;
        renderwindow.MouseButtonReleased += Renderwindow_MouseReleased;
        int hour, day, year;
        // drawing loop
        while (form.Visible) // loop while the window is open
		{
            if (!ticktimer.IsRunning){ ticktimer.Start(); }//start ticktimer if not started

            year = (int)Globals.Date / 8760;//Calculate year
            day = (int)(Globals.Date - year * 8760) / 24;//Calculate day
            hour = (int)(Globals.Date - (year * 8760) - (day * 24));//Calculate hour

            tickmicros = (ticktimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))); //convert to microseconds using timeticks and processor frequency
            form.Text = string.Format("ticksize = {0}, tickrate {4}({5})  year {1}  day {2}  hour {3}", ticksize, year, day, hour,tickrate,currenttickrate);
            

            if (tickmicros > 1000000/tickrate) //do simulation after enought time
            {
                currenttickrate = (int)(1000000 / tickmicros); //calculate actuall tickrate
                ticktimer.Restart(); //restart timer for next round
                Game.Update(ticksize); // Update simulation
                Globals.Date = Globals.Date + ticksize; // update date
            }

            System.Windows.Forms.Application.DoEvents(); // handle form events 

            renderwindow.Clear(Color.Black); // clear our SFML RenderWindow 

            renderwindow.DispatchEvents(); // handle SFML events - NOTE this is still required when SFML is hosted in another window 

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

    private void Renderwindow_MousePressed(object sender, MouseButtonEventArgs e)
    {
        SolarSystem sys = Game.Systems[0];
        if (e.Button == Mouse.Button.Left)
        {
            v = renderwindow.MapPixelToCoords(new Vector2i(e.X, e.Y), view);

            Console.WriteLine($"X:{v.X} Y:{v.Y}");

            if (sys.Star.Shape.GetGlobalBounds().Contains(v.X, v.Y))
                Star.Click();

            foreach (var planet in sys.Planets)
            {
                if (planet.Shape.GetGlobalBounds().Contains(v.X, v.Y))
                    planet.Click();

                foreach (var moon in planet.Moons)
                    if (moon.Shape.GetGlobalBounds().Contains(v.X, v.Y))
                        moon.Click();
            }
        }
        if (e.Button == Mouse.Button.Middle)
        {
            mousedrag = renderwindow.MapPixelToCoords(new Vector2i(e.X, e.Y), view); //get mouse cord when pressed
        }
    }
    private void Renderwindow_MouseReleased(object sender, MouseButtonEventArgs e) //event a mouse button was released
    {
        SolarSystem sys = Game.Systems[0];
        if (e.Button == Mouse.Button.Middle)
        {
            mousedrag = mousedrag - renderwindow.MapPixelToCoords(new Vector2i(e.X, e.Y), view); //calculate how far the  mouse moved
            view.Move(mousedrag); //move view that distance
        }
    }

    private void Renderwindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
	{
		int speed = 500;
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
			ticksize = ticksize + 1;
		if (e.Code == Keyboard.Key.G)
			ticksize = ticksize - 1;
		if (e.Code == Keyboard.Key.K)
			moonpathline.Clear();
        if (e.Code == Keyboard.Key.Y)
            tickrate = tickrate + 10;
        if (e.Code == Keyboard.Key.H & tickrate > 10)
            tickrate = tickrate - 10;
    }

	public void Draw()
	{
		//set view
		renderwindow.SetView(view);        
		SolarSystem sys = Game.Systems[0];

		//mouse cord testing
		renderwindow.Draw(new CircleShape()
		{
			Radius = 5,
			FillColor = Color.Magenta,
			Position = v,
			Origin = new Vector2f(5, 5)
		});

		//sun
		renderwindow.Draw(sys.Star.GetDrawable());

		foreach (var planet in sys.Planets)
		{
            //orbit path //TODO add orbits as a function? X Y Distance
            renderwindow.Draw(new CircleShape(planet.DistanceFromStar / 100)
			{
				Position = new Vector2f(0, 0),
				Origin = new Vector2f(planet.DistanceFromStar / 100, planet.DistanceFromStar / 100), //center of point                
				OutlineColor = Color.Green,
				FillColor = Color.Transparent,
				OutlineThickness = 10
			});


			planet.Text.Position = new Vector2f(planet.Position[0] + planet.Shape.Radius, planet.Position[1] + planet.Shape.Radius);
			planet.Text.Scale = new Vector2f(16, 16);
			renderwindow.Draw(planet.Text);

            Text positiontext = new Text($"{planet.DistanceFromStar/150000}", Space.Resources.Resources.Font);
            positiontext.Position = new Vector2f(planet.Position[0] + planet.Shape.Radius, planet.Position[1] + planet.Shape.Radius*4);
            positiontext.Scale = new Vector2f(16, 16);
            renderwindow.Draw(positiontext);

            //planet
            planet.GetDrawable();
			renderwindow.Draw(planet.Shape);


			foreach (var moon in planet.Moons)
			{
				float MoonRadius = moon.Size/10;

				//moon orbits
				renderwindow.Draw(new CircleShape(moon.DistanceFromPlanet / 2500)
				{
					Position = new Vector2f(planet.Position[0], planet.Position[1]),
					Origin = new Vector2f(moon.DistanceFromPlanet / 2500, moon.DistanceFromPlanet / 2500), //center of point
					OutlineColor = Color.Yellow,
					FillColor = Color.Transparent,
					OutlineThickness = 10
				});

				moon.Text.Position = new Vector2f(moon.Position[0] + moon.Shape.Radius, moon.Position[1] + moon.Shape.Radius);
				moon.Text.Scale = new Vector2f(16, 16);
				renderwindow.Draw(moon.Text);

				//moon
				renderwindow.Draw(moon.GetDrawable(MoonRadius));

			}

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