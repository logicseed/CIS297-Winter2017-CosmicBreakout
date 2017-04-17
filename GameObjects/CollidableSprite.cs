using Microsoft.Graphics.Canvas;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class CollidableSprite : Sprite
    {
        private Rect bounds;
        private CollisionLayer collisionLayer;
        private List<CollisionLayer> collidesWith;

        public Rect Bounds { get => bounds; protected set => bounds = value; }
        public CollisionLayer CollisionLayer { get => collisionLayer; protected set => collisionLayer = value; }
        public List<CollisionLayer> CollidesWith { get => collidesWith; protected set => collidesWith = value; }

        public CollidableSprite(CanvasBitmap spriteSheet, CollisionLayer collisionLayer)
            : base (spriteSheet)
        {
            //this.bounds = new Rect(new Point(0, 0), new Point(Image.ActualWidth, Image.ActualHeight));
        }

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            base.Update(spriteBatch, deltaTime);
        }

        private void UpdateCollisions(float deltaTime)
        {

        }
    }
}
