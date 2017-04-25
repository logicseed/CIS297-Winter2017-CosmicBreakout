using Microsoft.Graphics.Canvas;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public abstract class CollidableSprite : Sprite
    {
        protected Rect bounds;
        protected CollisionLayer collisionLayer;

        public Rect Bounds { get => bounds; protected set => bounds = value; }
        public CollisionLayer CollisionLayer { get => collisionLayer; protected set => collisionLayer = value; }

        public CollidableSprite(GameManager gameManager, Rect bounds, CollisionLayer collisionLayer)
            : base(gameManager, new Point(bounds.X, bounds.Y), new Size(bounds.Width, bounds.Height))
        {
            this.bounds = bounds;
            this.collisionLayer = collisionLayer;
        }

        public override void Update()
        {
            base.Update();
            bounds.X = location.X;
            bounds.Y = location.Y;
        }
    }
}
