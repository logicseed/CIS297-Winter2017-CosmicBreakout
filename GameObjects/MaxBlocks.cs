using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace GameObjects
{
    public class MaxBlocks : CollidableSprite
    {
        public MaxBlocks(GameManager gameManager, Rect bounds) 
            : base(gameManager, bounds, CollisionLayer.MaxBlocks)
        {
        }

        protected override void SetSpriteSource()
        {
        }
    }
}
