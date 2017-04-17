using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Paddle : MoveableSprite
    {
        public Paddle(CanvasBitmap spriteSheet, float maximumSpeed)
            : base(spriteSheet, maximumSpeed, CollisionLayer.Paddle)
        {

        }

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            base.Update(spriteBatch, deltaTime);
        }

        private void UpdateInput(float deltaTime)
        {

        }
    }
}
