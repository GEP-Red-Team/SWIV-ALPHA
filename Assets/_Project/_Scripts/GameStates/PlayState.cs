using CustomInput;
using UnityEngine;

namespace GameStates
{
    public class PlayState : GameState
    {
        public PlayState(StateMachine stateMachine) : base(stateMachine) {}

        private const int GAME_STARTING_LIVES = 3;

        private GameObject player = null;
        private PlayerController playerController = null;

        // Reset player.
        private bool checkResetPlayer = false;
        private float beginResetTime = 0f;
        private float resetDuration = 1.5f;

        
        private bool checkPlayerWon = false;
        private const int MAX_SCORE = 500;

        public override void Start()
        {
            Debug.Log("START() :: PLAY STATE");
            CanPause = true;

            // Reset game state.
            Game.GameData.lives = GAME_STARTING_LIVES;
            Game.GameData.currentScore = 0;

            // Show game state object.
            Game.GameData.playstateObjects.SetActive(true);

            // Get player game object.
            player = GameObject.FindGameObjectWithTag("Player");

            // Register player callbacks.
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.OnPlayerHitCallback += OnPlayerHit;
            playerController.OnEnemyHitCallback += OnEnemyHit;

            // Set the playstate the player belongs to.
            playerController.SetPlayState(this);
            
            UpdateLivesUI();
            UpdateScoreUI();
        }

        public override void Update()
        {
            if (checkResetPlayer)
            {
                if ((Time.time - beginResetTime) >= resetDuration)
                {
                    ResetPlayer();
                }
            }

            if (checkPlayerWon)
            {
                OnGameEnd();
            }
            
            if (InputManager.GetKeyDown(1, "pause"))
            {
                Game.OnPause();
            }
        }

        public override void End()
        {
            Debug.Log("START() :: PLAY STATE");

            // Hide game state object.
            Game.GameData.playstateObjects.SetActive(false);
        }

        public void OnPlayerHit()
        {
            Game.GameData.lives--;

            UpdateLivesUI();

            player.SetActive(false);
            BeginPlayerReset();

            if(Game.GameData.lives < 0)
            {
                OnGameEnd();
            }
        }

        public void AddPlayerScore(int amount)
        {
            Game.GameData.currentScore += amount;
            UpdateScoreUI();
        }

        private void UpdateLivesUI()
        {
            Game.GameData.livesText.text = "Lives " + Game.GameData.lives.ToString();
        }

        private void UpdateScoreUI()
        {
            Game.GameData.scoreText.text = "Score " + Game.GameData.currentScore.ToString();
        }

        private void OnGameEnd()
        {
            Game.SetState(new EndState(Game));
            Debug.Log("GAME END");
        }

        private void BeginPlayerReset()
        {
            checkResetPlayer = true;
            beginResetTime = Time.time;
        }

        private void ResetPlayer()
        {
            player.SetActive(true);
        }

        private void OnEnemyHit()
        {
            if (Game.GameData.currentScore >= MAX_SCORE)
            {
                checkPlayerWon = true;
            }
        }
    }
}
