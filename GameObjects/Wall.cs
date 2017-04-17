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
        private WallSide wallSide;
        private Rect wallCapASource;
        private Rect wallCapBSource;
        private Rect area;

        public Wall(CanvasBitmap spriteSheet, WallSide wallSide, Rect area)
            : base(spriteSheet, CollisionLayer.Wall)
        {
            this.wallSide = wallSide;
            this.area = area;

            switch (wallSide)
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

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            //base.Update(spriteBatch, deltaTime);

            switch (wallSide)
            {
                case WallSide.Top:
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X + 16, area.Y, area.Width - 32, area.Height),
                        spriteSource);
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X, area.Y, 16, 16),
                        wallCapASource);
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X + (area.Width - 16), area.Y, 16, 16),
                        wallCapBSource);
                    break;
                case WallSide.Bottom:
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X + 16, area.Y, area.Width - 32, area.Height),
                        spriteSource);//, Vector4.One, CanvasSpriteFlip.Vertical);
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X, area.Y, 16, 16),
                        wallCapASource);//, new Vector4(0.5f, 0.5f, 0.5f, 1), CanvasSpriteFlip.Vertical);
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X + (area.Width - 16), area.Y, 16, 16),
                        wallCapBSource);//, new Vector4(0.5f, 0.5f, 0.5f, 1), CanvasSpriteFlip.Vertical);
                    break;
                case WallSide.Left:
                case WallSide.Right:
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X, area.Y, area.Width, area.Height - 16),
                        spriteSource);
                    spriteBatch.DrawFromSpriteSheet(spriteSheet,
                        new Rect(area.X, area.Y + (area.Height - 16), 16, 16),
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
