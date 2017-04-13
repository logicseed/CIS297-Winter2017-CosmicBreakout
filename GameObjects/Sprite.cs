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
        private Image image;
        private Size size;
        private Point location;
        private bool destroyMe;

        public Image Image { get => image; private set => image = value; }
        public Size Size { get => size; protected set => size = value; }
        public Point Location { get => location; protected set => location = value; }
        public bool DestroyMe { get => destroyMe; protected set => destroyMe = value; }

        public Sprite(Image image)
        {
            this.image = image;
        }

        public virtual void Update(float deltaTime)
        {

        }

        private void UpdateImage()
        {
            // update image from size and location
        }

    }
}
