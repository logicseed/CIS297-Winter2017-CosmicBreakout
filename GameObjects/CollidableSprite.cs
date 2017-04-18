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
        protected List<CollisionLayer> collidesWith;

        public Rect Bounds { get => bounds; protected set => bounds = value; }
        public CollisionLayer CollisionLayer { get => collisionLayer; protected set => collisionLayer = value; }
        public List<CollisionLayer> CollidesWith { get => collidesWith; protected set => collidesWith = value; }

        public CollidableSprite(GameManager gameManager, Rect bounds, CollisionLayer collisionLayer)
            : base(gameManager, new Point(bounds.X, bounds.Y))
        {
            this.bounds = bounds;
            this.collisionLayer = collisionLayer;
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
        }

        private void UpdateCollisions(float deltaTime)
        {

        }
    }
}
