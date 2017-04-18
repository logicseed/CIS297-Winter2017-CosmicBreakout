using System;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Block : CollidableSprite
    {
        private int collisionsRemaining;
        private PowerupType powerupType = PowerupType.None;

        public int CollisionsRemaining { get => collisionsRemaining; set => collisionsRemaining = value; }
        public PowerupType PowerupType { get => powerupType; set => powerupType = value; }

        public Block(GameManager gameManager, Point location, int collisionsRemaining)
            : base(gameManager, new Rect(location.X, location.Y, 48, 16), CollisionLayer.Block)
        {
            this.collisionsRemaining = collisionsRemaining;

            if ((gameManager.Random.NextDouble() < 0.2))
            {
                powerupType = (PowerupType)gameManager.Random.Next(1, 4);
            }

            SetSpriteSource();
        }

        protected override void SetSpriteSource()
        {
            switch (collisionsRemaining)
            {
                case 2:
                    spriteSource = new Rect(64, 32, 48, 16);
                    break;
                case 3:
                    spriteSource = new Rect(64, 64, 48, 16);
                    break;
                default:
                    spriteSource = new Rect(64, 48, 48, 16);
                    break;
            }
        }

        public void Hit()
        {
            collisionsRemaining--;
            SetSpriteSource();
            if (collisionsRemaining <= 0)
            {
                destroyMe = true;
                gameManager.SpawnPowerup(location, powerupType);
            }
        }

        public void Lower()
        {
            location.Y += bounds.Height;
        }
    }
}
