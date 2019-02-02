using Harmony;
using System;
using UnityEngine;

namespace TheForestMod
{
    public class Manager : UnityEngine.MonoBehaviour
    {
        private string playerX = "0", playerY = "0", playerZ = "0";
        private static bool isKeyPressed = false;
        private static bool enabled_Menu = false;
        public static bool enabled_GodMode = false;
        public static bool enabled_MovementHack = false;
        public static bool enabled_PinLocation = false;

        private void Start()
        {
        }

        private void OnGUI()
        {
            if (enabled_Menu)
            {
                for (int i = 0; i < 10000; i += 20) { GUI.Label(new Rect(5, 0 + i, 200, 30), i.ToString()); } // Y Axis
                for (int i = 0; i < 10000; i += 40) { GUI.Label(new Rect(0 + i, 5, 200, 30), i.ToString()); } // X Axis

                // Main Menu:
                GUI.Box(new Rect(Screen.width - 300, 0, 300, 300), "- Hack by Z0F -");
                // Location Menu:
                GUI.Box(new Rect(Screen.width - 300, 300, 300, 200), "- Posistion Hacks -");

                ///--GUI HANDLING---//
                if (GUI.Button(new Rect(Screen.width - 275, 35, 250, 30),
                    new GUIContent("God Mode [" + enabled_GodMode.ToString() + "]", "Infinte Health, Stamana, Hunger, etc.")))
                {
                    enabled_GodMode = !enabled_GodMode;
                    FileLog.Log("GUI - God Mode: " + enabled_GodMode.ToString());
                }
                else if (GUI.Button(new Rect(Screen.width - 275, 75, 250, 30),
                    new GUIContent("Movement Hacks [" + enabled_MovementHack.ToString() + "]", "Run and swim faster, jump higher.")))
                {
                    enabled_MovementHack = !enabled_MovementHack;
                    FileLog.Log("GUI - Movement Hacks: " + enabled_GodMode.ToString());
                }
                else if (GUI.Button(new Rect(Screen.width - 275, 115, 250, 30),
                   new GUIContent("Pin Player Coords [" + enabled_PinLocation.ToString() + "]", "Show player coords when the menu is closed.")))
                {
                    enabled_PinLocation = !enabled_PinLocation;
                    FileLog.Log("Pin Player Coords: " + enabled_PinLocation.ToString());
                }
                else if (GUI.Button(new Rect(Screen.width - 275, 470, 250, 30),
                   new GUIContent("Teleport To Coords", "Teleport to specified coordinates.")))
                {
                    try
                    {
                        Vector3 newPlayerLocation = new Vector3(float.Parse(playerX), float.Parse(playerY), float.Parse(playerZ));
                        TheForest.Utils.LocalPlayer.Rigidbody.velocity = Vector3.zero;
                        TheForest.Utils.LocalPlayer.Transform.position = newPlayerLocation;
                        FileLog.Log("Teleporting to: " + newPlayerLocation.ToString());
                    }
                    catch (Exception) { }
                }

                playerX = GUI.TextField(new Rect(Screen.width - 225, 335, 50, 30), playerX);
                playerY = GUI.TextField(new Rect(Screen.width - 175, 335, 50, 30), playerY);
                playerZ = GUI.TextField(new Rect(Screen.width - 125, 335, 50, 30), playerZ);

                // Tooltip:
                GUI.Label(new Rect(Screen.width - 295, 278, 290, 22), GUI.tooltip);
            }
            if (enabled_Menu || enabled_PinLocation)
            {
                // Player Location:
                try
                {
                    Vector3 playerLocation = TheForest.Utils.LocalPlayer.FpCharacter.transform.position;
                    GUI.Label(new Rect(Screen.width - 185, Screen.height - 25, 200, 25), playerLocation.ToString());
                }
                catch (Exception)
                {
                    GUI.Label(new Rect(Screen.width - 185, Screen.height - 25, 200, 25), "(Player Location)");
                }
            }
        }

        public void Update()
        {
            // Toggle GUI Menu
            if (Input.GetKeyDown(KeyCode.F5))
            {
                enabled_Menu = !enabled_Menu;
                //Lock or unlock the view to allow menu control.
                if (enabled_Menu)
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

Toggler:
enabled_GodMode = GUI.Toggle(new Rect(Screen.width - 275, 115, 250, 30), enabled_GodMode, new GUIContent("TOGGLE God Mode [" + enabled_GodMode.ToString() + "]", "TOGGLERRR"), "button");

Basic Button:
if (GUI.Button(new Rect(10, 10, 200, 30), "Toggle God Mode [" + enabled_GodMode.ToString() + "]"))
    {
        enabled_GodMode = !enabled_GodMode;
        FileLog.Log("BUTTON - God Mode: " + enabled_GodMode.ToString());
    }

 */