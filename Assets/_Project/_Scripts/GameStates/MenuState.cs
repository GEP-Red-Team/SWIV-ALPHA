using System;
using CustomInput;
using UnityEditor;
using UnityEngine;

namespace GameStates
{
    public class MenuState : GameState
    {
        public MenuState(StateMachine stateMachine) : base(stateMachine) {}

        public override void Start()
        {
            Debug.Log("START() :: MENU STATE");
        }
        public override void Update()
        {
            if (InputManager.KeyDown("select")) 
            {
                Debug.Log(InputManager.GetKey("select"));
            }
   
            Game.SetState(new PlayState(Game));
        }

        public override void End()
        {
            Debug.Log("END()   :: MENU STATE");
        }
    }
}
