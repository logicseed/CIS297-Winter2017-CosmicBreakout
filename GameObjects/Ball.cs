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
        private bool hasExploded;

        private const float BLOW_RADIUS = 100.0f;
        private bool isExploding;

        public Ball(GameManager gameManager, float maximumSpeed)
            : base(gameManager, new Rect(GameSprite.BallLocation, GameSprite.BallSize), CollisionLayer.Ball, maximumSpeed)
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
            CheckCollisions(gameManager.ScreenBounds);
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
                    var hasRebounded = false;

                    // Collided on left or right
                    if (collisionBounds.Center().X != bounds.Center().X)
                    {
                        velocity.X *= -1;
                        hasRebounded = true;
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
                        if (sprite.CollisionLayer == CollisionLayer.Paddle)
                        {
                            var magnitude = velocity.Length();
                            var angle = (Math.PI / 180) * (90 - (((bounds.Center().X - sprite.Bounds.Center().X)/(0.5 * sprite.Bounds.Width)) * 80));
                            velocity.X = (float)Math.Cos(angle) * magnitude;
                            velocity.Y = -(float)Math.Sin(angle) * magnitude;
                        }
                        else
                        {
                            if (!hasRebounded) velocity.Y *= -1;
                        }
                        
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
                        if (isExploding) Explode();
                        (sprite as Block).Hit();
                    }

                    if(sprite.CollisionLayer == CollisionLayer.Destroy)
                    {
                        destroyMe = true;
                    }
                }
            }
        }

        public override void Draw(CanvasSpriteBatch spriteBatch)
        {
            if (hasExploded)
            {
                spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(
                            location.X - (BLOW_RADIUS / 2), 
                            location.Y - (BLOW_RADIUS / 2), 
                            size.Width + BLOW_RADIUS, 
                            size.Height + BLOW_RADIUS),
                        spriteSource);
                hasExploded = false;
            }
            else
            {
                spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(location.X, location.Y, size.Width, size.Height),
                        spriteSource);
            }
        }

        protected override void SetSpriteSource()
        {
            spriteSource = new Rect(SpriteSheet.BallLocation, SpriteSheet.BallSize);
        }

        public void Explode()
        {
            isExploding = true;
            hasExploded = false;
            foreach (var block in gameManager.Blocks)
            {
                var collisionBounds = new Rect(
                    bounds.X - (BLOW_RADIUS / 2), 
                    bounds.Y - (BLOW_RADIUS / 2), 
                    bounds.Width + BLOW_RADIUS, 
                    bounds.Height + BLOW_RADIUS);

                collisionBounds.Intersect(block.Bounds);
                if (!collisionBounds.IsEmpty)
                {
                    if (block.CollisionLayer == CollisionLayer.Block)
                    {
                        hasExploded = true;
                        isExploding = false;
                        block.DestroyMe = true;
                    }
                }
            }
        }
    }
}
