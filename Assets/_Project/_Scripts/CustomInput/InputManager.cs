using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomInput
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance = default;

        private Keybindings _keybindings = default;
        
        void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
            
            DontDestroyOnLoad(this);
            
            RestoreDefault(); //creates the object, not needed if made a poco
        }

        public static int GetAxis(int player, string axis)
        {
            switch (axis)
            {
                case "horizontal":
                {
                    var left = Input.GetKey(Instance._keybindings.CheckKey(player, "left")) ? -1 : 0;
                    var right = Input.GetKey(Instance._keybindings.CheckKey(player, "right")) ? 1 : 0;
                    return left + right;  
                }
                case "vertical":
                {
                    var up = Input.GetKey(Instance._keybindings.CheckKey(player, "up")) ? 1 : 0;
                    var down = Input.GetKey(Instance._keybindings.CheckKey(player, "down")) ? -1 : 0;
                    return up + down;  
                }
                default:
                    return 0;
            }
        }
        
        public static bool GetKeyPressed(int player, string map)
        {
            return Input.GetKey(Instance._keybindings.CheckKey(player, map));
        }
        
        public static bool GetKeyDown(int player, string map)
        {
            return Input.GetKeyDown(Instance._keybindings.CheckKey(player, map));
        }
        
        public static string GetKeyName(int player, string map)
        {
            return Instance._keybindings.CheckKey(player, map).ToString();
        }
        
        public static void SetKey(int player, string map, KeyCode keyCode)
        {
            Instance._keybindings.SetKeybind(player, map, keyCode);
        }
        
        public static bool KeyCodeIsUsed(KeyCode keyCode)
        {
            return Instance._keybindings.KeyCodeIsUsed(keyCode);
        }

        public void RestoreDefault()
        {
            //this makes a new instance so you wont be able to effect it in the editor once used
            _keybindings = new Keybindings();
            _keybindings.Init();
        }
        
    }
}
