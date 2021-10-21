using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    internal static class BECalculator
    {
        private static float curPercentage;
        /// <summary>
        /// Turns orgasm count and gauge percentage into one percentage value and interpolates it
        /// </summary>
        /// <param name="orgCount"></param>
        /// <param name="gaugePercentage"></param>
        /// <returns>interpolated BE percentage change </returns>
        internal static float CalculateBE(int orgCount,float  gaugePercentage)
        {
            if ((orgCount + gaugePercentage)> BE.NumberofOrgasms.Value)
            {
                curPercentage = 1f;
            }
            else
            {
                curPercentage = (orgCount + gaugePercentage) / BE.NumberofOrgasms.Value;
            }
            return (float) Interpolation.BEInterpolation(curPercentage);
        }
    }
}
