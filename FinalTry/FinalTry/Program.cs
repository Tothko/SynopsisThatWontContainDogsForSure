using Hybridizer.Runtime.CUDAImports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalTry
{
    class Program
    {

        private static float[] endResultsList = new float[1000];

        public static float[] EndResultsList
        {
            get { return endResultsList; }
            set
            {
                endResultsList = value;
            }
        }

        static void Main(string[] args)
        {
            // Sequential CPU
            for (int i = 0; i < 1000; i++)
            {
                EndResultsList[i] = (IntrinsicFunction.Erf(i));

            }
            // Sequential CPU GPU
            for (int i = 0; i < 1000; i++)
            {
                IntrinsicFunction.Run(i);

            }

            ParrallelPart.Run(1);

            Console.WriteLine(EndResultsList);
            Console.Read();
            Console.WriteLine("GPU Computing task started at: " + DateTime.Now);
            RunAsync().GetAwaiter().GetResult();
            Console.WriteLine("CPU Computing task started at: " + DateTime.Now);
            runAsync().GetAwaiter().GetResult();
        }

        public static async Task<float[]> RunAsync()
        {
            int numberOfIterations = 100000000;
            float[] output = new float[numberOfIterations];
            Task task1 = Task.Factory.StartNew(() => ParrallelPart.Run(numberOfIterations)); // GPU Parallel
            await task1.ContinueWith(task => { Console.WriteLine("GPU Computing task completed at: " + DateTime.Now + "\n With Results: \n" + output[2]); });
            return output;

        }
        public static async Task<float[]> runAsync()
        {
            int numberOfIterations = 100000000;
            float[] output = new float[numberOfIterations];
            Task task2 = Task.Factory.StartNew(() => ParrallelPart.run(numberOfIterations)); // CPU Parallel
            await task2.ContinueWith(task => { Console.WriteLine("CPU Computing task completed at: " + DateTime.Now + "\n With Results: \n" + output[2]); });
            return output;
        }
    }

    class ParrallelPart
    {
        [EntryPoint]
        public static void run(int iteration)
        {
            float[] output = new float[iteration];
            Parallel.For(0, iteration, i => { output[i] = QuakeRulez(i); });
            //return output;
        }

        public static void Run(int iteration)
        {
            HybRunner runner = HybRunner.Cuda().SetDistrib(1, 1);
            dynamic wrapped = runner.Wrap(new ParrallelPart());
            Console.WriteLine(":: CUDA :: ");
            cuda.DeviceSynchronize();
            wrapped.run(iteration);
        }


        [IntrinsicFunction("QuakeRulez")]
        public static float QuakeRulez(float number)
        {
            long i;
            float x2, y;
            const float threehalfs = 1.5F;

            x2 = number * 0.5F;
            y = number;
            i = BitConverter.ToInt32(BitConverter.GetBytes(y), 0);      // evil floating point bit level hacking
            i = 0x5f3759df - (i >> 1);                  // what the fuck? 
            y = BitConverter.ToInt32(BitConverter.GetBytes(i), 0);
            y = y * (threehalfs - (x2 * y * y));        // 1st iteration
                                                        //	y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed
            return y;
        }
    }

    class IntrinsicFunction
    {
        [IntrinsicFunction("printf")]
        public static void printf(string format, float val)
        {
            Console.WriteLine(val);
        }
        [IntrinsicFunction("erf")]
        public static float Erf(float number)
        {
            long i;
            float x2, y;
            const float threehalfs = 1.5F;

            x2 = number * 0.5F;
            y = number;
            i = BitConverter.ToInt32(BitConverter.GetBytes(y), 0);      // evil floating point bit level hacking
            i = 0x5f3759df - (i >> 1);                  // what the fuck? 
            y = BitConverter.ToInt32(BitConverter.GetBytes(i), 0);
            y = y * (threehalfs - (x2 * y * y));        // 1st iteration
                                                        //	y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed

            return y;
        }
        [EntryPoint]
        public static void run(int iteration)
        {
            Program.EndResultsList[iteration] = Erf(iteration);
            printf("%.17lf\n", Erf(iteration));
        }
        public static void Run(int iteration)
        {
            HybRunner runner = HybRunner.Cuda().SetDistrib(1, 1);
            dynamic wrapped = runner.Wrap(new IntrinsicFunction());
            Console.WriteLine(":: CUDA :: ");
            cuda.DeviceSynchronize();
            wrapped.run(iteration);
        }
    }   

}
