using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Paddle : MoveableSprite
    {
        private const double DAMPING = 0.2;

        private Rect path;
        protected Rect paddleCapASource;
        protected Rect paddleCapBSource;

        public Paddle(GameManager gameManager, float maximumSpeed, Rect path)
            : base(gameManager, new Rect(GameSprite.PaddleLocation, GameSprite.PaddleNormalSize), CollisionLayer.Paddle, maximumSpeed)
        {
            this.path = path;
        }

        public override void Update()
        {
            ClampWithinPath();
            base.Update();
        }

        protected override void SetSpriteSource()
        {
            spriteSource = new Rect(SpriteSheet.PaddleMiddleLocation, SpriteSheet.PaddleMiddleSize);
            paddleCapASource = new Rect(SpriteSheet.PaddleLeftLocation, SpriteSheet.PaddleLeftSize);
            paddleCapBSource = new Rect(SpriteSheet.PaddleRightLocation, SpriteSheet.PaddleRightSize);
        }
        public override void Draw(CanvasSpriteBatch spriteBatch)
        {
            spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                new Rect(bounds.X + GameSprite.PaddleCapSize.Width, bounds.Y, bounds.Width - (GameSprite.PaddleCapSize.Width * 2), bounds.Height),
                spriteSource);
            spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                new Rect(bounds.X, bounds.Y, GameSprite.PaddleCapSize.Width, GameSprite.PaddleCapSize.Height),
                paddleCapASource);
            spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                new Rect(bounds.X + (bounds.Width - GameSprite.PaddleCapSize.Width), bounds.Y, GameSprite.PaddleCapSize.Width, GameSprite.PaddleCapSize.Height),
                paddleCapBSource);
        }

        public void Move(double x, double y)
        {
            if (x > DAMPING || x < -DAMPING) location.X += x * maximumSpeed;
            if (y > DAMPING || y < -DAMPING) location.Y -= y * maximumSpeed;
            ClampWithinPath();
        }

        private void ClampWithinPath()
        {
            // Clamp within path
            if (location.X > (path.X + path.Width - bounds.Width))
            {
                location.X = (path.X + path.Width - bounds.Width);
            }

            if (location.X < path.X)
            {
                location.X = path.X;
            }

            if (location.Y > (path.Y + path.Height - bounds.Height))
            {
                location.Y = (path.Y + path.Height - bounds.Height);
            }

            if (location.Y < path.Y)
            {
                location.Y = path.Y;
            }
        }

        public void MakeWide()
        {
            this.bounds.Width = GameSprite.PaddleWideSize.Width;
            this.size.Width = GameSprite.PaddleWideSize.Width;
        }

        public void MakeNormal()
        {
            this.bounds.Width = GameSprite.PaddleNormalSize.Width;
            this.size.Width = GameSprite.PaddleNormalSize.Width;
        }

        public void MakeStack(Point primaryLocation)
        {
            this.location.X = primaryLocation.X;
            this.location.Y = primaryLocation.Y - GameSprite.PaddlePathPrimary.Height;
        }
    }
}
