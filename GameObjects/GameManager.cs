using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.Foundation;

namespace GameObjects
{
    public class GameManager
    {
        private CanvasBitmap background;
        private CanvasBitmap spriteSheet;

        private List<Wall> walls;
        private List<Ball> balls;

        public CanvasBitmap SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public List<Wall> Walls { get => walls; set => walls = value; }
        public List<Ball> Balls { get => balls; set => balls = value; }

        public GameManager(CanvasBitmap background, CanvasBitmap spriteSheet)
        {
            this.background = background;
            this.spriteSheet = spriteSheet;

            BuildWalls();
            balls = new List<Ball>();
            balls.Add(new Ball(this, .5f));
        }

        private void BuildWalls()
        {
            walls = new List<Wall>();

            walls.Add(new Wall(this, WallSide.Top, new Rect(48, 30, 864, 16)));
            walls.Add(new Wall(this, WallSide.Bottom, new Rect(48, 510, 864, 16)));
            walls.Add(new Wall(this, WallSide.Left, new Rect(48, 46, 16, 464)));
            walls.Add(new Wall(this, WallSide.Right, new Rect(896, 46, 16, 464)));
        }

        public void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            spriteBatch.Draw(background, new Rect(0,0,960,540));
            foreach (var wall in walls) { wall.Update(spriteBatch, deltaTime); }
            foreach (var ball in balls) { ball.Update(spriteBatch, deltaTime); }
            //spriteBatch.DrawFromSpriteSheet(spriteSheet, new Rect(), new Rect());
        }


    }
}
