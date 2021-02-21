using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sound
{
    public class AddButtonSounds : MonoBehaviour
    {
        private void Start()
        {
            var button = GetComponent<Button>().onClick;
            button.AddListener(() => SoundManager.PlaySound(SoundManager.Sound.ButtonClick));
        }
    }
}
