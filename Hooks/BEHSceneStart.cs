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

        internal static void ApplyHooks(Harmony instance)
        {
            instance.PatchAll(typeof(BEHSceneStart));
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HSprite), "InitHeroine")]
        public static void BEHSceneStartPost(List<ChaControl> ___females, HFlag ___flags)
        {
            //BE.Logger.LogMessage(___females[0].GetShapeBodyValue(4) + ___females[1].GetShapeBodyValue(4));
            originalSizeOne = ___females[0].GetShapeBodyValue(4);

            if (___flags.mode == HFlag.EMode.lesbian || ___flags.mode == HFlag.EMode.sonyu3P || ___flags.mode == HFlag.EMode.houshi3P)
            {
                originalSizeTwo = ___females[1].GetShapeBodyValue(4);
            }
        }
    }
}
