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
        protected float maximumSpeed;
        protected Vector2 velocity;

        public MoveableSprite(GameManager gameManager, Rect bounds, CollisionLayer collisionLayer, float maximumSpeed)
            : base (gameManager, bounds, collisionLayer)
        {
            this.maximumSpeed = maximumSpeed;
            //this.velocity = new Vector2(maximumSpeed, maximumSpeed);
        }

        public override void Update()
        {
            location = new Point(location.X + velocity.X, location.Y + velocity.Y);
            bounds.X = location.X;
            bounds.Y = location.Y;

            base.Update();
        }
    }
}
