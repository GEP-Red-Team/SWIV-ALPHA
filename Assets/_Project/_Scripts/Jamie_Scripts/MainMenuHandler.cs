using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{

    public GameObject Sprite1 = null;
    public GameObject Sprite2 = null;
    public GameObject Sprite3 = null;
    public GameObject Sprite4 = null;
    public GameObject ShipSelect = null;
    private GameObject[] Sprites;
    public int selectedShip = 0;
    private void Start()
    {
        Sprites = new GameObject[] { Sprite1, Sprite2, Sprite3, Sprite4 };
    }
    public void OnPlayButton()
    {
        // load game
        Debug.Log("Load Game");
    }
    public void OnQuitButton()
    {
        //quit game
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void SelectShip(int ship)
    {
        selectedShip = ship;
    }
    private void Update()
    {
        if (ShipSelect.activeSelf)
        {
            rotateSprites();
        }
    }
    private void rotateSprites()
    {
        for (int i = 0; i < 4; i++)
        {
            Sprites[i].transform.eulerAngles = new Vector3(Sprites[i].transform.eulerAngles.x, (Sprites[i].transform.eulerAngles.y + 20 * Time.deltaTime), Sprites[i].transform.eulerAngles.z);
        }
    }
}
