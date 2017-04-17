using System;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
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

        public Block(GameManager gameManager, Point location, int collisionsRemaining)
            : base(gameManager, new Rect(location.X, location.Y, 48, 16), CollisionLayer.Block)
        {
            this.collisionsRemaining = collisionsRemaining;
        }

        protected override void SetSpriteSource()
        {
            throw new NotImplementedException();
        }
    }
}
