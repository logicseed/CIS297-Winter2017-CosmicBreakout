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
    public class Powerup : MoveableSprite
    {
        private PowerupType powerupType;
 
        public Powerup(GameManager gameManager, Point location, float maximumSpeed, PowerupType powerupType, int ticksToLive)
            : base(gameManager, new Rect(location, GameSprite.PowerupSize), CollisionLayer.Powerup, maximumSpeed)
        {
            this.powerupType = powerupType;
            SetSpriteSource();
            velocity.Y = maximumSpeed;
        }

        public override void Update()
        {
            base.Update();

            if (CheckCollisions(gameManager.Paddles))
            {
                switch (powerupType)
                {
                    case PowerupType.WidePaddle:
                        gameManager.WidePaddle();
                        break;
                    case PowerupType.StackPaddle:
                        gameManager.StackedPaddle();
                        break;
                    case PowerupType.MultiBall:
                        gameManager.MultiBall();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                CheckCollisions(gameManager.ScreenBounds);
            }
        }

        protected bool CheckCollisions<T>(List<T> sprites) where T : CollidableSprite
        {
            foreach (var sprite in sprites)
            {
                var collisionBounds = new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                collisionBounds.Intersect(sprite.Bounds);
                if (!collisionBounds.IsEmpty)
                {
                    destroyMe = true;
                    return true;
                }
            }
            return false;
        }

        protected override void SetSpriteSource()
        {
            switch (powerupType)
            {
                case PowerupType.WidePaddle:
                    spriteSource = new Rect(SpriteSheet.PowerupWideLocation, SpriteSheet.PowerupWideSize);
                    break;
                case PowerupType.StackPaddle:
                    spriteSource = new Rect(SpriteSheet.PowerupStackLocation, SpriteSheet.PowerupStackSize);
                    break;
                case PowerupType.MultiBall:
                    spriteSource = new Rect(SpriteSheet.PowerupMultiLocation, SpriteSheet.PowerupMultiSize);
                    break;
                default:
                    spriteSource = new Rect(0, 0, 0, 0);
                    break;
            }
        }
    }
}
