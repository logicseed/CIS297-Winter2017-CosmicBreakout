using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Gaming.Input;

namespace GameObjects
{
    public static class Extensions
    {
        public static Point Center(this Rect rect)
        {
            return new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2));
        }

        public static bool Pressed(this GamepadButtons buttons, GamepadButtons button)
        {
            return (buttons & button) == button;
        }

        public static CanvasSpriteBatch CreateSpriteBatch(this CanvasDrawingSession session, bool pixelGraphics = false)
        {
            session.Units = CanvasUnits.Pixels;
            var spriteBatch = session.CreateSpriteBatch(CanvasSpriteSortMode.None,
                CanvasImageInterpolation.NearestNeighbor, CanvasSpriteOptions.ClampToSourceRect);
            return spriteBatch;
        }
    }
}
