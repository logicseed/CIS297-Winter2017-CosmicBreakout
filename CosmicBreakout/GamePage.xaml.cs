using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using GameObjects;
using Microsoft.Graphics.Canvas.UI;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CosmicBreakout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private DispatcherTimer dispatcherTimer;

        private GameManager gameManager;
        private DateTime lastDraw;

        private CanvasBitmap background;
        private CanvasBitmap spriteSheet;

        public GamePage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.CharacterReceived += CoreWindow_CharacterReceived;

        }

        private void CoreWindow_CharacterReceived(CoreWindow sender, CharacterReceivedEventArgs args)
        {
            if (args.KeyCode == 27) //ESC
            {
                CoreApplication.Exit();
            }
        }

        //private void GameCanvas_Draw(CanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        //{
        //    args.DrawingSession.Units = CanvasUnits.Pixels;
        //    var spriteBatch = args.DrawingSession.CreateSpriteBatch(CanvasSpriteSortMode.None,
        //        CanvasImageInterpolation.NearestNeighbor, CanvasSpriteOptions.ClampToSourceRect);

        //    if (lastDraw == null) lastDraw = DateTime.Now;
        //    var previousDraw = lastDraw;
        //    lastDraw = DateTime.Now;
        //    var deltaTime = (lastDraw - previousDraw).TotalSeconds;

        //    gameManager.Update(spriteBatch, deltaTime);
        //    spriteBatch.Dispose();
        //}

        private void GameCanvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            background = await CanvasBitmap.LoadAsync(sender, "Images/Background.png");
            spriteSheet = await CanvasBitmap.LoadAsync(sender, "Images/Sprites.png");
            gameManager = new GameManager(background, spriteSheet);
        }

        private void GameCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            //args.Timing.ElapsedTime.Seconds;
        }

        private void GameCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.Units = CanvasUnits.Pixels;
            var spriteBatch = args.DrawingSession.CreateSpriteBatch(CanvasSpriteSortMode.None,
                CanvasImageInterpolation.NearestNeighbor, CanvasSpriteOptions.ClampToSourceRect);

            if (lastDraw == null) lastDraw = DateTime.Now;
            var previousDraw = lastDraw;
            lastDraw = DateTime.Now;
            var deltaTime = (lastDraw - previousDraw).TotalSeconds;

            gameManager.Update(spriteBatch, deltaTime);
            spriteBatch.Dispose();
        }
    }
}
