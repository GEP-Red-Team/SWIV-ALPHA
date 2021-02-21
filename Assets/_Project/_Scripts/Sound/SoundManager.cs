using System;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.UI;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance = default;

        public enum Sound
        {
            Fire,
            Death,
            ShieldUp,
            ShieldDown,
            ButtonClick
        }
        
        public enum ParentState
        {
            None,
            Menu,
            Play,
            Pause
        }

        [Header("StateObjects")] 
        [SerializeField] private GameObject menuObjects = default;
        [SerializeField] private GameObject playObjects = default;
        [SerializeField] private GameObject pauseObjects = default;
        
        [Header("Audio Clips")]
        [SerializeField] private AudioClip fire = default;
        [SerializeField] private AudioClip death = default;
        [SerializeField] private AudioClip shieldUp = default;
        [SerializeField] private AudioClip shieldDown = default;
        [SerializeField] private AudioClip buttonClick = default;
        
        [SerializeField] private Dictionary<Sound, AudioClip> _gameSounds = new Dictionary<Sound, AudioClip>();

        private GameObject _menuSounds = default;
        private GameObject _playSounds = default;
        private GameObject _pauseSounds = default;
        
        private static bool IsMenuSoundNull => Instance._menuSounds == null;
        private static bool IsPlaySoundNull => Instance._playSounds == null;
        private static bool IsPauseSoundNull => Instance._pauseSounds == null;

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
            
            Instance._gameSounds.Add(Sound.Fire, fire);
            Instance._gameSounds.Add(Sound.Death, death);
            Instance._gameSounds.Add(Sound.ShieldUp, shieldUp);
            Instance._gameSounds.Add(Sound.ShieldDown, shieldDown);
            Instance._gameSounds.Add(Sound.ButtonClick, buttonClick);
        }
        
        public static void PlaySound(Sound sound, ParentState parentState = ParentState.None)
        {
            switch (parentState)
            {
                case ParentState.None:
                    if (IsMenuSoundNull) Instance._menuSounds = CreateStateAudio(Instance.transform);
                    Instance._menuSounds.GetComponent<AudioSource>().PlayOneShot(Instance._gameSounds[sound]);
                    break;
                
                case ParentState.Menu:
                    if (IsMenuSoundNull) Instance._menuSounds = CreateStateAudio(Instance.menuObjects.transform);
                    Instance._menuSounds.GetComponent<AudioSource>().PlayOneShot(Instance._gameSounds[sound]);
                    break;
                
                case ParentState.Play:
                    if (IsPlaySoundNull) Instance._playSounds = CreateStateAudio(Instance.playObjects.transform);
                    Instance._playSounds.GetComponent<AudioSource>().PlayOneShot(Instance._gameSounds[sound]);
                    break;
                
                case ParentState.Pause:
                    if (IsPauseSoundNull) Instance._pauseSounds = CreateStateAudio(Instance.pauseObjects.transform);
                    Instance._pauseSounds.GetComponent<AudioSource>().PlayOneShot(Instance._gameSounds[sound]);
                    break;
                
                default:
                    return;
            }
        }

        static GameObject CreateStateAudio(Transform parent)
        {
            var go = new GameObject("StateSound");
            go.transform.SetParent(parent);
            go.AddComponent<AudioSource>();
            return go;
        }
    }
}
