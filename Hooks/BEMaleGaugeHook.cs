using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BE
{
    internal static class BEMaleGaugeHook
    {
        internal static void ApplyHooks(Harmony instance)
        {
            instance.PatchAll(typeof(BEMaleGaugeHook));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(HFlag), "MaleGaugeUp")]
        public static void BEMaleGaugeUpPost(HFlag __instance)
        {
            if (BE.IsInstant.Value == true && BE.AttachedMale.Value == true && BE.IsDisabled.Value == false)
            {
                if (OrgasmCounter.CheckCounter(BEGetMaleOrgasmCount(__instance)) ==true)
                {
                    UpdateBE(__instance);  
                }
            }
            else if (BE.AttachedMale.Value == true && BE.IsDisabled.Value == false)
            {
                UpdateBE(__instance);
            }
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
                
                return BEHSceneStart.originalSizeOne + BECalculator.CalculateBE(BEGetOrgasmCount(__instance), BEGetCurrentMaleGauge(__instance) / 100) * (BE.ChangeSize.Value);
            }
            else
            {
                return BEHSceneStart.originalSizeTwo + BECalculator.CalculateBE(BEGetOrgasmCount(__instance), BEGetCurrentMaleGauge(__instance) / 100) * (BE.ChangeSize.Value);
            }

        }
        public static SaveData.Heroine BEGetTargetHeroine(HFlag __instance)
        {
            return __instance.lstHeroine[BEGetTargetHeroineId(__instance)];
        }

        public static SaveData.Player BEGetPlayer(HFlag __instance)
        {
            return __instance.player;
        }

        public static int BEGetTargetHeroineId(HFlag __instance)
        {
            return 0;
            //return (__instance.mode == HFlag.EMode.houshi3P || __instance.mode == HFlag.EMode.sonyu3P) ? (__instance.nowAnimationInfo.id % 2) : 0;
        }

        public static float BEGetCurrentMaleGauge(HFlag __instance)
        {
            return __instance.gaugeMale;
        }

        public static float BEInterpolateSize(HFlag __instance)
        {
            if (BE.StayatMax.Value == true && BEGetMaleOrgasmCount(__instance) >= BE.NumberofOrgasms.Value)
            {
                return BE.EndSize.Value;
            }
            return BE.StartSize.Value + BECalculator.CalculateBE(BEGetOrgasmCount(__instance), BEGetCurrentMaleGauge(__instance) / 100) * (BE.EndSize.Value-BE.StartSize.Value);
        }

        
        private static int BEGetOrgasmCount(HFlag __instance)
        {
            //BE.Logger.LogMessage("Splash: " + __instance.count.splash);
            //BE.Logger.LogMessage("Kuwae: " + __instance.count.kuwaeFinish);
            //BE.Logger.LogMessage("Name: " + __instance.count.nameFinish);
            // BE.Logger.LogMessage("houshiInside: " + __instance.count.houshiInside);
            // BE.Logger.LogMessage("sonyuInside: " + __instance.count.sonyuInside);
            // BE.Logger.LogMessage("sonyuAnalInside: " + __instance.count.sonyuAnalInside);
            //  BE.Logger.LogMessage("finish: " + __instance.finish);
            //  BE.Logger.LogMessage("houshiinside: " + __instance.count.houshiInside);
            //  BE.Logger.LogMessage("houshi outisde: " + __instance.count.houshiOutside);
            //   BE.Logger.LogMessage("handfinish: " + __instance.count.handFinish);
            //BE.Logger.LogMessage(__instance.count.houshiInside + __instance.count.houshiOutside + __instance.count.sonyuInside + __instance.count.sonyuOutside + __instance.count.sonyuAnalInside + __instance.count.sonyuAnalOutside + __instance.count.sonyuAnalCondomInside + __instance.count.sonyuCondomInside);
            return BEGetMaleOrgasmCount(__instance) % BE.NumberofOrgasms.Value;
        }

        private static int BEGetMaleOrgasmCount(HFlag __instance)
        {
            return __instance.count.houshiInside + __instance.count.houshiOutside + __instance.count.sonyuInside + __instance.count.sonyuOutside + __instance.count.sonyuAnalInside + __instance.count.sonyuAnalOutside + __instance.count.sonyuAnalCondomInside + __instance.count.sonyuCondomInside;
        }

    }
}
