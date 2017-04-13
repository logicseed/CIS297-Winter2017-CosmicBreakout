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
        public Powerup(float maximumSpeed, Image image) 
            : base(maximumSpeed, CollisionLayer.Powerup, image)
        {
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
