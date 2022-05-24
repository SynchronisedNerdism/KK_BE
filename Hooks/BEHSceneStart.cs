using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace BE
{
    internal static class BEHSceneStart
    {
        internal static float originalSizeOne = -1f;
        internal static float originalSizeTwo = -1f;
        internal static bool firstTime = true; 
        internal static float[] nipValuesOne = new float[3]; //11 12 13
        internal static float[] nipValuesTwo = new float[3]; //11 12 13

        internal static void ApplyHooks(Harmony instance)
        {
            instance.PatchAll(typeof(BEHSceneStart));
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HSprite), "InitHeroine")]
        public static void BEHSceneStartPost(List<ChaControl> ___females, HFlag ___flags)
        {
            if(firstTime)
            {
                for(int i = 0; i < nipValuesOne.Length; i++)
                {
                    nipValuesOne[i] = ___females[0].GetShapeBodyValue(11 + i);
                }

                //BE.Logger.LogMessage(___females[0].GetShapeBodyValue(4) + ___females[1].GetShapeBodyValue(4));
                originalSizeOne = ___females[0].GetShapeBodyValue(4);

                if (___flags.mode == HFlag.EMode.lesbian || ___flags.mode == HFlag.EMode.sonyu3P || ___flags.mode == HFlag.EMode.houshi3P)
                {
                    originalSizeTwo = ___females[1].GetShapeBodyValue(4);
                    for (int i = 0; i < nipValuesTwo.Length; i++)
                    {
                        nipValuesOne[i] = ___females[1].GetShapeBodyValue(11 + i);
                    }
                }
                firstTime = false;
            }
            
        }
    }
}
