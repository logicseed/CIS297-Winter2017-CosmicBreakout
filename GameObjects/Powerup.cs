using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Powerup : MoveableSprite
    {
        private PowerupType powerupType;

        public Powerup(GameManager gameManager, Point location, float maximumSpeed, PowerupType powerupType)
            : base(gameManager, new Rect(location.X, location.Y, 48, 16), CollisionLayer.Powerup, maximumSpeed)
        {
            this.powerupType = powerupType;
            SetSpriteSource();
            velocity.Y = maximumSpeed;
        }

        public override void Update()
        {
            base.Update();
        }

        protected override void SetSpriteSource()
        {
            switch (powerupType)
            {
                case PowerupType.WidePaddle:
                    spriteSource = new Rect(16, 32, 48, 16);
                    break;
                case PowerupType.StackPaddle:
                    spriteSource = new Rect(16, 48, 48, 16);
                    break;
                case PowerupType.MultiBall:
                    spriteSource = new Rect(16, 64, 48, 16);
                    break;
                default:
                    spriteSource = new Rect(0, 0, 0, 0);
                    break;
            }
        }
    }
}
