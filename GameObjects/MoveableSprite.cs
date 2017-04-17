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

            base.Update(spriteBatch, deltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {

        }
    }
}
