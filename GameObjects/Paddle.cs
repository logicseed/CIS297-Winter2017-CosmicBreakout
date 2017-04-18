﻿using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public class Paddle : MoveableSprite
    {
        private Rect path;

        public Paddle(GameManager gameManager, float maximumSpeed, Rect path)
            : base(gameManager, new Rect(440, 462, 80, 16), CollisionLayer.Paddle, maximumSpeed)
        {
            this.path = path;
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
        }

        protected override void SetSpriteSource()
        {
            spriteSource = new Rect(16, 0, 80, 16);
        }

        public void Move(double x, double y)
        {
            if (x > 0.2 || x < -0.2) location.X += x * maximumSpeed;
            if (y > 0.2 || y < -0.2) location.Y -= y * maximumSpeed;

            // Clamp within path
            if (location.X > (path.X + path.Width - bounds.Width))
            {
                location.X = (path.X + path.Width - bounds.Width);
            }

            if (location.X < path.X)
            {
                location.X = path.X;
            }

            if (location.Y > (path.Y + path.Height - bounds.Height))
            {
                location.Y = (path.Y + path.Height - bounds.Height);
            }

            if (location.Y < path.Y)
            {
                location.Y = path.Y;
            }
        }
    }
}
