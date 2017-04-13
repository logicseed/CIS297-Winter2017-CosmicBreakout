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
        public Paddle(float maximumSpeed, Image image)
            : base(maximumSpeed, CollisionLayer.Paddle, image)
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        private void UpdateInput(float deltaTime)
        {

        }
    }
}
