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
    public class Sprite
    {
        protected CanvasBitmap spriteSheet;
        protected Rect spriteSource;

        private Image image;
        private Size size;
        private Point location;
        private bool destroyMe;

        public Image Image { get => image; private set => image = value; }
        public Size Size { get => size; protected set => size = value; }
        public Point Location { get => location; protected set => location = value; }
        public bool DestroyMe { get => destroyMe; protected set => destroyMe = value; }

        public Sprite(CanvasBitmap spriteSheet)
        {
            this.spriteSheet = spriteSheet;
        }

        public virtual void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {

        }

        private void UpdateImage()
        {
            // update image from size and location
        }

    }
}
