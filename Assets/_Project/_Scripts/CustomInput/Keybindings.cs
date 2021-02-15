using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomInput
{
    [CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
    public class Keybindings : ScriptableObject
    {
        private Dictionary<string, KeyCode> _player1 = new Dictionary<string, KeyCode>();
        private Dictionary<string, KeyCode> _player2 = new Dictionary<string, KeyCode>();
        
        private void OnEnable()
        {
            _player1.Add("shoot",  KeyCode.Space);
            _player1.Add("select", KeyCode.Joystick1Button0);
            _player1.Add("pause",  KeyCode.Escape);
            _player1.Add("up",     KeyCode.W);
            _player1.Add("down",   KeyCode.S);
            _player1.Add("left",   KeyCode.A);
            _player1.Add("right",  KeyCode.D);
            
            _player2.Add("shoot",  KeyCode.Keypad0);
            _player2.Add("select", KeyCode.KeypadEnter);
            _player2.Add("pause",  KeyCode.KeypadPeriod);
            _player2.Add("up",     KeyCode.Keypad8);
            _player2.Add("down",   KeyCode.Keypad5);
            _player2.Add("left",   KeyCode.Keypad4);
            _player2.Add("right",  KeyCode.Keypad6);
        }

        public KeyCode CheckKey(int player, string map)
        {
            return player == 1 ? _player1[map] : _player2[map];
        }
        
        public void SetKeybind(int player, string map, KeyCode keyCode)
        {
            switch (player)
            {
                case 1:
                    _player1[map] = keyCode;
                    break;
                case 2:
                    _player2[map] = keyCode;
                    break;
            }
        }
    }
}
