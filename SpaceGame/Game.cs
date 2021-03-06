﻿using System;
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
using Space.Logic;
using System.IO;

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
		Shader shader = new Shader();

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
			Console.WriteLine(Environment.CurrentDirectory);
			var s = File.ReadAllText(@".\Src\Shader\SunShader.fs");
			shader = new Shader();
			//shader = R.LoadShaderCode(null,s) ;


			ScreenHeight = 240 * 4;
			ScreenWidth = 320 * 4;
			TargetFps = 60;

			//basic init of raylib
			R.InitWindow(ScreenWidth, ScreenHeight, "Space");
			R.SetTargetFPS(TargetFps);
            cameratarget = new Vector2(0, 0);

            camera = new Camera2D
            {
                target = cameratarget,
                zoom = 1f,
                rotation = 0.0f,
				
            };

			ticktimer.Start();
			Globals.Globals.Date = new Logic.DateTime();

			if (sys.Systems.Count <= 0)
				sys.NewGame();

		}

	
		private void Update()
		{
			Input();

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
		private void Input()
		{


			int speed = 10;

			//cam movement
			if (R.IsKeyDown(KeyboardKey.KEY_W))
				cameratarget = new Vector2(cameratarget.x, cameratarget.y - speed);
			if (R.IsKeyDown(KeyboardKey.KEY_A))
				cameratarget = new Vector2(cameratarget.x - speed, cameratarget.y);
			if (R.IsKeyDown(KeyboardKey.KEY_S))
				cameratarget = new Vector2(cameratarget.x, cameratarget.y + speed);
			if (R.IsKeyDown(KeyboardKey.KEY_D))
				cameratarget = new Vector2(cameratarget.x + speed, cameratarget.y);

            //zoom
            float oldzoom = camera.zoom;
			if (R.GetMouseWheelMove() > 0 || R.GetMouseWheelMove() < 0)
			{
				//This is not taking into account the screen movement to 'center' the mouse. That's why it's fucky.

				//cameratarget = R.GetScreenToWorld2D(R.GetMousePosition(), camera);
				cameratarget = R.GetScreenToWorld2D(R.GetMousePosition(),camera);
				camera.zoom += (R.GetMouseWheelMove() * 0.05f * camera.zoom);
			}

			//tick manipulation
			if (R.IsKeyDown(KeyboardKey.KEY_T))
				ticksize++;
			if (R.IsKeyDown(KeyboardKey.KEY_G))
				ticksize--;

			//space manipulation

			if (R.IsKeyPressed(KeyboardKey.KEY_Q))
				sys.NewGame(); //this is hacky as fuck :)

			//move camera
			camera.target = cameratarget; //this moves the camera to the set position.
			camera.offset = new Vector2(R.GetScreenWidth() / 2, R.GetScreenHeight() / 2); //this sets the 'focus' to center.


		}

		private void Draw()
		{
			SolarSystem s = sys.Systems[0];

            R.BeginDrawing();
			R.ClearBackground(Color.BLACK);
			float Radius = 0;



			R.BeginMode2D(camera);



			//R.BeginShaderMode(shader);
			Radius = s.Star.Size / 5000;
			R.DrawCircle(0, 0, Radius, Color.YELLOW);
			//R.EndShaderMode();


			for (int i = 0; i < s.Planets.Count; i++)
			{

				//orbit
				Radius = s.Planets[i].DistanceFromStar / 100;
				R.DrawCircleLines(0, 0, Radius, Color.RED);

				//planet itself
				Radius = s.Planets[i].Size / 2500;
				R.DrawCircleV(
					s.Planets[i].Position,
					Radius, Color.BLUE);

				//only render moons if you're close enough
				if (camera.zoom > 1.3)
				{
					for (int m = 0; m < s.Planets[i].Moons.Count; m++)
					{
						//orbit
						Radius = s.Planets[i].Moons[m].DistanceFromPlanet / 20000;
						R.DrawCircleLines(
							(int)s.Planets[i].Position.x,
							(int)s.Planets[i].Position.y,
							Radius, Color.YELLOW);

						//moon itself
						Radius = s.Planets[i].Moons[m].Size / 50;
						R.DrawCircleV(
							s.Planets[i].Moons[m].Position,
							Radius, Color.GREEN);
					}
				}
			}


			//asteroids, we don't update them yet so no point in drawing, they're just being generated 
			//for (int a = 0; a < s.Asteroids.Count; a++)
			//{
			//R.DrawCircleV(s.Asteroids[a].Position, 30, Color.GOLD);
			//}

			//Lets find this god damn mouse
			Vector2 pos = R.GetScreenToWorld2D(new Vector2(R.GetMouseX(), R.GetMouseY()), camera);
			R.DrawCircle((int)pos.x, (int)pos.y, 50, Color.BEIGE);

			//R.GetWorldToScreen(new Vector3(R.GetMouseX(), R.GetMouseY(), 0), camera);

			R.EndMode2D();


            R.DrawText("Target: " + camera.target.ToString(), 20, 0, 20, Color.WHITE);
            R.DrawText("Offset: " + camera.offset.ToString(), 20, 30, 20, Color.WHITE);
            R.DrawText("MW2SPo: " + R.GetScreenToWorld2D(R.GetMousePosition(),camera).ToString(), 20, 60, 20, Color.WHITE);
            R.DrawText("  Zoom: " + camera.zoom.ToString(), 20, 120, 20, Color.WHITE);
            

			R.EndDrawing();
		}
	}
}