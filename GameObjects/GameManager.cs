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

        public int score = 0;
        public bool gameOver = false;
        private int blockTicks = 0;
        private int stackTicks = 0;
        private int wideTicks = 0;
        private bool isPaddleWide = false;
        private bool isPaddleStacked = false;

        private const int MAX_BLOCK_TICKS = 900;
        private const int RANDOM_SEED = 23;
        private const float BALL_SPEED = 6f;
        private const float PADDLE_SPEED = 10f;
        private const float POWERUP_SPEED = 3f;
        private const int STACK_TICKS = 300;
        private const int WIDE_TICKS = 300;

        private List<Wall> walls;
        private List<Ball> balls;
        private List<Paddle> paddles;
        private List<Block> blocks;
        private List<Powerup> powerups;
        private List<CollidableSprite> screenBounds;
        private List<CollidableSprite> blockBounds;

        public CanvasBitmap SpriteSheet { get => spriteSheet; set => spriteSheet = value; }
        public List<Wall> Walls { get => walls; set => walls = value; }
        public List<Ball> Balls { get => balls; set => balls = value; }
        public Random Random { get => random; set => random = value; }
        public List<Paddle> Paddles { get => paddles; set => paddles = value; }
        public List<Block> Blocks { get => blocks; set => blocks = value; }
        public List<Powerup> Powerups { get => powerups; set => powerups = value; }
        public List<CollidableSprite> ScreenBounds { get => screenBounds; set => screenBounds = value; }
        public List<CollidableSprite> BlockBounds { get => blockBounds; set => blockBounds = value; }

        public GameManager(CanvasBitmap background, CanvasBitmap spriteSheet)
        {
            this.random = new Random(RANDOM_SEED);
            this.background = background;
            this.spriteSheet = spriteSheet;

            InitializeCollections();

            BuildWalls();
            BuildBounds();
            MultiBall();
            SpawnPaddle();
            BuildBlockRow();
            BuildBlockRow();
            BuildBlockRow();
            BuildBlockRow();
        }

        private void InitializeCollections()
        {
            walls = new List<Wall>();
            screenBounds = new List<CollidableSprite>();
            blockBounds = new List<CollidableSprite>();
            paddles = new List<Paddle>();
            balls = new List<Ball>();
            blocks = new List<Block>();
            powerups = new List<Powerup>();
        }

        private void SpawnPaddle(bool isStack = false)
        {
            if (paddles.Count >= 2) return;

            if (isStack)
            {
                paddles.Add(new Paddle(this, PADDLE_SPEED, GameSprite.PaddlePathStacked));
                paddles.Last().MakeStack(new Point(paddles[0].Bounds.X, paddles[0].Bounds.X));
            }
            else
            {
                paddles.Add(new Paddle(this, PADDLE_SPEED, GameSprite.PaddlePathPrimary));
            }
        }

        private void BuildWalls()
        {


            walls.Add(new Wall(this, WallSide.Top, new Rect(GameSprite.WallTopLocation, GameSprite.WallTopSize)));
            walls.Add(new Wall(this, WallSide.Left, new Rect(GameSprite.WallSideLeftLocation, GameSprite.WallSideSize)));
            walls.Add(new Wall(this, WallSide.Right, new Rect(GameSprite.WallSideRightLocation, GameSprite.WallSideSize)));
        }

        private void BuildBounds()
        {
            screenBounds.Add(new CollidableSprite(this, new Rect(96, 1060, 1920, 16), CollisionLayer.Destroy));
            blockBounds.Add(new CollidableSprite(this, new Rect(96, 800, 1920, 16), CollisionLayer.MaxBlocks));
        }

        private void BuildBlockRow()
        {
            // Lower existing blocks
            foreach (var block in blocks) block.Lower();

            // Create left side
            for (int i = 0; i < 7; i++)
            {
                var location = new Point(224 + (i * 96), 188);
                int rand = random.Next(1, 4);

                switch (rand)
                {
                    case 1:
                        blocks.Add(new Block(this, location, rand, BlockType.OneHit));
                        break;
                    case 2:
                        blocks.Add(new Block(this, location, rand, BlockType.TwoHit));
                        break;
                    case 3:
                        blocks.Add(new Block(this, location, rand, BlockType.ThreeHit));
                        break;
                }
            }
            // Create right side
            for (int i = 0; i < 7; i++)
            {
                var location = new Point(1024 + (i * 96), 188);
                int rand = random.Next(1, 4);

                switch (rand)
                {
                    case 1:
                        blocks.Add(new Block(this, location, rand, BlockType.OneHit));
                        break;
                    case 2:
                        blocks.Add(new Block(this, location, rand, BlockType.TwoHit));
                        break;
                    case 3:
                        blocks.Add(new Block(this, location, rand, BlockType.ThreeHit));
                        break;
                }
            }
        }

        public void Update()
        {
            BallsInPlay();
            CalculateScore(blocks);

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
            blockTicks += (balls.Count / 6) + 1;
            if (blockTicks >= MAX_BLOCK_TICKS)
            {
                blockTicks = 0;
                BuildBlockRow();
            }

            CheckStackPaddle();
            CheckWidePaddle();
        }

        private void CalculateScore(List<Block> blocks)
        {
            foreach (var block in blocks)
            {
                if (block.DestroyMe)
                {
                    switch (block.Type)
                    {
                        case BlockType.OneHit:
                            score += 1;
                            break;
                        case BlockType.TwoHit:
                            score += 3;
                            break;
                        case BlockType.ThreeHit:
                            score += 7;
                            break;
                    }
                }
            }
        }

        private void DestroyGameObjects<T>(List<T> gameObjects) where T : Sprite
        {
            var thisLock = new Object();
            lock (thisLock)
            {
                var indexesToDelete = new List<int>();
                foreach (var gameObject in gameObjects)
                {
                    if (gameObject.DestroyMe) indexesToDelete.Add(gameObjects.IndexOf(gameObject));
                }

                foreach (var index in indexesToDelete)
                {
                    if (index < gameObjects.Count) gameObjects.RemoveAt(index);
                }
            }
        }

        public void SpawnPowerup(Point location, PowerupType powerupType)
        {
            if (powerupType != PowerupType.None)
            {
                powerups.Add(new Powerup(this, location, POWERUP_SPEED, powerupType));
            }
        }

        public void Draw(CanvasSpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, GameSprite.Background);
            foreach (var wall in walls) { wall.Draw(spriteBatch); }
            foreach (var ball in balls) { ball.Draw(spriteBatch); }
            foreach (var paddle in paddles) { paddle.Draw(spriteBatch); }
            foreach (var block in blocks) { block.Draw(spriteBatch); }
            foreach (var powerup in powerups) { powerup.Draw(spriteBatch); }
        }

        public void BallsInPlay()
        {
            if (Balls.Count <= 0)
                gameOver = true;
        }

        public void MultiBall()
        {
            balls.Add(new Ball(this, BALL_SPEED));
            balls.Add(new Ball(this, BALL_SPEED));
            balls.Add(new Ball(this, BALL_SPEED));
            balls.Add(new Ball(this, BALL_SPEED));
            balls.Add(new Ball(this, BALL_SPEED));
            balls.Add(new Ball(this, BALL_SPEED));
            //balls.Add(new Ball(this, BALL_SPEED));
            //balls.Add(new Ball(this, BALL_SPEED));
        }

        public void WidePaddle()
        {
            foreach (var paddle in paddles)
            {
                paddle.MakeWide();
            }
            isPaddleWide = true;
            wideTicks = 0;
        }

        public void CheckWidePaddle()
        {
            if (isPaddleWide)
            {
                wideTicks++;
                if (wideTicks >= WIDE_TICKS)
                {
                    wideTicks = 0;
                    isPaddleWide = false;
                    foreach (var paddle in paddles)
                    {
                        paddle.MakeNormal();
                    }
                }
            }
        }

        public void StackedPaddle()
        {
            SpawnPaddle(true);
            isPaddleStacked = true;
            stackTicks = 0;
            if (isPaddleWide)
            {
                foreach (var paddle in paddles)
                {
                    paddle.MakeWide();
                }
            }
        }

        public void CheckStackPaddle()
        {
            if (isPaddleStacked)
            {
                stackTicks++;
                if (stackTicks >= STACK_TICKS)
                {
                    stackTicks = 0;
                    isPaddleStacked = false;
                    paddles.RemoveAt(1);
                }
            }
        }
    }
}
