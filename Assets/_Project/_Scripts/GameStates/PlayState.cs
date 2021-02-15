using UnityEngine;

namespace GameStates
{
    public class PlayState : GameState
    {
        public PlayState(StateMachine stateMachine) : base(stateMachine) {}

        private const int GAME_STARTING_LIVES = 3;

        public override void Start()
        {
            Debug.Log("START() :: PLAY STATE");
            CanPause = true;

            // Reset game state.
            Game.GameData.lives = GAME_STARTING_LIVES;
            Game.GameData.currentScore = 0;

            // Show game state object.
            Game.GameData.playstateObjects.SetActive(true);
        }

        public override void Update()
        {
            
        }

        public override void End()
        {
            Debug.Log("START() :: PLAY STATE");

            // Hide game state object.
            Game.GameData.playstateObjects.SetActive(false);
        }
    }
}
