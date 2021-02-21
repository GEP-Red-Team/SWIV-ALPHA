using System;
using System.Collections;
using System.Collections.Generic;
using CustomInput;
using Data;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GameStates
{
    public class MenuState : GameState
    {
        public MenuState(StateMachine stateMachine) : base(stateMachine) {}

        private List<GameObject> _sprites = new List<GameObject>();
        private bool _shipSelectScreen = false;

        private MenuObjects MenuObjects => Game.menuObjects;

        private bool _listeningForInput = false;

        public override void Start()
        {
            Game.GameData.mainMenuObjects.SetActive(true);
            
            MenuObjects.playButton.onClick.AddListener(OnPlay);
            MenuObjects.levelSelectButton.onClick.AddListener(OnLevelSelectScreen);
            MenuObjects.shipSelectButton.onClick.AddListener(OnShipSelectScreen);
            MenuObjects.controlsButton.onClick.AddListener(OnControlScreen);
            MenuObjects.quitButton.onClick.AddListener(OnQuit);
            
            MenuObjects.ship1B.onClick.AddListener(() => OnShipSelected(1));
            MenuObjects.ship2B.onClick.AddListener(() => OnShipSelected(2));
            MenuObjects.ship3B.onClick.AddListener(() => OnShipSelected(3));
            MenuObjects.ship4B.onClick.AddListener(() => OnShipSelected(4));
            
            MenuObjects.back1.onClick.AddListener(OnBack);
            MenuObjects.back2.onClick.AddListener(OnBack);
            MenuObjects.back3.onClick.AddListener(OnBack);
            
            //Controls Buttons
            MenuObjects.p1Up.onClick.AddListener(   () => OnSetKey(1, "up", MenuObjects.p1Up));
            MenuObjects.p1Down.onClick.AddListener( () => OnSetKey(1, "down", MenuObjects.p1Down));
            MenuObjects.p1Left.onClick.AddListener( () => OnSetKey(1, "left", MenuObjects.p1Left));
            MenuObjects.p1Right.onClick.AddListener(() => OnSetKey(1, "right", MenuObjects.p1Right));
            MenuObjects.p1Shoot.onClick.AddListener(() => OnSetKey(1, "shoot", MenuObjects.p1Shoot));
            
            MenuObjects.p2Up.onClick.AddListener(   () => OnSetKey(2, "up", MenuObjects.p2Up));
            MenuObjects.p2Down.onClick.AddListener( () => OnSetKey(2, "down", MenuObjects.p2Down));
            MenuObjects.p2Left.onClick.AddListener( () => OnSetKey(2, "left", MenuObjects.p2Left));
            MenuObjects.p2Right.onClick.AddListener(() => OnSetKey(2, "right", MenuObjects.p2Right));
            MenuObjects.p2Shoot.onClick.AddListener(() => OnSetKey(2, "shoot", MenuObjects.p2Shoot));
            
            //Add sprites to the list
            _sprites.Add(MenuObjects.ship1S);
            _sprites.Add(MenuObjects.ship2S);
            _sprites.Add(MenuObjects.ship3S);
            _sprites.Add(MenuObjects.ship4S);
        }

        public override void Update()
        {
            if (_shipSelectScreen) 
            {
                RotateSprites();
            }
        }

        public override void End()
        {
            // hide the menu
            MenuObjects.menuParent.SetActive(false);
            
            // deregister callbacks
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

            MenuObjects.p1Up.onClick.RemoveListener(   () => OnSetKey(1, "up", MenuObjects.p1Up));
            MenuObjects.p1Down.onClick.RemoveListener( () => OnSetKey(1, "down", MenuObjects.p1Down));
            MenuObjects.p1Left.onClick.RemoveListener( () => OnSetKey(1, "left", MenuObjects.p1Left));
            MenuObjects.p1Right.onClick.RemoveListener(() => OnSetKey(1, "right", MenuObjects.p1Right));
            MenuObjects.p1Shoot.onClick.RemoveListener(() => OnSetKey(1, "shoot", MenuObjects.p1Shoot));
            
            MenuObjects.p2Up.onClick.RemoveListener(   () => OnSetKey(2, "up", MenuObjects.p2Up));
            MenuObjects.p2Down.onClick.RemoveListener( () => OnSetKey(2, "down", MenuObjects.p2Down));
            MenuObjects.p2Left.onClick.RemoveListener( () => OnSetKey(2, "left", MenuObjects.p2Left));
            MenuObjects.p2Right.onClick.RemoveListener(() => OnSetKey(2, "right", MenuObjects.p2Right));
            MenuObjects.p2Shoot.onClick.RemoveListener(() => OnSetKey(2, "shoot", MenuObjects.p2Shoot));
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
            //Application.Quit();
            EditorApplication.isPlaying = false;
        }

        private void RotateSprites()
        {
            foreach (var ship in _sprites)
            {
                var rot = ship.transform.eulerAngles;
                rot.y = rot.y + 20 * Time.deltaTime;
                ship.transform.eulerAngles = rot;
            }
        }
        
        private void OnSetKey(int player, string key, Button button)
        {
            //check if already listening for input 
            if (_listeningForInput) return;
            //if not start listening
            Game.StartCoroutine(ListenForInput(player, key, button));
        }

        private IEnumerator ListenForInput(int player, string key, Button button)
        {
            _listeningForInput = true;
            
            //set text to "press key"
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            var oldText = text.text;
            text.SetText("- press key -");
            
            //wait for key press            
            while (!Input.anyKeyDown) yield return null;
            
            //check every key to see if its pressed
            KeyCode keyPressed = default; 
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                //check for the pressed key
                if (!Input.GetKey(keyCode)) continue;
                //check if key is already bound
                if (InputManager.KeyCodeIsUsed(keyCode))
                {
                    text.SetText(oldText);
                    _listeningForInput = false;
                    yield break;
                }
                //store value and exit loop
                keyPressed = keyCode;
                break;
            }

            //set the key 
            InputManager.SetKey(player, key, keyPressed);
            
            //update the text
            text.SetText(InputManager.GetKeyName(player, key));

            yield return new WaitForSeconds(.5F);
            _listeningForInput = false;
        }
    }
}
