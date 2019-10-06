using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using R = Raylib.Raylib;
using LiteDB;

namespace Space
{
	public class Game
	{

		//base setup
		static public int ScreenWidth { get; set; } //clamp to 0 min
		static public int ScreenHeight { get; set; } //clamp to 0 min
		static public int TargetFps { get; set; }

		static public LiteDatabase Database { get; set; }


		static readonly public Random rng = new Random();
		public Objects.SystemManager sys = new Objects.SystemManager();

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
			
			//TODO: Translate SFMLWindow into raylib window, and move SystemManage (previously game.cs) into this logic.

		}

		private void Update()
		{
		
		}

		private void Draw()
		{
			R.BeginDrawing();
			R.ClearBackground(Color.BLACK);
			

		
			R.EndMode2D();
			R.EndDrawing();
		}
	}
}