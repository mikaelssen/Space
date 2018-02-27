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

	public SFMLWindow()
	{
		// initialize the form
		form = new System.Windows.Forms.Form // create our form
		{
			Size = new System.Drawing.Size(800, 800), 
			AutoSize = true,
		};
		form.Show(); 

		//event listeners
		form.Resize += Form_Resize;
		form.KeyPress += Form_KeyPress;

		rendersurface = new DrawingSurface// our control for SFML to draw on
		{
			Size = form.Size, // set our SFML surface control size to be 500 width & 500 height
		}; 
		form.Controls.Add(rendersurface); // add the SFML surface control to our form

		// initialize sfml
		renderwindow = new RenderWindow(rendersurface.Handle); // creates our SFML RenderWindow on our surface control
		//set view
		view = renderwindow.GetView();
		view.Center = new Vector2f(0,0);
		//event handler for keys
		renderwindow.KeyPressed += Renderwindow_KeyPressed;
		//TODO make sure that the window is selected properly, as it won't allow for movement if it's not. BUG

		// drawing loop
		while (form.Visible) // loop while the window is open
		{
			System.Windows.Forms.Application.DoEvents(); // handle form events
			renderwindow.DispatchEvents(); // handle SFML events - NOTE this is still required when SFML is hosted in another window
			renderwindow.Clear(Color.Black); // clear our SFML RenderWindow
			Game.Update();
			Draw();
			renderwindow.Display(); // display what SFML has drawn to the screen
		}
	}
	

	private void Form_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
	{
			//handle sfml input here i guess?
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
			view.Move(new Vector2f(-speed,  0));
		if (e.Code == Keyboard.Key.D)
			view.Move(new Vector2f( speed,  0));
		if (e.Code == Keyboard.Key.W)
			view.Move(new Vector2f( 0, -speed));
		if (e.Code == Keyboard.Key.S)
			view.Move(new Vector2f( 0,  speed));
		if (e.Code == Keyboard.Key.Space)
			view.Zoom(4);

		//System.Diagnostics.Debug.WriteLine(e.Code.ToString());

	}

	public void Draw()
	{


		double image_size = 2500;
		//set view
		renderwindow.SetView(view);
		//TODO Only render the one active system 
		if (Game.Systems.Count <= 0)
		{
			Game.NewGame();
		}
		SolarSystem sys = Game.Systems[0];

		double system_size = 1000;
		float star_size = 0;
		float system_scale = 1;

		star_size = sys.Star.Size / 20000;

		foreach (var planet in sys.Planets)
		{
			if (system_size < planet.DistanceFromStar * 2)
				system_size = planet.DistanceFromStar * 2;
		}
		system_scale = (float)((image_size - 100) / system_size);
		star_size = (int)(star_size * system_scale);

		renderwindow.Draw(new CircleShape(star_size) {
			FillColor = Color.Yellow,
			Radius = star_size,
			Position = new Vector2f(0,0)
			
		});

		foreach (var planet in sys.Planets)
		{

			double Orbit = planet.DistanceFromStar * system_scale;
			double Radius = planet.Size * system_scale / 500;
			double Bearing = planet.Bearing;


			//orbit?
			renderwindow.Draw(new CircleShape((float)Orbit,100)
			{
				FillColor = Color.Cyan,
			});
			//g.DrawEllipse(new Pen(Color.FromArgb(255, 255, 255, 255)), (int)(view.Size.X / 2 - Orbit), (int)(view.Size.X / 2 - Orbit), (int)Orbit * 2, (int)Orbit * 2);

			double pos_x = (image_size / 2 - Orbit * Math.Sin(Bearing * (Math.PI / 180.0)));
			double pos_y = (image_size / 2 - Orbit * Math.Cos(Bearing * (Math.PI / 180.0)));


			renderwindow.Draw(new CircleShape((float)Radius, 100)
			{
				FillColor = Color.Red, Position = new Vector2f((float)pos_x, (float)pos_y)
			});

			renderwindow.Draw(new CircleShape((float)planet.DistanceFromStar,40)
			{
				Position = new Vector2f(0, 0),
				OutlineColor = Color.Green,
				FillColor = Color.Transparent,
				OutlineThickness = 2
			});
			foreach (var moon in planet.Moons)
			{
				renderwindow.Draw(new CircleShape(4, 8)
					{ FillColor = Color.Red, Position = new Vector2f(moon.Position[0], moon.Position[1]) });
			}

			VertexArray line = new VertexArray(PrimitiveType.LinesStrip, 0);
			line.Append(new Vertex(new Vector2f(planet.Position[0], planet.Position[1])));
			Console.WriteLine($"x{planet.Position[0]} y{planet.Position[1]}");
			line.Append(new Vertex(new Vector2f(0,0)));


			

			renderwindow.Draw(line);


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