using Harmony;
using System;
using UnityEngine;

namespace TheForestMod
{
    public class Manager : UnityEngine.MonoBehaviour
    {
        private static bool showMenu = false;
        private static bool isKeyPressed = false;
        public static bool enabled_GodMode = false;
        public static bool enabled_MovementHack = false;

        public void OnGUI()
        {
            if (showMenu)
            {
                for (int i = 0; i < 10000; i += 20) { GUI.Label(new Rect(5, 0 + i, 200, 30), i.ToString()); } // Y Axis
                for (int i = 0; i < 10000; i += 40) { GUI.Label(new Rect(0 + i, 5, 200, 30), i.ToString()); } // X Axis

                ///--GUI HANDLING---//
                if (GUI.Button(new Rect(10, 10, 300, 30), "Toggle God Mode [" + enabled_GodMode.ToString() + "]"))
                {
                    enabled_GodMode = !enabled_GodMode;
                    FileLog.Log("BUTTON - God Mode: " + enabled_GodMode.ToString());
                }
                else if (GUI.Button(new Rect(10, 50, 300, 30), "Toggle Movement Hacks [" + enabled_MovementHack.ToString() + "]"))
                {
                    enabled_MovementHack = !enabled_MovementHack;
                    FileLog.Log("BUTTON - Movement Hacks: " + enabled_GodMode.ToString());
                }
            }
        }

        public void Update()
        {
            // Toggle GUI Menu
            if (Input.GetKeyDown(KeyCode.F7))
            {
                showMenu = !showMenu;
                //Lock or unlock the view to allow menu control.
                if (showMenu)
                {
                    try { TheForest.Utils.LocalPlayer.FpCharacter.LockView(); }
                    catch (Exception) { }
                }
                else
                {
                    try { TheForest.Utils.LocalPlayer.FpCharacter.UnLockView(); }
                    catch (Exception) { }
                }
            }

            ///--KEYBIND HANDLING---//
            if (Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.GetKeyDown(KeyCode.Keypad0) && !isKeyPressed)
                {
                    enabled_GodMode = !enabled_GodMode;
                    isKeyPressed = true;
                    FileLog.Log("God Mode: " + enabled_GodMode.ToString());
                }
                else if (Input.GetKeyDown(KeyCode.Keypad1) && !isKeyPressed)
                {
                    enabled_MovementHack = !enabled_MovementHack;
                    isKeyPressed = true;
                    FileLog.Log("Movement Hacks: " + enabled_MovementHack.ToString());
                }
                else { isKeyPressed = false; }
            }
        }

        public static void Log(string str)
        {
            FileLog.Log("[" + DateTime.Now.ToString("hh:mm:ss") + "] " + str);
        }
    }
}

/*

if (GUI.Button(new Rect(10, 10, 200, 30), "Toggle God Mode [" + enabled_GodMode.ToString() + "]"))
    {
        enabled_GodMode = !enabled_GodMode;
        isKeyPressed = true;
        FileLog.Log("BUTTON - God Mode: " + enabled_GodMode.ToString());
    }

 */