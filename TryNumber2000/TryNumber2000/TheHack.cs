using Hybridizer.Runtime.CUDAImports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryNumber2000
{
    public class TheHack
    {
        public static List<float> outComeList = new List<float>();

        [EntryPoint]
        public static List<float> run(int interations)
        {
            Parallel.For(0, interations, (i) => { outComeList.Add(Erf(i)); });
            return outComeList;
        }
        public static List<float> Run()
        {
            HybRunner runner = HybRunner.Cuda();
            dynamic wrapped = runner.Wrap(new TheHack());
            cuda.DeviceSynchronize();
            return wrapped.run(1000);
        }
        [IntrinsicFunction("erf")]
        private static float Erf(float number)
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
}
