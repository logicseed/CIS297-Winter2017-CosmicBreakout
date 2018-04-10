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
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CosmicBreakout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<int> HighScoreList;
        private Gamepad gamepad = null;
        private int ticks = 0;
        private const int DELAY_TICKS = 20;

        /*   public static MainPage Instance { get { return Nested.instance; } }
           private class Nested
           {
               // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
               static Nested() {}
               internal static readonly MainPage instance = new MainPage();
           }*/

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();
            view.TryEnterFullScreenMode();
            Windows.UI.ViewManagement.ApplicationViewScaling.TrySetDisableLayoutScaling(true);
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }


        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HighScores));
        }

        private async void GamePadInput_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
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
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        () => { Frame.Navigate(typeof(GamePage)); });
                        break;
                    default:
                        break;
                }
            }
            ticks++;
        }
    }
}
