using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    internal static class MathUtils
    {
        
        internal static double LogInterpolation(float completion)
        {
            //so when x =0 returns 0 and when x =1 returns 1
            return Math.Log(completion + 1, 2.0);  //log2(x+1)
        }

        internal static float LinearInterpolation(float completion)
        {
            return completion;
        }

        internal static double ExponentialInterpolation(float completion)
        {
            //when x = 0 returns 0 when x =1 returns 1
            return Math.Pow(2,completion) - 1;// 2^x -1
        }

        internal static double QuadraticInterpolation(float completion) //slightly curvier version of exponential interpolation
        {
            //when x = 0 returns 0 when x =1 returns 1
            return Math.Pow(completion, 2);// x^2
        }
    }
}
