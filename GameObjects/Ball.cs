using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Ball : MoveableSprite
    {
        public Ball(float maximumSpeed, Image image)
            : base(maximumSpeed, CollisionLayer.Ball, image)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
