using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using System.Numerics;
using Windows.Foundation;

namespace GameObjects
{
    public class GameManager
    {
        private CanvasBitmap background;
        private CanvasBitmap spriteSheet;

        private List<Wall> walls;

        public GameManager(CanvasBitmap background, CanvasBitmap spriteSheet)
        {
            this.background = background;
            this.spriteSheet = spriteSheet;

            BuildWalls();
        }

        private void BuildWalls()
        {
            walls = new List<Wall>();

            walls.Add(new Wall(spriteSheet, WallSide.Top, new Rect(48, 30, 864, 16)));
            walls.Add(new Wall(spriteSheet, WallSide.Bottom, new Rect(48, 510, 864, 16)));
            walls.Add(new Wall(spriteSheet, WallSide.Left, new Rect(48, 46, 16, 464)));
            walls.Add(new Wall(spriteSheet, WallSide.Right, new Rect(896, 46, 16, 464)));
        }

        public void Update(CanvasSpriteBatch spriteBatch, double deltaTime)
        {
            spriteBatch.Draw(background, new Rect(0,0,960,540));
            foreach (var wall in walls) { wall.Update(spriteBatch, deltaTime); }
            //spriteBatch.DrawFromSpriteSheet(spriteSheet, new Rect(), new Rect());
        }


    }
}
