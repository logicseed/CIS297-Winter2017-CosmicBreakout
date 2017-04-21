using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace GameObjects
{
    public class Destroy : CollidableSprite
    {
        public Destroy(GameManager gameManager, Rect bounds) : 
            base(gameManager, bounds, CollisionLayer.Destroy)
        {
        }

        protected override void SetSpriteSource()
        {
        }
    }
}
