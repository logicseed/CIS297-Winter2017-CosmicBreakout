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
using Windows.System;
using Windows.Gaming.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CosmicBreakout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private Gamepad gamepad = null;

        private GameManager gameManager;
        private DateTime lastDrawTime;

        private CanvasBitmap background;
        private CanvasBitmap spriteSheet;

        public GamePage()
        {
            this.InitializeComponent();

            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;

            Window.Current.CoreWindow.CharacterReceived += CoreWindow_CharacterReceived;
            //this.Focus(FocusState.Programmatic);
        }

        private void CoreWindow_CharacterReceived(CoreWindow sender, CharacterReceivedEventArgs args)
        {
            if (args.KeyCode == 27) //ESC
            {
                CoreApplication.Exit();
            }
        }

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

        private async void GameCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            var reading = gamepad.GetCurrentReading();

            // This handles input that may cause changes to the application as opposed to the game state.
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                // Handle gamepad input
                switch (reading.Buttons)
                {
                    case GamepadButtons.Menu:
                        Frame.Navigate(typeof(MainPage));
                        break;
                    default:
                        break;
                }
            }
            );
        }



        private void GameCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            // Handle Game Input
            var reading = gamepad.GetCurrentReading();
            foreach(var paddle in gameManager.Paddles)
            {
                paddle.Move(reading.LeftThumbstickX, reading.LeftThumbstickY);
            }

            var spriteBatch = args.DrawingSession.CreateSpriteBatch(true);
            gameManager.Update(spriteBatch, DeltaTime);
            spriteBatch.Dispose();
        }

        private double DeltaTime
        {
            get
            {
                if (lastDrawTime == null) lastDrawTime = DateTime.Now;

                var previousDrawTime = lastDrawTime;
                lastDrawTime = DateTime.Now;

                var deltaTime = (lastDrawTime - previousDrawTime).TotalSeconds;
                return deltaTime;
            }
        }

        private void GameCanvas_KeyDown(object sender, KeyRoutedEventArgs e)
        {

            if (e.Key == VirtualKey.Escape)
            {
                CoreApplication.Exit();
            }
        }

        private void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {
            gamepad = null;
        }

        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            gamepad = e;
        }
    }
}
