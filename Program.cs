using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Greed.Game.Casting;
using Greed.Game.Directing;
using Greed.Game.Services;


namespace Greed
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        public static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        public static int CELL_SIZE = 30;
        private static int FONT_SIZE = 30;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed";
        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_MINERALS = 40;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();


            // create the score
            Actor score = new Actor();
            score.SetText("");
            score.SetFontSize(FONT_SIZE);
            score.SetColor(WHITE);
            score.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("score", score);

            
            // create the framerate



            // create the player
            Actor player = new Actor();
            player.SetText("A");
            player.SetFontSize(FONT_SIZE);
            player.SetColor(WHITE);
            player.SetPosition(new Point(MAX_X / 2, MAX_Y - CELL_SIZE * 5));
            cast.AddActor("player", player);

        
            // create the minerals
            Random random = new Random();
            for (int i = 0; i < DEFAULT_MINERALS; i++)
            {
                string text;
                int point = 0;
                int type = random.Next(0, 3);

                if (type == 0)
                {
                    text = "*";
                    point = 100;
                }
                else
                {
                    text = "o";
                    point = -50;
                }
                
                

                int x = random.Next(1, COLS);
                int y = random.Next(1, ROWS); // TODO: start at top of the screen
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);

                int vx = 0;
                int vy = 1;
                Point velocity = new Point(vx, vy);
                velocity = velocity.Scale(CELL_SIZE);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Mineral mineral = new Mineral(point);
                mineral.SetText(text);
                mineral.SetFontSize(FONT_SIZE);
                mineral.SetColor(color);
                mineral.SetPosition(position);
                mineral.SetVelocity(velocity);
                cast.AddActor("minerals", mineral);
            }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService 
                = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}