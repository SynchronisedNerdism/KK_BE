using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BE
{
    internal static class BEMasturbationHook
    {
        internal static void ApplyHooks(Harmony instance)
        {
            instance.PatchAll(typeof(BEMasturbationHook));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(HMasturbation),"Proc")]
        public static void BEFinishGaugeDownPost(ChaControl ___female, ref HFlag ___flags)
        {
            if (BE.IsDisabled.Value == false)
            {   
                if (___female.getAnimatorStateInfo(0).IsName("Orgasm_B"))
                {
                    OrgasmCounter.ChangeMasOrgasmState("Orgasm_B");
                }
                if (!(___female.getAnimatorStateInfo(0).IsName(OrgasmCounter.GetMasOrgasmState())) && OrgasmCounter.GetMasOrgasmState() == "Orgasm_B")
                {
                    OrgasmCounter.ChangeMasOrgasmState("None");
                    ___flags.count.aibuOrg++;
                }
            }
            
        }
    }
}
