using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Powerup : MoveableSprite
    {
        public Powerup(CanvasBitmap spriteSheet, float maximumSpeed)
            : base(spriteSheet, maximumSpeed, CollisionLayer.Powerup)
        {
        }

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            base.Update(spriteBatch, deltaTime);
        }
    }
}
