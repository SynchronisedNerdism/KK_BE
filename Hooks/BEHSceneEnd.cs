using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace BE
{
    internal static class BEHSceneEnd
    {
        internal static void ApplyHooks(Harmony instance)
        {
            instance.PatchAll(typeof(BEHSceneEnd));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(HSprite), "OnClickHSceneEnd")]
        public static void BEHSceneEndPost()
        {
            OrgasmCounter.ResetCounter();
            OrgasmCounter.ChangeMasOrgasmState("None");
            BEHSceneStart.originalSizeOne = -1;
            BEHSceneStart.originalSizeTwo = -1;
        }
    }
}
