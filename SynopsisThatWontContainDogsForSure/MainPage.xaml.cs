using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
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


        public MainPage()
        {
            this.InitializeComponent();
            
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void FirstTabBtn_Checked(object sender, RoutedEventArgs e)
        {
            HeaderTextBlock.Text = "Just sequintal method";
            DescriptionTextBlock.Text = @"List<OutputModel> outputModels = new List<OutputModel>();
            var startTime = DateTime.Now;
            for (int i = 0; i < numberOfItterations; i++)
            {
                OutputModel outputModel = new OutputModel
                {
                    Id = i,
                    Value = Q_rsqrt(i),
                    ThreadName = Thread.CurrentThread.Name,
                    TaskName = 'Not really in a task',
                    Time = DateTime.Now
                };
                outputModels.Add(outputModel);
            }
            var endTime = startTime - DateTime.Now;
            OutputList.ItemsSource = outputModels;";
        }

        private void SecondTabBtn_Checked(object sender, RoutedEventArgs e)
        {
            HeaderTextBlock.Text = "Parralel for tasks";
            DescriptionTextBlock.Text = @"ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            var startTime = DateTime.Now;
            Parallel.For(0, numberOfTasks, (i, state) =>
              {
                  OutputModel outputModel = new OutputModel
                  {
                      Id = i,
                      Value = Q_rsqrt(i),
                      ThreadName = Thread.CurrentThread.Name,
                      TaskName = Task.CurrentId.ToString(),
                      Time = DateTime.Now
                  };
                  outputModels.Add(outputModel);
              });
            var endTime = startTime - DateTime.Now;
            OutputList.ItemsSource = outputModels;";
        }

        private void ThirdTabBtn_Checked(object sender, RoutedEventArgs e)
        {
            HeaderTextBlock.Text = "Partitioned tasks xoxo";
            DescriptionTextBlock.Text = @"ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            var startTime = DateTime.Now;
            Parallel.ForEach(Partitioner.Create(0, numberOrTasks), (range)  =>
            {
            for(i = range.Item1; i < range.Item2; i++)
{
                    OutputModel outputModel = new OutputModel
                    {
                        Id = i,
                        Value = Q_rsqrt(i),
                        ThreadName = Thread.CurrentThread.Name,
                        TaskName = Task.CurrentId.ToString(),
                        Time = DateTime.Now
                    };
                    outputModels.Add(outputModel);
                }
            });
            var endTime = startTime - DateTime.Now;
            OutputList.ItemsSource = outputModels;";
        }

        private void FourthTabBtn_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SequentialFor(int numberOfItterations)
        {
            List<OutputModel> outputModels = new List<OutputModel>();
            var startTime = DateTime.Now;
            for (int i = 0; i < numberOfItterations; i++)
            {
                OutputModel outputModel = new OutputModel
                {
                    Id = i,
                    Value = Q_rsqrt(i),
                    ThreadName = Thread.CurrentThread.Name,
                    TaskName = "Not really in a task",
                    Time = DateTime.Now
                };
                outputModels.Add(outputModel);
            }
            var endTime = startTime - DateTime.Now;
            OutputList.ItemsSource = outputModels;
        }
        private void ParallelFor(int numberOfTasks)
        {
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            var startTime = DateTime.Now;
            Parallel.For(0, numberOfTasks, (i, state) =>
              {
                  OutputModel outputModel = new OutputModel
                  {
                      Id = i,
                      Value = Q_rsqrt(i),
                      ThreadName = Thread.CurrentThread.Name,
                      TaskName = Task.CurrentId.ToString(),
                      Time = DateTime.Now
                  };
                  outputModels.Add(outputModel);
              });
            var endTime = startTime - DateTime.Now;
            OutputList.ItemsSource = outputModels;
        }
        private void ParralelForPartitioned(int numberOrTasks)
        {
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            var startTime = DateTime.Now;
            Parallel.ForEach(Partitioner.Create(0, numberOrTasks), (range)  =>
            {
            for(int i = range.Item1; i < range.Item2; i++)
{
                    OutputModel outputModel = new OutputModel
                    {
                        Id = i,
                        Value = Q_rsqrt(i),
                        ThreadName = Thread.CurrentThread.Name,
                        TaskName = Task.CurrentId.ToString(),
                        Time = DateTime.Now
                    };
                    outputModels.Add(outputModel);
                }
            });
            var endTime = startTime - DateTime.Now;
            OutputList.ItemsSource = outputModels;
        }
        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FirstTabBtn.IsChecked == true) SequentialFor(100000);
            if (SecondTabBtn.IsChecked == true) ParallelFor(100000);
            if (ThirdTabBtn.IsChecked == true) ParralelForPartitioned(100000);
            
        }
    }
}
