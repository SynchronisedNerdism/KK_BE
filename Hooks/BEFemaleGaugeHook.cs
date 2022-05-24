using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BE
{
    internal static class BEFemaleGaugeHook
    {
        //fem = UnityEngine.Object.FindObjectOfType(CharFemale)

        internal static void ApplyHooks(Harmony instance)
        {
            instance.PatchAll(typeof(BEFemaleGaugeHook));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(HFlag), "FemaleGaugeUp")]
        public static void BEFemaleGaugeUpPost(HFlag __instance)
        {
            if (BE.IsInstant.Value == true && BE.AttachedMale.Value == false && BE.IsDisabled.Value == false)
            {
                if (OrgasmCounter.CheckCounter(__instance.GetOrgCount())==true)
                {
                    UpdateBE(__instance);
                }
            }
            else if (BE.AttachedMale.Value == false && BE.IsDisabled.Value == false)
            {
                UpdateBE(__instance);
                
            }
        }
        
        public static SaveData.Heroine BEGetTargetHeroine(HFlag __instance)
        {
            return __instance.lstHeroine[BEGetTargetHeroineId(__instance)];
        }
        
        public static int BEGetTargetHeroineId(HFlag __instance)
        {
            return 0;
            //return (__instance.mode == HFlag.EMode.houshi3P || __instance.mode == HFlag.EMode.sonyu3P) ? (__instance.nowAnimationInfo.id % 2) : 0;
        }

        public static void UpdateBE(HFlag __instance)
        {
            for (int i = 0; i < BEHSceneStart.nipValuesOne.Length; i++)
            {
                BEGetTargetHeroine(__instance).chaCtrl.SetShapeBodyValue(11 + i, BEHSceneStart.nipValuesOne[i]);
            }
            if (BE.IsOriginalSize.Value == true)
            {
                BEGetTargetHeroine(__instance).chaCtrl.SetShapeBodyValue(4, BEOriginalInterpolateSize(__instance, true));
                BEGetTargetHeroine(__instance).chaCtrl.updateShapeBody = true;
                BEGetTargetHeroine(__instance).chaCtrl.UpdateBustSoftnessAndGravity();
                BEGetTargetHeroine(__instance).chaCtrl.updateBustSize = true;
                BEGetTargetHeroine(__instance).chaCtrl.reSetupDynamicBoneBust = true;

                if (__instance.mode == HFlag.EMode.lesbian || __instance.mode == HFlag.EMode.sonyu3P || __instance.mode == HFlag.EMode.houshi3P)
                {
                    for (int i = 0; i < BEHSceneStart.nipValuesTwo.Length; i++)
                    {
                        __instance.lstHeroine[1].chaCtrl.SetShapeBodyValue(11 + i, BEHSceneStart.nipValuesTwo[i]);
                    }
                    __instance.lstHeroine[1].chaCtrl.SetShapeBodyValue(4, BEOriginalInterpolateSize(__instance, false));
                    __instance.lstHeroine[1].chaCtrl.updateShapeBody = true;
                    __instance.lstHeroine[1].chaCtrl.UpdateBustSoftnessAndGravity();
                    __instance.lstHeroine[1].chaCtrl.updateBustSize = true;
                    __instance.lstHeroine[1].chaCtrl.reSetupDynamicBoneBust = true;
                }
            }
            else
            {
                BEGetTargetHeroine(__instance).chaCtrl.SetShapeBodyValue(4, BEInterpolateSize(__instance));
                BEGetTargetHeroine(__instance).chaCtrl.updateShapeBody = true;
                BEGetTargetHeroine(__instance).chaCtrl.UpdateBustSoftnessAndGravity();
                BEGetTargetHeroine(__instance).chaCtrl.updateBustSize = true;
                BEGetTargetHeroine(__instance).chaCtrl.reSetupDynamicBoneBust = true;

                if (__instance.mode == HFlag.EMode.lesbian || __instance.mode == HFlag.EMode.sonyu3P || __instance.mode == HFlag.EMode.houshi3P)
                {
                    for (int i = 0; i < BEHSceneStart.nipValuesTwo.Length; i++)
                    {
                        __instance.lstHeroine[1].chaCtrl.SetShapeBodyValue(11 + i, BEHSceneStart.nipValuesTwo[i]);
                    }
                    __instance.lstHeroine[1].chaCtrl.SetShapeBodyValue(4, BEInterpolateSize(__instance));
                    __instance.lstHeroine[1].chaCtrl.updateShapeBody = true;
                    __instance.lstHeroine[1].chaCtrl.UpdateBustSoftnessAndGravity();
                    __instance.lstHeroine[1].chaCtrl.updateBustSize = true;
                    __instance.lstHeroine[1].chaCtrl.reSetupDynamicBoneBust = true;
                }
            }
        }
        

        public static float BEGetCurrentFemaleGauge(HFlag __instance)
        {
            return __instance.gaugeFemale;
        }
        public static AnimatorStateInfo BEGetAnimatorStateInfo(HFlag __instance)
        {
            return BEGetTargetHeroine(__instance).chaCtrl.getAnimatorStateInfo(0);
        }

        public static float BEInterpolateSize(HFlag __instance)
        {
            if (BE.StayatMax.Value == true && (__instance.GetOrgCount() >= BE.NumberofOrgasms.Value))
            {
                return BE.EndSize.Value;
            }
            return BE.StartSize.Value + BECalculator.CalculateBE(BEGetOrgasmCount(__instance), BEGetCurrentFemaleGauge(__instance) / 100) * (BE.EndSize.Value-BE.StartSize.Value);
        }

        public static float BEOriginalInterpolateSize(HFlag __instance, bool isFirst)
        {
            if (BE.StayatMax.Value == true && (__instance.GetOrgCount() >= BE.NumberofOrgasms.Value))
            {
                if (isFirst)
                {
                    return BEHSceneStart.originalSizeOne + BE.ChangeSize.Value;
                }
                else
                {
                    return BEHSceneStart.originalSizeTwo + BE.ChangeSize.Value;
                }

            }
            if (isFirst)
            {
                return BEHSceneStart.originalSizeOne + BECalculator.CalculateBE(BEGetOrgasmCount(__instance), BEGetCurrentFemaleGauge(__instance) / 100)* (BE.ChangeSize.Value);
            }
            else
            {
                return BEHSceneStart.originalSizeTwo + BECalculator.CalculateBE(BEGetOrgasmCount(__instance), BEGetCurrentFemaleGauge(__instance) / 100) * (BE.ChangeSize.Value);
            }
           
        }
        private static int BEGetOrgasmCount(HFlag __instance)
        {
            //BE.Logger.LogMessage("HFlag Count: " + __instance.count);
            return __instance.GetOrgCount()% BE.NumberofOrgasms.Value;
        }

    }
}
