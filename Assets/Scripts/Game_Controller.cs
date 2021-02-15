using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public float player_reset_duration = 3f;
    public GameObject shield_powerup_prefab = null;

    private GameObject player;
    private bool player_dead = false;
    private float reset_time = 0f;
    private int player_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player_dead)
        {
            if((Time.time - reset_time) >= player_reset_duration)
            {
                ResetPlayer();
            }
        }
    }

    public void OnPlayerDead()
    {
        player_dead = true;
        reset_time = Time.time;
    }

    void ResetPlayer()
    {
        player.SetActive(true);
    }

    public void AddPlayerScore(int amount)
    {
        player_score += amount;
        // Update UI as an event here from a public text reference instead of polling every tick.
    }

    public int GetPlayerScore()
    {
        return player_score;
    }

    public void InstantiateShieldPowerup(Transform transform)
    {
        Instantiate(shield_powerup_prefab, transform.position, Quaternion.identity);
    }
}
