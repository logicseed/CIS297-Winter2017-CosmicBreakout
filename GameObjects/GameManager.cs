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
        private Random random;

        private List<Wall> walls;
        private List<Ball> balls;
        private List<Paddle> paddles;

        public CanvasBitmap SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public List<Wall> Walls { get => walls; set => walls = value; }
        public List<Ball> Balls { get => balls; set => balls = value; }
        public Random Random { get => random; set => random = value; }
        public List<Paddle> Paddles { get => paddles; set => paddles = value; }

        public GameManager(CanvasBitmap background, CanvasBitmap spriteSheet)
        {
            this.random = new Random(23);
            this.background = background;
            this.spriteSheet = spriteSheet;

            BuildWalls();
            balls = new List<Ball>();
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));
            balls.Add(new Ball(this, 3f));

            paddles = new List<Paddle>();
            paddles.Add(new Paddle(this, 3f, new Rect(64,462,832,48)));
        }

        private void BuildWalls()
        {
            walls = new List<Wall>();

            walls.Add(new Wall(this, WallSide.Top, new Rect(48, 30, 864, 16)));
            walls.Add(new Wall(this, WallSide.Bottom, new Rect(48, 510, 864, 16)));
            walls.Add(new Wall(this, WallSide.Left, new Rect(48, 46, 16, 464)));
            walls.Add(new Wall(this, WallSide.Right, new Rect(896, 46, 16, 464)));
        }

        public void Update(double deltaTime)
        {
            foreach (var wall in walls) { wall.Update(deltaTime); }
            foreach (var ball in balls) { ball.Update(deltaTime); }
            foreach (var paddle in paddles) { paddle.Update(deltaTime); }
        }

        public void Draw(CanvasSpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rect(0, 0, 960, 540));
            foreach (var wall in walls) { wall.Draw(spriteBatch); }
            foreach (var ball in balls) { ball.Draw(spriteBatch); }
            foreach (var paddle in paddles) { paddle.Draw(spriteBatch); }
        }
    }
}
