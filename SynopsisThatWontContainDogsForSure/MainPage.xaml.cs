using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
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

namespace SynopsisThatWontContainDogsForSure
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        MediaPlayer mediaPlayer;
        private string _input;

        public string Input
        {
            get { return _input; }
            set { _input = value;
                OnPropertyChanged("Input");
            }
        }


        private List<float> _outPutList;

        public List<float> OutPutList
        {
            get { return _outPutList; }
            set { _outPutList = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected async void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High,
          () =>
          {
              PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
          });
        }
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(480, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.CompactOverlay;
            OutPutList = new List<float>();



            
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
        private void TextBox_OnBeforeTextChanging(TextBox sender,
                                          TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void ExecuteBtn1_Click(object sender, RoutedEventArgs e)
        {
            OutPutList = new List<float>();
            for (int i = 0; i < Int32.Parse(Input); i++)
            {
                OutPutList.Add(Q_rsqrt(i));
            }
            OutPutListBox.ItemsSource = OutPutList;
        }

        private void ExecuteBtn2_Click(object sender, RoutedEventArgs e)
        {
            OutPutList = new List<float>();
            Parallel.For(0, Int32.Parse(Input), i => { OutPutList.Add(Q_rsqrt(i)); });
            OutPutListBox.ItemsSource = OutPutList;

        }

        private async void ExecuteBtn3_Click(object sender, RoutedEventArgs e)
        {
            OutPutList = new List<float>();

            OutPutList = Task.Factory.StartNew(() => ThirdMethod()).Result;
            await Task.CompletedTask;
            OutPutListBox.ItemsSource = OutPutList;


        }

        private async void ExecuteBtn4_Click(object sender, RoutedEventArgs e)
        {
            OutPutList = new List<float>();
            Parallel.For(0, Int32.Parse(Input), i => { OutPutList.Add(Q_rsqrt(i)); });
            OutPutListBox.ItemsSource = OutPutList;
        }

        private void ExecuteBtn5_Click(object sender, RoutedEventArgs e)
        {
           // Magic program = new Magic();
        }
        private List<float> ThirdMethod()
        {
            List<float> localOutPutList = new List<float>();

            for (int i = 0; i < Int32.Parse(Input); i++)
            {
                localOutPutList.Add(Q_rsqrt(i));
            } 

            return localOutPutList;
        }
    } 
}
