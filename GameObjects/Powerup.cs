using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Powerup : MoveableSprite
    {
        public Powerup(GameManager gameManager, Point location, float maximumSpeed)
            : base(gameManager, new Rect(location.X, location.Y, 48, 16), CollisionLayer.Powerup, maximumSpeed)
        {
        }

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            base.Update(spriteBatch, deltaTime);
        }

        protected override void SetSpriteSource()
        {
            throw new NotImplementedException();
        }
    }
}
