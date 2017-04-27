using GameObjects;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Gaming.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CosmicBreakout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private Gamepad gamepad = null;

        private GameManager gameManager;

        private CanvasBitmap background;
        private CanvasBitmap spriteSheet;

        public GamePage()
        {
            this.InitializeComponent();
            //Windows.UI.ViewManagement.ApplicationViewScaling.TrySetDisableLayoutScaling(true);
            //gameOverFlag = false;
        }

        /// <summary>
        /// Loads the image resources and game manager into memory.
        /// </summary>
        private void GameCanvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        /// <summary>
        /// Loads the image resources and game manager into memory asynchronously.
        /// </summary>
        private async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            background = await CanvasBitmap.LoadAsync(sender, "Images/Background.png");
            spriteSheet = await CanvasBitmap.LoadAsync(sender, "Images/Sprites.png");
            gameManager = new GameManager(background, spriteSheet);
        }

        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    // Populate the list of users.
        //    IReadOnlyList<User> users = await User.FindAllAsync();
        //    int userNumber = 1;
        //    foreach (User user in users)
        //    {
        //        string displayName = (string)await user.GetPropertyAsync(KnownUserProperties.DisplayName);

        //        // Choose a generic name if we do not have access to the actual name.
        //        if (String.IsNullOrEmpty(displayName))
        //        {
        //            displayName = "User #" + userNumber.ToString();
        //            userNumber++;
        //        }
        //        currentUser = displayName;
        //    }
        //}

        /// <summary>
        /// Primary game logic loop. Called at a fixed interval.
        /// </summary>
        private async void GameCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (gamepad == null && Gamepad.Gamepads.Count > 0) gamepad = Gamepad.Gamepads[0];

            if (gamepad != null)
            {
                var reading = gamepad.GetCurrentReading();

                switch (reading.Buttons)
                {
                    case GamepadButtons.Menu:
                        gameManager.gameOver = true;
                        break;
                    case GamepadButtons.X:
                        gameManager.MultiBall();
                        break;
                    case GamepadButtons.Y:
                        gameManager.StackedPaddle();
                        break;
                    case GamepadButtons.B:
                        gameManager.WidePaddle();
                        break;
                    case GamepadButtons.A:
                        gameManager.Explode();
                        break;
                    default:
                        break;
                }

                foreach (var paddle in gameManager.Paddles)
                {
                    paddle.Move(reading.LeftThumbstickX, reading.LeftThumbstickY);
                }
            }
            gameManager.Update();
            if (gameManager.gameOver)
            {
                //if (!gameOverFlag)
                //{
                //    CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                //           () => { ((App)Application.Current).HighScoreData.datalist.Add(new KeyValuePair<int,string>(gameManager.score, currentUser)); });
                //}
                //gameOverFlag = true;
                

                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                       () => { Frame.Navigate(typeof(GameOverPage), gameManager.score); });
            }

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ScoreBox.Text = gameManager.score.ToString();
            });

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                BallsBox.Text = gameManager.Balls.Count.ToString();
            });
        }

        //private bool gameOverFlag;
        //public string currentUser;

        /// <summary>
        /// Primary graphics loop. Called 60 times per second.
        /// </summary>
        private void GameCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            var spriteBatch = args.DrawingSession.CreateSpriteBatch(true);
            gameManager.Draw(spriteBatch);
            spriteBatch.Dispose();
        }

        /// <summary>
        /// Handles keyboard input events.
        /// </summary>
        private void GameCanvas_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            args.Handled = true;
            var virtualKey = args.VirtualKey;
            var action = GameCanvas.RunOnGameLoopThreadAsync(() => KeyDown_GameLoopThread(virtualKey));
        }

        /// <summary>
        /// Handles input on the gameloop thread.
        /// </summary>
        /// <param name="virtualKey"></param>
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

        /// <summary>
        ///
        /// </summary>
        private void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {
            gamepad = null;
        }

        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            gamepad = e;
        }

        /// <summary>
        /// Register events upon page loaded.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Gamepad.GamepadAdded += Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;
            Window.Current.CoreWindow.KeyDown += GameCanvas_KeyDown;

        }

        /// <summary>
        /// Unregister events upon page unloaded.
        /// </summary>
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
