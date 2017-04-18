using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Wall : CollidableSprite
    {
        protected WallSide wallSide;
        protected Rect wallCapASource;
        protected Rect wallCapBSource;

        public Wall(GameManager gameManager, WallSide wallSide, Rect bounds)
            : base(gameManager, bounds, CollisionLayer.Wall)
        {
            this.wallSide = wallSide;
            SetSpriteSource();
        }

        protected override void SetSpriteSource()
        {
            var wall = this as Wall;

            switch (wall.wallSide)
            {
                case WallSide.Top:
                case WallSide.Bottom:
                    spriteSource = new Rect(32, 16, 48, 16);
                    wallCapASource = new Rect(16, 16, 16, 16);
                    wallCapBSource = new Rect(80, 16, 16, 16);
                    break;
                case WallSide.Left:
                case WallSide.Right:
                    spriteSource = new Rect(0, 16, 16, 48);
                    wallCapASource = new Rect(0, 64, 16, 16);
                    wallCapBSource = new Rect(0, 64, 16, 16);
                    break;
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(CanvasSpriteBatch spriteBatch)
        {
            var wall = this as Wall;

            switch (wall.wallSide)
            {
                case WallSide.Top:
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X + 16, bounds.Y, bounds.Width - 32, bounds.Height),
                        spriteSource);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X, bounds.Y, 16, 16),
                        wallCapASource);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X + (bounds.Width - 16), bounds.Y, 16, 16),
                        wallCapBSource);
                    break;
                case WallSide.Bottom:
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X + 16, bounds.Y, bounds.Width - 32, bounds.Height),
                        spriteSource);//, Vector4.One, CanvasSpriteFlip.Vertical);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X, bounds.Y, 16, 16),
                        wallCapASource);//, new Vector4(0.5f, 0.5f, 0.5f, 1), CanvasSpriteFlip.Vertical);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X + (bounds.Width - 16), bounds.Y, 16, 16),
                        wallCapBSource);//, new Vector4(0.5f, 0.5f, 0.5f, 1), CanvasSpriteFlip.Vertical);
                    break;
                case WallSide.Left:
                case WallSide.Right:
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height - 16),
                        spriteSource);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X, bounds.Y + (bounds.Height - 16), 16, 16),
                        wallCapASource);
                    break;
            }
        }
    }

    public enum WallSide
    {
        Top,
        Bottom,
        Left,
        Right
    }
}
