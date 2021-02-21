using Data;
using GameStates;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

public class LoseState : GameState
{
    public LoseState(StateMachine stateMachine) : base(stateMachine) { }

    private LoseObjects LoseObjects => Game.loseObjects;

    public override void Start()
    {
        Game.GameData.loseScreenObjects.SetActive(true);

        LoseObjects.mainMenuButton.onClick.AddListener(OnMenu);
        LoseObjects.quitButton.onClick.AddListener(OnQuit);
    }

    public override void Update() { }

    public override void End()
    {
        LoseObjects.loseParent.SetActive(false);
    }


    private void OnMenu()
    {
        Game.SetState(new MenuState(Game));
    }

    private void OnQuit()
    {
        EditorApplication.isPlaying = false; // used just for editor
        //Application.Quit(); // would be use in actual game
    }
}