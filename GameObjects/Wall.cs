using Microsoft.Graphics.Canvas;
using Windows.Foundation;

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
                    spriteSource = new Rect(SpriteSheet.WallTopMiddleLocation, SpriteSheet.WallTopMiddleSize);
                    wallCapASource = new Rect(SpriteSheet.WallTopLeftLocation, SpriteSheet.WallTopLeftSize);
                    wallCapBSource = new Rect(SpriteSheet.WallTopRightLocation, SpriteSheet.WallTopRightSize);
                    break;
                case WallSide.Left:
                case WallSide.Right:
                    spriteSource = new Rect(SpriteSheet.WallSideMiddleLocation, SpriteSheet.WallSideMiddleSize);
                    wallCapASource = new Rect(SpriteSheet.WallSideBottomLocation, SpriteSheet.WallSideBottomSize);
                    wallCapBSource = new Rect(SpriteSheet.WallSideBottomLocation, SpriteSheet.WallSideBottomSize);
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
                        new Rect(bounds.X + GameSprite.WallTopCapSize.Width, bounds.Y, bounds.Width - GameSprite.WallTopCapSize.Width, bounds.Height),
                        spriteSource);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X, bounds.Y, GameSprite.WallTopCapSize.Width, GameSprite.WallTopCapSize.Height),
                        wallCapASource);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X + (bounds.Width - GameSprite.WallTopCapSize.Width), bounds.Y, GameSprite.WallTopCapSize.Width, GameSprite.WallTopCapSize.Height),
                        wallCapBSource);
                    break;
                case WallSide.Left:
                case WallSide.Right:
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height - GameSprite.WallSideCapSize.Height),
                        spriteSource);
                    spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(bounds.X, bounds.Y + (bounds.Height - GameSprite.WallSideCapSize.Height), GameSprite.WallSideCapSize.Width, GameSprite.WallSideCapSize.Height),
                        wallCapASource);
                    break;
                default:
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
