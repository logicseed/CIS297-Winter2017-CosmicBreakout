using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class MoveableSprite : CollidableSprite
    {
        private float maximumSpeed;

        public float MaximumSpeed { get => maximumSpeed; set => maximumSpeed = value; }

        public MoveableSprite(float maximumSpeed, CollisionLayer collisionLayer, Image image)
            : base(collisionLayer, image)
        {
            this.maximumSpeed = maximumSpeed;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        private void UpdateMovement(float deltaTime)
        {

        }
    }
}
