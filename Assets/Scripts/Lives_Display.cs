using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives_Display : MonoBehaviour
{
    public Text lives_text;

    private Player_Controller player_controller;

    void Start()
    {
        player_controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {

        lives_text.text = "Lives: " + player_controller.GetLives();
    }
}
