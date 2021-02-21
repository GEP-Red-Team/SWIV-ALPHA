using CustomInput;
using UnityEngine;

namespace GameStates
{
    public class PlayState : GameState
    {
        public PlayState(StateMachine stateMachine) : base(stateMachine) {}

        private const int GAME_STARTING_LIVES = 3;

        private GameObject player = null;
        private Material[] playerMaterials = null;
        private const string playerColorUniform = "_Color";
        private PlayerController playerController = null;

        // Reset player.
        private bool checkResetPlayer = false;
        private float beginResetTime = 0f;
        private float resetDuration = 2.5f;
        private float lastFlash = 0f;
        private bool on = false;
        public float flashInterval = 0.2f;
        
        private bool checkPlayerWon = false;
        private const int MAX_SCORE = 10000;

        public override void Start()
        {
            Debug.Log("START() :: PLAY STATE");
            CanPause = true;

            // Reset game state.
            Game.GameData.lives = GAME_STARTING_LIVES;
            Game.GameData.currentScore = 0;

            // Show game state object.
            Game.GameData.playstateObjects.SetActive(true);

            // Get player game object and material.
            player = GameObject.FindGameObjectWithTag("Player");
            playerMaterials = player.GetComponent<MeshRenderer>().materials;

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
                if ((Time.time - lastFlash) >= flashInterval)
                {
                    lastFlash = Time.time;
                    if (on)
                    {
                        SetPlayerAlpha(0f);
                        on = false;
                    }
                    else
                    {
                        SetPlayerAlpha(1f);
                        on = true;
                    }
                }

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
            if(!checkResetPlayer)
            {
                Game.GameData.lives--;

                UpdateLivesUI();

                BeginPlayerReset();

                if (Game.GameData.lives < 0)
                {
                    OnGameEnd();
                }
            }
        }

        public bool IsResettingPlayer()
        {
            return checkResetPlayer;
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

            SetPlayerAlpha(0f);
            checkResetPlayer = true;
            beginResetTime = Time.time;
            lastFlash = Time.time;
        }

        private void SetPlayerAlpha(float alpha)
        {
            for (int i = 0; i < playerMaterials.Length; ++i)
            {
                Color col = playerMaterials[i].GetColor(playerColorUniform);
                playerMaterials[i].SetColor(playerColorUniform, new Color(col.r, col.g, col.b, alpha));
            }
        }

        private void ResetPlayer()
        {
            SetPlayerAlpha(1f);
            checkResetPlayer = false;
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
