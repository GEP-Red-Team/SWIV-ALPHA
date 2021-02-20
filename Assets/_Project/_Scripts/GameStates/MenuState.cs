﻿using System;
using System.Collections.Generic;
using CustomInput;
using Data;
using UnityEditor;
using UnityEngine;

namespace GameStates
{
    public class MenuState : GameState
    {
        public MenuState(StateMachine stateMachine) : base(stateMachine) {}

        private List<GameObject> _sprites = new List<GameObject>();
        private bool _shipSelectScreen = false;

        private MenuObjects MenuObjects => Game.menuObjects;

        public override void Start()
        {
<<<<<<< HEAD
            Debug.Log("START() :: MENU STATE");
            Game.GameData.mainMenuObjects.SetActive(true);
            Game.GameData.mainMenuHandler.OnPlayClickedCallback += OnPlayButtonClicked;
        }

=======
            //Enable the objects in scene
            MenuObjects.menuParent.gameObject.SetActive(true);
            
            //Register buttons to callbacks
            MenuObjects.playButton.onClick.AddListener(OnPlay);
            MenuObjects.levelSelectButton.onClick.AddListener(OnLevelSelectScreen);
            MenuObjects.shipSelectButton.onClick.AddListener(OnShipSelectScreen);
            MenuObjects.controlsButton.onClick.AddListener(OnControlScreen);
            MenuObjects.quitButton.onClick.AddListener(OnQuit);
            
            //Individual Ship Buttons
            MenuObjects.ship1B.onClick.AddListener(() => OnShipSelected(1));
            MenuObjects.ship2B.onClick.AddListener(() => OnShipSelected(2));
            MenuObjects.ship3B.onClick.AddListener(() => OnShipSelected(3));
            MenuObjects.ship4B.onClick.AddListener(() => OnShipSelected(4));
            
            //Back Buttons
            MenuObjects.back1.onClick.AddListener(OnBack);
            MenuObjects.back2.onClick.AddListener(OnBack);
            MenuObjects.back3.onClick.AddListener(OnBack);

            //Add sprites to the list
            _sprites.Add(MenuObjects.ship1S);
            _sprites.Add(MenuObjects.ship2S);
            _sprites.Add(MenuObjects.ship3S);
            _sprites.Add(MenuObjects.ship4S);
        }
        
>>>>>>> c2cef5784c5d07b0f559e6cc51aecf08d9e0f235
        public override void Update()
        {
            if (_shipSelectScreen) 
            {
                RotateSprites();
            }
        }

        public override void End()
        {
<<<<<<< HEAD
            Debug.Log("END()   :: MENU STATE");
            Game.GameData.mainMenuObjects.SetActive(false);
        }

        public void OnPlayButtonClicked()
        {
            Game.SetState(new PlayState(Game));
=======
            //Disable the objects in scene
            MenuObjects.menuParent.SetActive(false);
            
            //Deregister buttons to callbacks
            MenuObjects.playButton.onClick.RemoveListener(OnPlay);
            MenuObjects.levelSelectButton.onClick.RemoveListener(OnLevelSelectScreen);
            MenuObjects.shipSelectButton.onClick.RemoveListener(OnShipSelectScreen);
            MenuObjects.controlsButton.onClick.RemoveListener(OnControlScreen);
            MenuObjects.quitButton.onClick.RemoveListener(OnQuit);
            
            MenuObjects.ship1B.onClick.RemoveListener(() => OnShipSelected(1));
            MenuObjects.ship2B.onClick.RemoveListener(() => OnShipSelected(2));
            MenuObjects.ship3B.onClick.RemoveListener(() => OnShipSelected(3));
            MenuObjects.ship4B.onClick.RemoveListener(() => OnShipSelected(4));
            
            MenuObjects.back1.onClick.RemoveListener(OnBack);
            MenuObjects.back2.onClick.RemoveListener(OnBack);
            MenuObjects.back3.onClick.RemoveListener(OnBack);
        }

        private void OnPlay()
        {
            Game.SetState(new PlayState(Game));
            MenuObjects.menuParent.SetActive(false);
        }

        private void OnLevelSelectScreen()
        {
            MenuObjects.levelSelect.SetActive(true);
            MenuObjects.mainMenu.SetActive(false);
        }

        private void OnShipSelectScreen()
        {
            _shipSelectScreen = true;
            MenuObjects.shipSelect.SetActive(true);
            MenuObjects.mainMenu.SetActive(false);
        }

        private void OnShipSelected(int option)
        {
            Game.GameData.shipOption = option;
            MenuObjects.shipSelect.SetActive(false);
            MenuObjects.mainMenu.SetActive(true);
            _shipSelectScreen = false;
        }

        private void OnControlScreen()
        {
            MenuObjects.mainMenu.SetActive(false);
            MenuObjects.controls.SetActive(true);
        }

        private void OnBack()
        {
            MenuObjects.mainMenu.SetActive(true);
            MenuObjects.levelSelect.SetActive(false);
            MenuObjects.shipSelect.SetActive(false);
            MenuObjects.controls.SetActive(false);
        }
        
        private void OnQuit()
        {
            //NOTE : this doesnt work in editor
            Application.Quit();
        }
        
        private void RotateSprites()
        {
            foreach (var ship in _sprites)
            {
                var rot = ship.transform.eulerAngles;
                rot.y = rot.y + 20 * Time.deltaTime;
                ship.transform.eulerAngles = rot;
            }
>>>>>>> c2cef5784c5d07b0f559e6cc51aecf08d9e0f235
        }
    }
}
