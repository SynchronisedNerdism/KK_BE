using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BE
{
    internal static class OrgasmCounter
    {
        private static int orgasmCounter = 0;
        private static string masOrgasmState;

        internal static void ResetCounter()
        {
            orgasmCounter = 0;
        }
        internal static void IncrementCounter()
        {
            orgasmCounter++;
        }
        internal static int GetCounter()
        {
            return orgasmCounter;
        }

        internal static bool CheckCounter(int orgasmCount)
        {
            if(orgasmCounter != orgasmCount)
            {
                orgasmCounter = orgasmCount;
                return true;
            }
            return false;
        }

        internal static void ChangeMasOrgasmState(string name)
        {
            masOrgasmState = name;
        }

        internal static string GetMasOrgasmState()
        {
            return masOrgasmState;
        }

       
    }
}
