using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using BepInEx.Configuration;

namespace BE
{
    [BepInPlugin(GUID, "BE", Version)]
    [BepInProcess(GameProcessName)]
    [BepInProcess(GameProcessNameSteam)]
    public class BE : BaseUnityPlugin
    {
        private const string GUID = "BE";
        private const string Version = "1.1";
        private const string GameProcessName = "Koikatu";
        private const string GameProcessNameSteam = "Koikatsu Party";

        internal static BE Instance;
        internal static new ManualLogSource Logger;

        internal static ConfigEntry<float> StartSize;
        internal static ConfigEntry<float> EndSize;
        internal static ConfigEntry<bool> AttachedMale;
        internal static ConfigEntry<InterpolationType> SetInterpolationType;
        internal static ConfigEntry<int> NumberofOrgasms;
        internal static ConfigEntry<bool> StayatMax;
        internal static ConfigEntry<bool> IsInstant;
        internal static ConfigEntry<bool> IsDisabled;
        internal static ConfigEntry<bool> IsOriginalSize;
        internal static ConfigEntry<float> ChangeSize;
        private void Awake()
        {
            try
            {
                var i = new Harmony(GUID);
                Instance = this;
                Logger = base.Logger;
                BEFemaleGaugeHook.ApplyHooks(i);
                BEMaleGaugeHook.ApplyHooks(i);
                BEMasturbationHook.ApplyHooks(i);
                BEHSceneEnd.ApplyHooks(i);
                BEHSceneStart.ApplyHooks(i);
                String[] headerText = new String[] { "General Settings", "Original Size Growth", "Set Original Size" };                 
            

                StartSize = Config.Bind(headerText[2], "Start Size", 0.0f, "Adjust Start Size of BE(0 is normal)");
                EndSize = Config.Bind(headerText[2], "End Size", 0.7f, "Adjust End Size of BE");
                NumberofOrgasms = Config.Bind(headerText[0], "Orgasms to max size", 1, "Number of orgasms till bust reaches max size");
                AttachedMale = Config.Bind(headerText[0], "Is Attached to Male Gauge", false, "Bust size will be based on male gauge rather than female gauge");
                StayatMax = Config.Bind(headerText[0], "Stay at max size", false, "Whether heroine will stay at max bust size once reached");
                IsInstant = Config.Bind(headerText[0], "BE is instant", false, "Whether BE will be instant when orgasming");
                IsDisabled = Config.Bind(headerText[0], "Disable BE", false, "Disable the BE mod");
                IsOriginalSize = Config.Bind(headerText[1], "Is at Original Size", false, "Whether BE will start at original size(set Endsize to be larger than this size)");
                ChangeSize = Config.Bind(headerText[1], "Change Size", 0.7f, "Adjust End Size of BE");
                SetInterpolationType = Config.Bind(headerText[0], "Scaling Type", InterpolationType.Linear, "Choose the type of bust scaling\nLinear will\nBezier will\nLogarithmic will\nExponenial Will");
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                Logger.LogError("BE Mod has trouble hooking onto Koikatsu");
            }
            
        }        
}
    public enum InterpolationType
    {
        Linear,
        Quadratic,
        Logarithmic,
        Exponential
    }
}
