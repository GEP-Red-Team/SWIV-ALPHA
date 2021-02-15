using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class GameData : MonoBehaviour
    {
        [Header("GAME INFO")]
        [SerializeField] private GameMode gameMode = GameMode.OnePlayer;
        [SerializeField] private int level = 1;
        [SerializeField] private bool isPaused = false;
        public bool IsPaused { set => isPaused = value; get => isPaused; }


        [Header("PLAYER INFO")] 
        [SerializeField] private int lives = 3;
        [SerializeField] private int currentScore = 0;
        
        [SerializeField] private bool gunPowerUp = false;
        [SerializeField] private bool shieldPowerUp = false;
    }
}
