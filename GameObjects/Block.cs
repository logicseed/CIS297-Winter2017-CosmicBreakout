using System;
using Microsoft.Graphics.Canvas;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using System.Collections.Generic;

namespace GameObjects
{
    public class Block : CollidableSprite
    {
        private const double POWERUP_CHANCE = 1.0;

        private BlockType type;
        private int collisionsRemaining;
        private PowerupType powerupType = PowerupType.None;

        public BlockType Type { get => type; set => type = value; }
        public int CollisionsRemaining { get => collisionsRemaining; set => collisionsRemaining = value; }
        public PowerupType PowerupType { get => powerupType; set => powerupType = value; }

        public Block(GameManager gameManager, Point location, int collisionsRemaining, BlockType type)
            : base(gameManager, new Rect(location, GameSprite.BlockSize), CollisionLayer.Block)
        {
            this.type = type;
            this.collisionsRemaining = collisionsRemaining;

            if ((gameManager.Random.NextDouble() < POWERUP_CHANCE))
            {
                powerupType = (PowerupType)gameManager.Random.Next(1, (int)PowerupType.COUNT);
            }

            SetSpriteSource();
        }

        public override void Update()
        {
            base.Update();
            CheckCollisions(gameManager.BlockBounds);
        }

        protected override void SetSpriteSource()
        {
            switch (collisionsRemaining)
            {
                case 2:
                    spriteSource = new Rect(SpriteSheet.BlockDoubleLocation, SpriteSheet.BlockSize);
                    break;
                case 3:
                    spriteSource = new Rect(SpriteSheet.BlockTripleLocation, SpriteSheet.BlockSize);
                    break;
                default:
                    spriteSource = new Rect(SpriteSheet.BlockSingleLocation, SpriteSheet.BlockSize);
                    break;
            }
        }

        protected void CheckCollisions<T>(List<T> sprites) where T : CollidableSprite
        {
            foreach (var sprite in sprites)
            {
                var collisionBounds = new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                collisionBounds.Intersect(sprite.Bounds);
                if (!collisionBounds.IsEmpty)
                {
                    if (sprite.CollisionLayer == CollisionLayer.MaxBlocks)
                    {
                        gameManager.gameOver = true;
                    }
                }
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
