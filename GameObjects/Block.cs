using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Block : CollidableSprite
    {
        private int collisionsRemaining;
        private bool dropsPowerup;
        private PowerupType dropsPowerupType;

        public int CollisionsRemaining { get => collisionsRemaining; set => collisionsRemaining = value; }
        public bool DropsPowerup { get => dropsPowerup; set => dropsPowerup = value; }
        public PowerupType DropsPowerupType { get => dropsPowerupType; set => dropsPowerupType = value; }

        public Block(int collisionsRemaining, CollisionLayer collisionLayer, Image image)
            : base(collisionLayer, image)
        {
            this.collisionsRemaining = collisionsRemaining;
        }
    }
}
