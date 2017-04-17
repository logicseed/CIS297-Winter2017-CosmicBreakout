using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class MoveableSprite : CollidableSprite
    {
        private float maximumSpeed;

        public float MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }

        public MoveableSprite(CanvasBitmap spriteSheet, float maximumSpeed, CollisionLayer collisionLayer)
            : base(spriteSheet, collisionLayer)
        {
            this.maximumSpeed = maximumSpeed;
        }

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            base.Update(spriteBatch, deltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {

        }
    }
}
