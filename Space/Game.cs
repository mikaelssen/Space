using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Space.Objects.Dynamic.Ships;
using Space.Objects.Fixed.Body;
using Space.Managers;
using Raylib;
using R = Raylib.Raylib;
using LiteDB;
using System.Diagnostics;

namespace Space
{
	public class Game
	{

		//base setup
		static public int ScreenWidth { get; set; } //clamp to 0 min
		static public int ScreenHeight { get; set; } //clamp to 0 min
		static public int TargetFps { get; set; }
		static Camera2D camera;
		static Vector2 cameratarget;
		static public LiteDatabase Database { get; set; }


		static readonly public Random rng = new Random();
		public SystemManager sys = new SystemManager();

		int ticksize = 1;
		int tickrate = 10, currenttickrate;
		long tickmicros;
		Stopwatch ticktimer = new Stopwatch();

		/// <summary>
		/// Main loop
		/// </summary>
		public void Run() 
		{
			Setup();

			//Run forever untill close flag
			while (!R.WindowShouldClose())
			{
				Update();
				Draw();
			}
		}

		/// <summary>
		/// Setup the game window.
		/// </summary>
		private void Setup()
		{
			ScreenHeight = 240 * 4;
			ScreenWidth = 320 * 4;
			TargetFps = 60;
			//basic init of raylib
			R.InitWindow(ScreenWidth, ScreenHeight, "Space");
			R.SetTargetFPS(TargetFps);

			cameratarget = new Vector2(0, 0);

			camera = new Camera2D();
			camera.target = cameratarget;
			camera.zoom = 1f;

			ticktimer.Start();
			Globals.Globals.Date = new Globals.DateTime();

			if (sys.Systems.Count <= 0)
				sys.NewGame();

		}

		private void Update()
		{
			input();

			tickmicros = (ticktimer.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L))); //convert to microseconds using timeticks and processor frequency
			R.SetWindowTitle(string.Format("ticksize = {0}, tickrate {4}({5})  year {1}  day {2}  hour {3}",
				ticksize, Globals.Globals.Date.GetYear(), Globals.Globals.Date.GetDay(), Globals.Globals.Date.GetHour(), tickrate, currenttickrate));

			if (tickmicros > 1000000 / tickrate) //do simulation after enought time
			{
				currenttickrate = (int)(1000000 / tickmicros); //calculate actuall tickrate
				ticktimer.Restart(); //restart timer for next round
				sys.Update(ticksize); //update simulation
				Globals.Globals.Date.Date += ticksize; // update date
			}
		}
		private void input()
		{
			int speed = 10;

			//cam movement

			if (R.IsKeyDown(KeyboardKey.KEY_W))
				cameratarget = new Vector2(cameratarget.x, cameratarget.y + speed);
			if (R.IsKeyDown(KeyboardKey.KEY_A))
				cameratarget = new Vector2(cameratarget.x + speed, cameratarget.y);
			if (R.IsKeyDown(KeyboardKey.KEY_S))
				cameratarget = new Vector2(cameratarget.x, cameratarget.y - speed);
			if (R.IsKeyDown(KeyboardKey.KEY_D))
				cameratarget = new Vector2(cameratarget.x - speed, cameratarget.y);

            //zoom
            float oldzoom = camera.zoom;
			camera.zoom += ((float)R.GetMouseWheelMove() * 0.1f * camera.zoom);

            //tick manipulation
            if (R.IsKeyDown(KeyboardKey.KEY_T))
				ticksize++;
			if (R.IsKeyDown(KeyboardKey.KEY_G))
				ticksize--;

			//move camera
            

			camera.target = cameratarget;
            camera.offset = new Vector2(R.GetScreenWidth() / 2, R.GetScreenHeight() / 2);


        }

		private void Draw()
		{
			SolarSystem s = sys.Systems[0];

            R.BeginDrawing();
			R.ClearBackground(Color.BLACK);

			R.BeginMode2D(camera);


			float Radius = 0;

			Radius = s.Star.Size / 5000;
			R.DrawCircle(0, 0, Radius, Color.YELLOW);

			for (int i = 0; i < s.Planets.Count; i++)
			{
				Radius = s.Planets[i].Size / 2500;
				R.DrawCircle(
					(int)s.Planets[i].Position.x,
					(int)s.Planets[i].Position.y,
					Radius, Color.BLUE);

				for (int m = 0; m < s.Planets[i].Moons.Count; m++)
				{
					Radius = s.Planets[i].Moons[m].Size / 50;
					R.DrawCircle(
						(int)s.Planets[i].Moons[m].Position.x,
						(int)s.Planets[i].Moons[m].Position.y,
						Radius, Color.GREEN);
				}
			}


			R.EndMode2D();
			R.EndDrawing();
		}
	}
}