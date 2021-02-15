using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameStates
{
    public abstract class GameState
    {
        protected GameState(StateMachine stateMachine) { Game = stateMachine; }
        protected readonly StateMachine Game = default;
        
        public bool CanPause { protected set; get; }
        
        public abstract void Start();
        public abstract void Update();
        public abstract void End();

    }
}
