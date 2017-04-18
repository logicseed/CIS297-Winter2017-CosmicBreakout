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

        private void GameCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (gamepad != null)
            {
                var reading = gamepad.GetCurrentReading();

                switch (reading.Buttons)
                {
                    case GamepadButtons.Menu:
                        CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () => { Frame.Navigate(typeof(MainPage)); });
                        break;
                    default:
                        break;
                }

                foreach (var paddle in gameManager.Paddles)
                {
                    paddle.Move(reading.LeftThumbstickX, reading.LeftThumbstickY);
                }
            }
            gameManager.Update(DeltaTime);
        }



        private void GameCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var spriteBatch = args.DrawingSession.CreateSpriteBatch(true);
            gameManager.Draw(spriteBatch);
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

        private void GameCanvas_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            args.Handled = true;
            var virtualKey = args.VirtualKey;
            var action = GameCanvas.RunOnGameLoopThreadAsync(() => KeyDown_GameLoopThread(virtualKey));
        }

        private void KeyDown_GameLoopThread(VirtualKey virtualKey)
        {
            switch (virtualKey)
            {
                case VirtualKey.Escape:
                    CoreApplication.Exit();
                    break;
                case VirtualKey.Up:
                    foreach (var paddle in gameManager.Paddles) paddle.Move(0, 2);
                    break;
                case VirtualKey.Down:
                    foreach (var paddle in gameManager.Paddles) paddle.Move(0, -2);
                    break;
                case VirtualKey.Left:
                    foreach (var paddle in gameManager.Paddles) paddle.Move(-2, 0);
                    break;
                case VirtualKey.Right:
                    foreach (var paddle in gameManager.Paddles) paddle.Move(2, 0);
                    break;
                default:
                    break;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;
            Window.Current.CoreWindow.KeyDown += GameCanvas_KeyDown;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Gamepad.GamepadAdded -= Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved -= Gamepad_GamepadRemoved;
            Window.Current.CoreWindow.KeyDown -= GameCanvas_KeyDown;

            GameCanvas.RemoveFromVisualTree();
            GameCanvas = null;
        }
    }
}
