﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SynopsisThatWontContainDogsForSure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer mediaPlayer;

        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
        {
            mediaPlayer.Play();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetUpVideo();
        }

        private void SetUpVideo()
        {
            MediaPlayerElement.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/DogNotJumping.mp4"));
            mediaPlayer = MediaPlayerElement.MediaPlayer;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            mediaPlayer.Play();

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        [EntryPoint]
        public static void Run(float[,] result)
        {
            int size = result.GetLength(0);
            Parallel.For(0, size, 0, size, (i, j) => {
                float x = fromX + i * h;
                float y = fromY + j * h;
                result[i, j] = IterCount(x, y);
            });
        }

        public static float IterCount(float cx, float cy)
        {
            float result = 0.0F;
            float x = 0.0f, y = 0.0f, xx = 0.0f, yy = 0.0f;
            while (xx + yy <= 4.0f && result < maxiter)
            {
                xx = x * x;
                yy = y * y;
                float xtmp = xx - yy + cx;
                y = 2.0f * x * y + cy;
                x = xtmp;
                result++;
            }
            return result;
        }
        private float Q_rsqrt(float number)
        {
            long i;
            float x2, y;
            const float threehalfs = 1.5F;

            x2 = number * 0.5F;
            y = number;
            i = BitConverter.SingleToInt32Bits(y);      // evil floating point bit level hacking
            i = 0x5f3759df - (i >> 1);                  // what the fuck? 
            y = BitConverter.SingleToInt32Bits(i);
            y = y * (threehalfs - (x2 * y * y));        // 1st iteration
        //	y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed

            return y;
        }
    }
}
