using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace GameObjects
{
    public static class GameSprite
    {
        public static Rect Background => new Rect(0, 0, 1920, 1080);
        public static Point BallLocation => new Point(924, 524);
        public static Size BallSize => new Size(32, 32);

        public static Size BlockSize => new Size(96, 32);

        public static Rect PaddlePathPrimary => new Rect(128, 924, 1664, 96);
        public static Rect PaddlePathStacked => new Rect(128, 828, 1664, 96);
        public static Point PaddleLocation => new Point(880, 924);
        public static Size PaddleNormalSize => new Size(160, 32);
        public static Size PaddleWideSize => new Size(320, 32);
        public static Size PaddleCapSize => new Size(32, 32);


        public static Point WallTopLocation => new Point(96, 60);
        public static Size WallTopSize => new Size(1728, 32);
        public static Size WallTopCapSize => new Size(32, 32);

        public static Point WallSideLeftLocation => new Point(96, 92);
        public static Point WallSideRightLocation => new Point(1792, 92);
        public static Size WallSideSize => new Size(32, 928);
        public static Size WallSideCapSize => new Size(32, 32);

        public static Size PowerupSize => new Size(96, 32);


    }
}
