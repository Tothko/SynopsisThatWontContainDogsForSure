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
            HeaderTextBlock.Text = "Totally not copied for mic.docs";
            DescriptionTextBlock.Text = @"var startTime = DateTime.Now;
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            // Source must be array or IList.
            var source = Enumerable.Range(0, arraySize).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);


            // Loop over the partitions in parallel.
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                // Loop over each range element without a delegate invocation.
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    OutputModel outputModel = new OutputModel
                    {
                        Id = i,
                        Value = Q_rsqrt(source[i]),
                        ThreadName = Thread.CurrentThread.Name,
                        TaskName = Task.CurrentId.ToString(),
                        Time = DateTime.Now
                    };
                    outputModels.Add(outputModel);
                }
            });
            var endTime = startTime - DateTime.Now;
            EndTimeTextBox.Text = endTime.ToString();";
        }
        private void FifthTabBtn_Checked(object sender, RoutedEventArgs e)
        {
            HeaderTextBlock.Text = "Options to limit tasks";
            DescriptionTextBlock.Text = @"var startTime = DateTime.Now;
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            // Source must be array or IList.
            var source = Enumerable.Range(0, arraySize).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);


            // Loop over the partitions in parallel.
            Parallel.ForEach(rangePartitioner, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (range, loopState) =>
            {
                // Loop over each range element without a delegate invocation.
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    OutputModel outputModel = new OutputModel
                    {
                        Id = i,
                        Value = Q_rsqrt(source[i]),
                        ThreadName = Thread.CurrentThread.Name,
                        TaskName = Task.CurrentId.ToString(),
                        Time = DateTime.Now
                    };
                    outputModels.Add(outputModel);
                }
            });
            var endTime = startTime - DateTime.Now;
            EndTimeTextBox.Text = endTime.ToString();";
        }

        private void SixthTabBtn_Checked(object sender, RoutedEventArgs e)
        {
            HeaderTextBlock.Text = "Are you in a simulation inside of simulation that's inside of another simaltion?";
            DescriptionTextBlock.Text = @"Open code for this one";
        }

        private void SequentialFor(int numberOfItterations)
        {
            List<OutputModel> outputModels = new List<OutputModel>();
            var startTime = DateTime.Now;
            StartTimeTextBox.Text = startTime.ToLongTimeString();
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
            TimeSpan endTime = startTime - DateTime.Now;
            EndTimeTextBox.Text = endTime.ToString();

            //     OutputList.ItemsSource = outputModels;
        }
        private void ParallelFor(int numberOfTasks)
        {
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            var startTime = DateTime.Now;
            StartTimeTextBox.Text = startTime.ToLongTimeString();

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
            EndTimeTextBox.Text = endTime.ToString();

            // OutputList.ItemsSource = outputModels;
        }
        private void ParralelForPartitioned(int iterations)
        {
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            var startTime = DateTime.Now;
            StartTimeTextBox.Text = startTime.ToLongTimeString();

            Parallel.ForEach(Partitioner.Create(0, iterations), (range) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
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
            EndTimeTextBox.Text = endTime.ToString();

            //  OutputList.ItemsSource = outputModels;
        }

        private void ParralelForPartitionedThatWorks(int arraySize)
        {
            var startTime = DateTime.Now;
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            // Source must be array or IList.
            var source = Enumerable.Range(0, arraySize).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);


            // Loop over the partitions in parallel.
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                // Loop over each range element without a delegate invocation.
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    OutputModel outputModel = new OutputModel
                    {
                        Id = i,
                        Value = Q_rsqrt(source[i]),
                        ThreadName = Thread.CurrentThread.Name,
                        TaskName = Task.CurrentId.ToString(),
                        Time = DateTime.Now
                    };
                    outputModels.Add(outputModel);
                }
            });
            var endTime = startTime - DateTime.Now;
            EndTimeTextBox.Text = endTime.ToString();

            //  OutputList.ItemsSource = outputModels;

        }

        private void ParralelForPartitionedWithOptions(int arraySize)
        {
            var startTime = DateTime.Now;
            ConcurrentBag<OutputModel> outputModels = new ConcurrentBag<OutputModel>();
            // Source must be array or IList.
            var source = Enumerable.Range(0, arraySize).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);


            // Loop over the partitions in parallel.
            Parallel.ForEach(rangePartitioner, new ParallelOptions { MaxDegreeOfParallelism = 2 }, (range, loopState) =>
            {
                // Loop over each range element without a delegate invocation.
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    OutputModel outputModel = new OutputModel
                    {
                        Id = i,
                        Value = Q_rsqrt(source[i]),
                        ThreadName = Thread.CurrentThread.Name,
                        TaskName = Task.CurrentId.ToString(),
                        Time = DateTime.Now
                    };
                    outputModels.Add(outputModel);


                }
            });
            var endTime = startTime - DateTime.Now;
            EndTimeTextBox.Text = endTime.ToString();
            // OutputList.ItemsSource = outputModels;

        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            OutputList.ItemsSource = null;
            int entryNumber = 10000000;
            if (FirstTabBtn.IsChecked == true) SequentialFor(entryNumber);
            if (SecondTabBtn.IsChecked == true) ParallelFor(entryNumber);
            if (ThirdTabBtn.IsChecked == true) ParralelForPartitioned(entryNumber);
            if (FourthTabBtn.IsChecked == true) ParralelForPartitionedThatWorks(entryNumber);
            if (FifthTabBtn.IsChecked == true) ParralelForPartitionedWithOptions(entryNumber);
            if (SixthTabBtn.IsChecked == true) Simulate(10);


        }
        private async void OperationAsyncBtn_Click(object sender, RoutedEventArgs e)
        {
            int entryNumber = 100000;
            if (FirstTabBtn.IsChecked == true) await Task.Factory.StartNew(() => SequentialFor(entryNumber));
            if (SecondTabBtn.IsChecked == true) await Task.Factory.StartNew(() => ParallelFor(entryNumber));
            if (ThirdTabBtn.IsChecked == true) await Task.Factory.StartNew(() => ParralelForPartitioned(entryNumber));
            if (FourthTabBtn.IsChecked == true) await Task.Factory.StartNew(() => ParralelForPartitionedThatWorks(entryNumber));
        }

        private void Simulate(int numberOfSimulations)
        {
            int entryNumber = 10000000;
            List<string> SimulationOutcomes = new List<string>(); ;

            List<TimeSpan> simulationTimes = new List<TimeSpan>(); ;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                var SimulationStart = DateTime.Now;
                SequentialFor(entryNumber);
                var simulationTime = DateTime.Now - SimulationStart;
                simulationTimes.Add(simulationTime);
            }
            double TotalSimulationDuration = 0;
            foreach (var item in simulationTimes)
            {
                TotalSimulationDuration = TotalSimulationDuration + item.TotalSeconds;
            }
            SimulationOutcomes.Add("First simulation took:" + TotalSimulationDuration + " Which concludes in avarage time of: " + TotalSimulationDuration / numberOfSimulations);



            simulationTimes = new List<TimeSpan>(); ;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                var SimulationStart = DateTime.Now;
                ParallelFor(entryNumber);
                var simulationTime = DateTime.Now - SimulationStart;
                simulationTimes.Add(simulationTime);
            }
             TotalSimulationDuration = 0;
            foreach (var item in simulationTimes)
            {
                TotalSimulationDuration = TotalSimulationDuration + item.TotalSeconds;
            }
            SimulationOutcomes.Add("Second simulation took:" + TotalSimulationDuration + " Which concludes in avarage time of: " + TotalSimulationDuration / numberOfSimulations);


            simulationTimes = new List<TimeSpan>(); ;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                var SimulationStart = DateTime.Now;
                ParralelForPartitioned(entryNumber);
                var simulationTime = DateTime.Now - SimulationStart;
                simulationTimes.Add(simulationTime);
            }
            TotalSimulationDuration = 0;
            foreach (var item in simulationTimes)
            {
                TotalSimulationDuration = TotalSimulationDuration + item.TotalSeconds;
            }
            SimulationOutcomes.Add("Third simulation took:" + TotalSimulationDuration + " Which concludes in avarage time of: " + TotalSimulationDuration / numberOfSimulations);


            simulationTimes = new List<TimeSpan>(); ;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                var SimulationStart = DateTime.Now;
                ParralelForPartitionedThatWorks(entryNumber);
                var simulationTime = DateTime.Now - SimulationStart;
                simulationTimes.Add(simulationTime);
            }
            TotalSimulationDuration = 0;
            foreach (var item in simulationTimes)
            {
                TotalSimulationDuration = TotalSimulationDuration + item.TotalSeconds;
            }
            SimulationOutcomes.Add("Fourth simulation took:" + TotalSimulationDuration + " Which concludes in avarage time of: " + TotalSimulationDuration / numberOfSimulations);


            simulationTimes = new List<TimeSpan>(); ;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                var SimulationStart = DateTime.Now;
                ParralelForPartitionedWithOptions(entryNumber);
                var simulationTime = DateTime.Now - SimulationStart;
                simulationTimes.Add(simulationTime);
            }
            TotalSimulationDuration = 0;
            foreach (var item in simulationTimes)
            {
                TotalSimulationDuration = TotalSimulationDuration + item.TotalSeconds;
            }
            SimulationOutcomes.Add("Fifth simulation took:" + TotalSimulationDuration + " Which concludes in avarage time of: " + (TotalSimulationDuration / numberOfSimulations));

            OutputList.ItemsSource = SimulationOutcomes;

        }

    }
}
