using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameObjects
{
    public abstract class Sprite
    {
        protected GameManager gameManager;

        protected Rect spriteSource;
        protected Point location;
        protected bool destroyMe;

        public Point Location { get => location; protected set => location = value; }
        public bool DestroyMe { get => destroyMe; protected set => destroyMe = value; }

        public Sprite(GameManager gameManager, Point location)
        {
            this.gameManager = gameManager;
            this.location = location;
            SetSpriteSource();
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(CanvasSpriteBatch spriteBatch)
        {
            spriteBatch.DrawFromSpriteSheet(gameManager.SpriteSheet,
                        new Rect(location.X, location.Y, spriteSource.Width, spriteSource.Height),
                        spriteSource);
        }

        protected abstract void SetSpriteSource();
    }
}
