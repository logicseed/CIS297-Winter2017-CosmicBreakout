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

        private int blockTicks = 0;
        private const int MAX_BLOCK_TICKS = 120;

        private List<Wall> walls;
        private List<Ball> balls;
        private List<Paddle> paddles;
        private List<Block> blocks;
        private List<Powerup> powerups;

        public CanvasBitmap SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public List<Wall> Walls { get => walls; set => walls = value; }
        public List<Ball> Balls { get => balls; set => balls = value; }
        public Random Random { get => random; set => random = value; }
        public List<Paddle> Paddles { get => paddles; set => paddles = value; }
        public List<Block> Blocks { get => blocks; set => blocks = value; }
        public List<Powerup> Powerups { get => powerups; set => powerups = value; }

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
            paddles.Add(new Paddle(this, 5f, new Rect(64,462,832,48)));

            blocks = new List<Block>();
            BuildBlockRow();
            BuildBlockRow();
            BuildBlockRow();
            BuildBlockRow();

            powerups = new List<Powerup>();
        }

        private void BuildWalls()
        {
            walls = new List<Wall>();

            walls.Add(new Wall(this, WallSide.Top, new Rect(48, 30, 864, 16)));
            walls.Add(new Wall(this, WallSide.Bottom, new Rect(48, 510, 864, 16)));
            walls.Add(new Wall(this, WallSide.Left, new Rect(48, 46, 16, 464)));
            walls.Add(new Wall(this, WallSide.Right, new Rect(896, 46, 16, 464)));
        }

        private void BuildBlockRow()
        {
            // Lower existing blocks
            foreach (var block in blocks) block.Lower();

            // Create left side
            for (int i = 0; i < 7; i++)
            {
                var location = new Point(112 + (i * 48), 94);
                blocks.Add(new Block(this, location, random.Next(1, 4)));
            }
            // Create right side
            for (int i = 0; i < 7; i++)
            {
                var location = new Point(512 + (i * 48), 94);
                blocks.Add(new Block(this, location, random.Next(1, 4)));
            }
        }

        public void Update()
        {
            // Cleanup game objects
            DestroyGameObjects(balls);
            DestroyGameObjects(paddles);
            DestroyGameObjects(blocks);
            DestroyGameObjects(powerups);

            // Update all game objects
            foreach (var wall in walls) { wall.Update(); }
            foreach (var ball in balls) { ball.Update(); }
            foreach (var paddle in paddles) { paddle.Update(); }
            foreach (var block in blocks) { block.Update(); }
            foreach (var powerup in powerups) { powerup.Update(); }

            // Create next row of blocks
            blockTicks++;
            if (blockTicks >= MAX_BLOCK_TICKS)
            {
                blockTicks = 0;
                BuildBlockRow();
            }
        }

        private void DestroyGameObjects<T>(List<T> gameObjects) where T : Sprite
        {
            var indexesToDelete = new List<int>();
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.DestroyMe) indexesToDelete.Add(gameObjects.IndexOf(gameObject));
            }

            foreach (var index in indexesToDelete)
            {
                gameObjects.RemoveAt(index);
            }
        }

        public void SpawnPowerup(Point location, PowerupType powerupType)
        {
            if (powerupType != PowerupType.None)
            {
                powerups.Add(new Powerup(this, location, 1f, powerupType, 480));
            }
        }

        public void Draw(CanvasSpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rect(0, 0, 960, 540));
            foreach (var wall in walls) { wall.Draw(spriteBatch); }
            foreach (var ball in balls) { ball.Draw(spriteBatch); }
            foreach (var paddle in paddles) { paddle.Draw(spriteBatch); }
            foreach (var block in blocks) { block.Draw(spriteBatch); }
            foreach (var powerup in powerups) { powerup.Draw(spriteBatch); }
        }
    }
}
