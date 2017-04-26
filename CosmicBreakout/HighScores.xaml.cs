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
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CosmicBreakout
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScores : Page
    {
        public HighScores()
        {
            this.InitializeComponent();
            populateData();
        }

        public ObservableCollection<ScoreRecord> ScoreList = new ObservableCollection<ScoreRecord>();
        public void populateData()
        {
            if (((App)Application.Current).HighScoreData.datalist.Count() > 1)
            {
                int idx = 0;
                bool found = false;
                foreach (KeyValuePair<int, string> kvp in ((App)Application.Current).HighScoreData.datalist)
                {
                    if (kvp.Value == "No Scores Recorded Yet")
                    {
                        found = true;
                        break;
                    }
                    idx++;
                }
                if (found)
                    ((App)Application.Current).HighScoreData.datalist.RemoveAt(idx);
            }

            for (int i = 0; i < ((App)Application.Current).HighScoreData.datalist.Count(); i++)
            {
                ScoreList.Add(new ScoreRecord
                {
                    Player = ((App)Application.Current).HighScoreData.datalist.ElementAt(i).Value,
                    Score = ((App)Application.Current).HighScoreData.datalist.ElementAt(i).Key.ToString()
            });

            }

            ScoreList.OrderByDescending(a => a.Score);
            high_scores.ItemsSource = ScoreList;
        }



        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
