using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    internal static class Interpolation
    {
        /// <summary>
        /// Varies the rate of change of BE
        /// </summary>
        /// <param name="completion">Percentage completion of BE from 0 to 1</param>
        /// <returns>Modified BE completion percentage from 0 to 1</returns>
        internal static double BEInterpolation(float completion)
        {
            switch (BE.SetInterpolationType.Value) {
                case InterpolationType.Linear:
                    return MathUtils.LinearInterpolation(completion);
                case InterpolationType.Logarithmic:
                    return MathUtils.LogInterpolation(completion);
                case InterpolationType.Quadratic:
                    return MathUtils.QuadraticInterpolation(completion);
                case InterpolationType.Exponential:
                    return MathUtils.ExponentialInterpolation(completion);
                default:
                    return MathUtils.LinearInterpolation(completion);
            }

        }
    }
}
