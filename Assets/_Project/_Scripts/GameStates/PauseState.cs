using UnityEngine;

namespace GameStates
{
    public class PauseState : GameState
    {
        public PauseState(StateMachine stateMachine) : base(stateMachine) {}
    
        public override void Start()
        {
            Debug.Log("START() :: PAUSE STATE");
            Game.GameData.IsPaused = true;

        }
        public override void Update()
        {
            
        }
        public override void End()
        {
            Debug.Log("END()   :: PAUSE STATE");
            Game.GameData.IsPaused = false;
        }


    }
}
