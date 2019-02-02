using Harmony;
using System;
using System.Reflection;
using UnityEngine;

namespace TheForestMod
{
    public class MainPatcher
    {
        public static UnityEngine.GameObject cheatObject;

        private static void Patch()
        {
            var harmony = HarmonyInstance.Create("com.z0f.TheForestMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            cheatObject = new UnityEngine.GameObject();
            cheatObject.AddComponent<TheForestMod.Manager>();
            UnityEngine.Object.DontDestroyOnLoad(cheatObject);

            //Debug log.
            FileLog.Log("\n------" + DateTime.Now.ToString("MMM ddd d hh:mm:ss yyyy") + "------" + "\n-Log for The Forest cheat-\nCheat made by Z0F using Harmony.");
        }
    }
}