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
            PauseObjects.pauseParent.SetActive(true);
            
            //Register callbacks
            PauseObjects.resumeButton.onClick.AddListener(OnResume);
            PauseObjects.restartButton.onClick.AddListener(OnRestart);
            PauseObjects.mainMenuButton.onClick.AddListener(OnMenu);
        }
        public override void Update()
        {
            
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
            Game.OnPause();
        }

        private void OnRestart()
        {
            //not sure how this will work
        }

        private void OnMenu()
        {
            Game.OnPause();
            Game.SetState(new MenuState(Game));
        }


    }
}
