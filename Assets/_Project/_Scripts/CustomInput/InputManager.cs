using System.Collections.Generic;
using UnityEngine;

namespace CustomInput
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

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
        
        public static bool KeyDown(string map)
        {
            return Input.GetKeyDown(Instance._keybindings.CheckKey(1, map));
        }
        
        public static string GetKey(int player, string map)
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
