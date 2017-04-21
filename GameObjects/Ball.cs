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
    public class Ball : MoveableSprite
    {
        public Ball(GameManager gameManager, float maximumSpeed)
            : base(gameManager, new Rect(462, 262, 16, 16), CollisionLayer.Ball, maximumSpeed)
        {
            var rads = gameManager.Random.NextDouble() * 2 * Math.PI;

            velocity = new Vector2(
                (float)Math.Cos(rads) * maximumSpeed,
                (float)Math.Sin(rads) * maximumSpeed
                );
        }

        public override void Update()
        {
            base.Update();
            CheckCollisions(gameManager.Walls);
            CheckCollisions(gameManager.Paddles);
            CheckCollisions(gameManager.Blocks);
            CheckCollisions(gameManager.OutOfBounds);
        }

        protected void CheckCollisions<T>(List<T> sprites) where T : CollidableSprite
        {
            var hasCollided = false;
            foreach (var sprite in sprites)
            {
                if (hasCollided) break;

                var collisionBounds = new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                collisionBounds.Intersect(sprite.Bounds);
                if (!collisionBounds.IsEmpty)
                {
                    hasCollided = true;

                    // Collided on left or right
                    if (collisionBounds.Center().X != bounds.Center().X)
                    {
                        velocity.X *= -1;
                        // Move out of collider
                        if (collisionBounds.Center().X < bounds.Center().X
                            && sprite.CollisionLayer != CollisionLayer.Paddle)
                        {
                            location.X += collisionBounds.Width;
                        }
                        else
                        {
                            location.X -= collisionBounds.Width;
                        }
                    }
                    // Collided on top or bottom
                    if (collisionBounds.Center().Y != bounds.Center().Y)
                    {
                        velocity.Y *= -1;
                        // Move out of collider
                        if (collisionBounds.Center().Y < bounds.Center().Y
                             && sprite.CollisionLayer != CollisionLayer.Paddle)
                        {
                            location.Y += collisionBounds.Height;
                        }
                        else
                        {
                            location.Y -= collisionBounds.Height;
                        }
                    }

                    if (sprite.CollisionLayer == CollisionLayer.Block)
                    {
                        (sprite as Block).Hit();
                    }

                    if(sprite.CollisionLayer == CollisionLayer.Destroy)
                    {
                        destroyMe = true;
                    }
                }
            }
        }

        protected override void SetSpriteSource()
        {
            spriteSource = new Rect(0,0,16,16);
        }
    }
}
