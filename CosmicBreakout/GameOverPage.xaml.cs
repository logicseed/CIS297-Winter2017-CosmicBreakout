using GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CosmicBreakout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOverPage : Page
    {
        private int score;
        private Gamepad gamepad = null;
        private int ticks = 0;
        private const int DELAY_TICKS = 20;

        public GameOverPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            score = (int)e.Parameter;

            ScoreText.Text = score.ToString();
        }

        private void GamePadInput_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
        {
            if (gamepad == null && Gamepad.Gamepads.Count > 0) gamepad = Gamepad.Gamepads[0];

            if (gamepad != null && ticks > DELAY_TICKS)
            {
                var reading = gamepad.GetCurrentReading();

                switch (reading.Buttons)
                {
                    case GamepadButtons.Menu:
                    case GamepadButtons.View:
                    case GamepadButtons.X:
                    case GamepadButtons.Y:
                    case GamepadButtons.A:
                    case GamepadButtons.B:
                    case GamepadButtons.DPadDown:
                    case GamepadButtons.DPadLeft:
                    case GamepadButtons.DPadRight:
                    case GamepadButtons.DPadUp:
                    case GamepadButtons.LeftShoulder:
                    case GamepadButtons.LeftThumbstick:
                    case GamepadButtons.Paddle1:
                    case GamepadButtons.Paddle2:
                    case GamepadButtons.Paddle3:
                    case GamepadButtons.Paddle4:
                    case GamepadButtons.RightShoulder:
                    case GamepadButtons.RightThumbstick:
                        CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () => { Frame.Navigate(typeof(MainPage)); });
                        break;
                    default:
                        break;
                }
            }
            ticks++;
        }
    }
}
