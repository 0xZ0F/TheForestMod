using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using UnityEngine;

namespace TheForestMod
{
    [HarmonyPatch(typeof(FirstPersonCharacter))]
    [HarmonyPatch("Update")]
    class FirstPersonCharacter_Patch
    {
        [HarmonyPrefix]
        private static void Prefix(FirstPersonCharacter __instance)
        {
            // GOD MODE
            if (Manager.enabled_GodMode)
            {
                __instance.allowFallDamage = false;
            }
        }

        [HarmonyPostfix]
        private static void Postfix(FirstPersonCharacter __instance)
        {
            // MOVEMENT HACK
            if (Manager.enabled_MovementHack)
            {
                __instance.runSpeed = 50f;
                __instance.jumpHeight = 100f;
                __instance.swimmingSpeed = 20f;
            }
        }
    }
}
