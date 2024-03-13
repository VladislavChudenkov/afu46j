using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace AbobaMusic
{
    public partial class MainWindow : Window
    {
        private List<string> playlist = new List<string>();
        private int currentTrackIndex = 0;
        private bool isPlaying = false;
        private bool isRepeatMode = false;
        private bool isShuffleMode = false;
        private CancellationTokenSource cancellationTokenSource;
        private Task positionTrackerTask;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Audio files (*.mp3, *.m4a, *.wav)|*.mp3;*.m4a;*.wav";

            if (openFileDialog.ShowDialog() == true)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                LoadPlaylist(selectedFiles);
                PlayTrack();
            }
        }

        private void LoadPlaylist(string[] files)
        {
            playlist.Clear();
            foreach (string file in files)
            {
                if (IsAudioFile(file))
                    playlist.Add(file);
            }
        }

        private bool IsAudioFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            return ext == ".mp3" || ext == ".m4a" || ext == ".wav";
        }

        private void PlayTrack()
        {
            if (playlist.Count == 0)
                return;

            if (currentTrackIndex < 0)
                currentTrackIndex = 0;
            else if (currentTrackIndex >= playlist.Count)
                currentTrackIndex = playlist.Count - 1;

            mediaPlayer.Source = new Uri(playlist[currentTrackIndex]);
            mediaPlayer.Play();
            isPlaying = true;

            UpdateHistory();
            UpdatePositionTracker();
        }

        private void UpdateHistory()
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.UpdateHistory(playlist);
            historyWindow.Show();
        }

        private void UpdatePositionTracker()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            positionTrackerTask = Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested && mediaPlayer.Position < mediaPlayer.NaturalDuration.TimeSpan)
                {
                    Thread.Sleep(100); // Update approximately every 100 milliseconds
                    double currentPosition = mediaPlayer.Position.TotalSeconds;

                    // Update UI with current position
                    Dispatcher.Invoke(() =>
                    {
                        currentPositionLabel.Text = TimeSpan.FromSeconds(currentPosition).ToString(@"mm\:ss");
                        positionSlider.Value = currentPosition;
                    });
                }
            }, cancellationToken);
        }


        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
                mediaPlayer.Pause();
            else
                mediaPlayer.Play();

            isPlaying = !isPlaying;
        }

        private void NextTrackButton_Click(object sender, RoutedEventArgs e)
        {
            currentTrackIndex++;
            PlayTrack();
        }

        private void PreviousTrackButton_Click(object sender, RoutedEventArgs e)
        {
            currentTrackIndex--;
            PlayTrack();
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            isRepeatMode = !isRepeatMode;
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            isShuffleMode = !isShuffleMode;
            if (isShuffleMode)
                ShufflePlaylist();
            else
                playlist.Sort();
        }

        private void ShufflePlaylist()
        {
            Random rng = new Random();
            int n = playlist.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = playlist[k];
                playlist[k] = playlist[n];
                playlist[n] = value;
            }
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaPlayer != null)
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(positionSlider.Value);
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = volumeSlider.Value;
        }

        private void mediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (isRepeatMode)
                PlayTrack();
            else if (isShuffleMode)
            {
                currentTrackIndex = new Random().Next(0, playlist.Count);
                PlayTrack();
            }
            else
            {
                currentTrackIndex++;
                if (currentTrackIndex < playlist.Count)
                    PlayTrack();
                else
                {
                    currentTrackIndex = 0;
                    isPlaying = false;
                }
            }
        }

        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            positionSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            UpdatePositionTracker();
        }

    }
}
