using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using R = Raylib.Raylib;

namespace Bitcomputing
{
	public class Game
	{

		//base setup
		static public int ScreenWidth { get; set; } //clamp to 0 min
		static public int ScreenHeight { get; set; } //clamp to 0 min
		static public int TargetFps { get; set; }
		static readonly public Random rng = new Random();

		/// <summary>
		/// Main loop
		/// </summary>
		public void Run() 
		{

			ScreenHeight = 240 * 4;
			ScreenWidth = 320 * 4;
			TargetFps = 60;
			//basic init of raylib
			R.InitWindow(ScreenWidth, ScreenHeight, "Bitcomputing");
			R.SetTargetFPS(TargetFps);

			Setup();

			//Run forever untill close flag
			while (!R.WindowShouldClose())
			{
				Update();
				Draw();
			}
		}

		private void Setup()
		{
		
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