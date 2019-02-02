using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using UnityEngine;

namespace TheForestMod
{
    [HarmonyPatch(typeof(PlayerStats))]
    [HarmonyPatch("Update")]
    class PlayerStats_Patch
    {
        [HarmonyPostfix]
        private static void Postfix(PlayerStats __instance)
        {
            // GOD MODE
            if (Manager.enabled_GodMode)
            {
                __instance.Health = 100f;
                __instance.Energy = 100f;
                __instance.Stamina = 100f;
                __instance.Fullness = 100f;
                __instance.Thirst = 0f;
                __instance.Sanity.CurrentSanity = 100f;
            }
        }
    }
}
