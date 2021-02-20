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
            Game.GameData.mainMenuObjects.SetActive(true);
            Game.GameData.mainMenuHandler.OnPlayClickedCallback += OnPlayButtonClicked;
        }

        public override void Update()
        {
            if (InputManager.KeyDown("select")) 
            {
                Debug.Log(InputManager.GetKey("select"));
            }
        }

        public override void End()
        {
            Debug.Log("END()   :: MENU STATE");
            Game.GameData.mainMenuObjects.SetActive(false);
        }

        public void OnPlayButtonClicked()
        {
            Game.SetState(new PlayState(Game));
        }
    }
}
