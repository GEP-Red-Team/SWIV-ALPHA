using UnityEngine;

namespace CustomInput
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        [SerializeField] private Keybindings keybindings = default;

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
            return Input.GetKeyDown(Instance.keybindings.CheckKey(1, map));
        }
        
        public static string GetKey(string map)
        {
            return Instance.keybindings.CheckKey(1, map).ToString();
        }

        public static void SetKey(string map, KeyCode keyCode)
        {
            Instance.keybindings.SetKeybind(1, map, keyCode);
        }

        public void RestoreDefault()
        {
            //this makes a new instance so you wont be able to effect it in the editor once used
            keybindings = ScriptableObject.CreateInstance<Keybindings>();
        }
    }
}
