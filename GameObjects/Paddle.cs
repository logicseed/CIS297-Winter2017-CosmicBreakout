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
    public class Paddle : MoveableSprite
    {
        public Paddle(GameManager gameManager, float maximumSpeed)
            : base(gameManager, new Rect(440, 462, 80, 16), CollisionLayer.Paddle, maximumSpeed)
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

        private void UpdateInput(float deltaTime)
        {

        }
    }
}
