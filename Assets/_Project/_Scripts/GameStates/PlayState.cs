using CustomInput;
using UnityEngine;

namespace GameStates
{
    public class PlayState : GameState
    {
        public PlayState(StateMachine stateMachine) : base(stateMachine) {}

        public override void Start()
        {
            Debug.Log("START() :: PLAY STATE");
            CanPause = true;

            /*Game.LevelData.LoadLevel(1);
            Game.LevelData.init();*/
        }

        public override void Update()
        {
            if (InputManager.KeyDown("pause"))
            {
                Game.OnPause();
            }
        }

        public override void End()
        {
            Debug.Log("START() :: PLAY STATE");
        }
    }
}
