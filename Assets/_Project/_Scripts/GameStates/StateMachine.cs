using System;
using UnityEngine;
using Data;
using Random = UnityEngine.Random;

namespace GameStates
{
    public class StateMachine : MonoBehaviour
    {
        private GameState _gameState = default;                //Cant serialise abstract class   
        private GameState _pausedState = default;              //  
        
        [SerializeField] private GameData gameData = default;  //Object containing all of the game data
        public GameData GameData { get => gameData; set => gameData = value; }
        public MenuObjects menuObjects = default;
        public PauseObjects pauseObjects = default;

        private void OnEnable()
        {
            SetState(new MenuState(this)); //Sets the first state
            DontDestroyOnLoad(this);    
        }
        
        public void SetState(GameState newState)
        {
            _gameState?.End();          //If there is a gamestate? call end()
            _gameState = newState;      //Set the gamestate to the new gamestate you pass in 
            _gameState.Start();         //Run the start method of the new gamestate
        }

        void Update()
        {
            _gameState.Update();
        }

        public void OnPause()
        {
            if (_gameState.CanPause)
            {
                _pausedState = _gameState;
                _gameState = new PauseState(this);
                _gameState.Start();
            }
            else if (gameData.IsPaused)
            {
                _gameState.End();
                _gameState = _pausedState;
                _pausedState = null;
            }
        }
    }
}

