using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    public class MenuObjects : MonoBehaviour
    {
        [Header("Parent Objects")]
        public GameObject menuParent = default;
        public GameObject mainMenu = default;
        public GameObject levelSelect = default;
        public GameObject shipSelect = default;
        public GameObject controls = default;

        [Header("Menu Buttons")] 
        public Button playButton = default;
        public Button levelSelectButton = default;
        public Button shipSelectButton = default;
        public Button controlsButton = default;
        public Button quitButton = default;

        [Header("Back Buttons")] 
        public Button back1 = default;
        public Button back2 = default;
        public Button back3 = default;
        
        [Header("Ship Buttons")]
        public Button ship1B = default;
        public Button ship2B = default;
        public Button ship3B = default;
        public Button ship4B = default;
        
        [Header("Ship Sprites")]
        public GameObject ship1S = default;
        public GameObject ship2S = default;
        public GameObject ship3S = default;
        public GameObject ship4S = default;
        
    }
}
