using Data;
using GameStates;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

public class EndState : GameState
{
    public EndState(StateMachine stateMachine) : base(stateMachine) { }

    private EndObjects EndObjects => Game.endObjects;

    public override void Start()
    {
        Game.GameData.endScreenObjects.SetActive(true);

        EndObjects.title.SetText(TitleText());
        EndObjects.mainMenuButton.onClick.AddListener(OnMenu);
        EndObjects.quitButton.onClick.AddListener(OnQuit);
    }

    public override void Update() { }

    public override void End()
    {
        EndObjects.endParent.SetActive(false);
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

    private string TitleText()
    {
        var lives = 0;
        lives = Game.GameData.lives;
        return lives <= 0 ? "GAME OVER" : "YOU WIN";
    }
}