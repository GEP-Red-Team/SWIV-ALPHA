using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Display : MonoBehaviour
{
    public Text score_text;
    public Game_Controller game_controller = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score: " + game_controller.GetPlayerScore();
    }
}
