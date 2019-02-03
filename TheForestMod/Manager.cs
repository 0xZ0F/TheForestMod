using Harmony;
using System;
using UnityEngine;

namespace TheForestMod
{
    public class Manager : UnityEngine.MonoBehaviour
    {
        private string playerX = "0", playerY = "0", playerZ = "0";
        private static int toolbar = 0;
        private string[] toolbarLabels = { "Cheats", "Player", "Location" };

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
            if (enabled_Menu) //Cheats
            {
                GUI.Box(new Rect(25, 30, 300, 30), "");
                toolbar = GUI.Toolbar(new Rect(25, 30, 250, 30), toolbar, toolbarLabels);

                for (int i = 0; i < 10000; i += 20) { GUI.Label(new Rect(5, 0 + i, 200, 30), i.ToString()); } // Y Axis
                for (int i = 0; i < 10000; i += 40) { GUI.Label(new Rect(0 + i, 5, 200, 30), i.ToString()); } // X Axis
                if (toolbar == 0) //Cheats
                {
                    GUI.Box(new Rect(25, 60, 300, 300), "Cheats", GUI.skin.window);

                    GUI.Label(new Rect(30, 75, 150, 20), "God Mode:");
                    enabled_GodMode = GUI.Toggle(new Rect(160, 75, 150, 20), enabled_GodMode, new GUIContent("[" + enabled_GodMode.ToString() + "]", "Infinite health, stamina, hunger, etc."));
                    // Add instant tree, noclip, freecam, save game.
                }
                else if (toolbar == 1) //Player
                {
                    GUI.Box(new Rect(25, 60, 300, 300), "Player", GUI.skin.window);
                }
                else if (toolbar == 2) //Location
                {
                    GUI.Box(new Rect(25, 60, 300, 300), "Location (Coords in bottom right)", GUI.skin.window);

                    GUI.Label(new Rect(30, 75, 150, 20), "Pin Player Coords:");
                    enabled_PinLocation = GUI.Toggle(new Rect(160, 75, 150, 20), enabled_PinLocation, new GUIContent("[" + enabled_PinLocation.ToString() + "]", "Show player coords when the menu is closed."));

                    // Teleport to coords provided:
                    if (GUI.Button(new Rect(50, 320, 250, 30), new GUIContent("Teleport To Coords", "Teleport to specified coordinates.")))
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

                    playerX = GUI.TextField(new Rect(100, 280, 50, 30), playerX);
                    playerY = GUI.TextField(new Rect(150, 280, 50, 30), playerY);
                    playerZ = GUI.TextField(new Rect(200, 280, 50, 30), playerZ);
                }

                // Tooltip:
                GUI.Label(new Rect(25, 360, 300, 30), GUI.tooltip);
            }
            // Display player location:
            if (enabled_Menu || enabled_PinLocation)
            {
                try
                {
                    Vector3 playerLocation = TheForest.Utils.LocalPlayer.FpCharacter.transform.position;
                    GUI.Label(new Rect(Screen.width - 185, Screen.height - 25, 200, 25), playerLocation.ToString());
                }
                catch (Exception)
                {
                    GUI.Label(new Rect(Screen.width - 185, Screen.height - 25, 200, 25), "(Player Coordinates)");
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