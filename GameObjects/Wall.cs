using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Wall : CollidableSprite
    {
        public Wall(CollisionLayer collisionLayer, Image image)
            : base(collisionLayer, image)
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
