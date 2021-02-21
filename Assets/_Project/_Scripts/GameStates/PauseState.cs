using CustomInput;
using Data;
using UnityEngine;

namespace GameStates
{
    public class PauseState : GameState
    {
        public PauseState(StateMachine stateMachine) : base(stateMachine) {}
        private PauseObjects PauseObjects => Game.pauseObjects;
        
        public override void Start()
        {
            //Tell the game data and activate the scene objects
            Game.GameData.IsPaused = true;
            Game.GameData.playstateObjects.SetActive(false);
            PauseObjects.pauseParent.SetActive(true);
            
            //Register callbacks
            PauseObjects.resumeButton.onClick.AddListener(OnResume);
            PauseObjects.restartButton.onClick.AddListener(OnRestart);
            PauseObjects.mainMenuButton.onClick.AddListener(OnMenu);
            PauseObjects.mainMenuButton.onClick.AddListener(OnMenu);
        }
        public override void Update()
        {
            if (InputManager.GetKeyDown(1, "pause"))
            {
                OnResume();
            }
        }
        public override void End()
        {
            //Tell the game data and deactivate the scene objects
            Game.GameData.IsPaused = false;
            PauseObjects.pauseParent.SetActive(false);
            
            //Remove callbacks
            PauseObjects.resumeButton.onClick.RemoveListener(OnResume);
            PauseObjects.restartButton.onClick.RemoveListener(OnRestart);
            PauseObjects.mainMenuButton.onClick.RemoveListener(OnMenu);
        }

        private void OnResume()
        {
            Game.GameData.playstateObjects.SetActive(true);
            Game.OnPause();
        }

        private void OnRestart()
        {
            //not sure how this will work
            //could just create a new play state??
        }

        private void OnMenu()
        {
            Game.OnPause();
            Game.SetState(new MenuState(Game));
        }
    }
}
