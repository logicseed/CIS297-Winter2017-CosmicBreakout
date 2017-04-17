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
    public class Ball : MoveableSprite
    {
        public Ball(GameManager gameManager, float maximumSpeed)
            : base(gameManager, new Rect(462, 262, 16, 16), CollisionLayer.Ball, maximumSpeed)
        {

        }

        public override void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            //foreach()
            base.Update(spriteBatch, deltaTime);
        }

        protected override void SetSpriteSource()
        {
            spriteSource = new Rect(0,0,16,16);
        }
    }
}
