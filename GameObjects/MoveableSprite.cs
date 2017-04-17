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
    public abstract class MoveableSprite : CollidableSprite
    {
        private float maximumSpeed;
        protected Vector2 velocity;

        public float MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }

        public MoveableSprite(GameManager gameManager, Rect bounds, CollisionLayer collisionLayer, float maximumSpeed)
            : base (gameManager, bounds, collisionLayer)
        {
            this.maximumSpeed = maximumSpeed;
            this.velocity = new Vector2(maximumSpeed, maximumSpeed);
        }

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            location = new Point(location.X + velocity.X, location.Y + velocity.Y);
            bounds.X = location.X;
            bounds.Y = location.Y;

            foreach(var wall in gameManager.Walls)
            {
                var tempBounds = new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                tempBounds.Intersect(wall.Bounds);
                if (!tempBounds.IsEmpty)
                {
                    if (tempBounds.Center().X > bounds.Center().X)
                    {
                        velocity.X = velocity.X * -1;
                    }
                    if (tempBounds.Center().Y > bounds.Center().Y)
                    {
                        velocity.Y = velocity.Y * -1;
                    }
                }
            }

            base.Update(spriteBatch, deltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {

        }
    }
}
