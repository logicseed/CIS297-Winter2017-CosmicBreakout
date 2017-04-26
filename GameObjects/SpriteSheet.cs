using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace GameObjects
{
    /// <summary>
    /// Size and location of sprites on the sprite sheet.
    /// </summary>
    public static class SpriteSheet
    {
        public static Point BallLocation => new Point(0,0);
        public static Size BallSize => new Size(16,16);

        public static Point BlockSingleLocation => new Point(64, 48);
        public static Point BlockDoubleLocation => new Point(64, 32);
        public static Point BlockTripleLocation => new Point(64, 64);
        public static Size BlockSize => new Size(48, 16);

        public static Point PaddleLeftLocation => new Point(16, 0);
        public static Size PaddleLeftSize => new Size(16, 16);

        public static Point PaddleMiddleLocation => new Point(32, 0);
        public static Size PaddleMiddleSize => new Size(48, 16);

        public static Point PaddleRightLocation => new Point(80, 0);
        public static Size PaddleRightSize => new Size(16, 16);

        public static Point PowerupWideLocation => new Point(16, 32);
        public static Size PowerupWideSize => new Size(48, 16);

        public static Point PowerupStackLocation => new Point(16, 48);
        public static Size PowerupStackSize => new Size(48, 16);

        public static Point PowerupMultiLocation => new Point(16, 64);
        public static Size PowerupMultiSize => new Size(48, 16);

        public static Point WallTopLeftLocation => new Point(16, 16);
        public static Size WallTopLeftSize => new Size(16, 16);

        public static Point WallTopMiddleLocation => new Point(32, 16);
        public static Size WallTopMiddleSize => new Size(48, 16);

        public static Point WallTopRightLocation => new Point(80, 16);
        public static Size WallTopRightSize => new Size(16, 16);

        public static Point WallSideBottomLocation => new Point(0, 64);
        public static Size WallSideBottomSize => new Size(16, 16);

        public static Point WallSideMiddleLocation => new Point(0, 16);
        public static Size WallSideMiddleSize => new Size(16, 48);
    }
}
