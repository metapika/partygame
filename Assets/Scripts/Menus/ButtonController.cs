using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCore {

    namespace Menu {

        public class ButtonController : MonoBehaviour
        {
#region Fields
            private Button menuButton;
            public PageController pageController;
            public GameManager GameManager;
            public ButtonType ButtonType;
#endregion

#region Private Functions
            private void Start()
            {
                menuButton = GetComponent<Button>();
                GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                if (menuButton != null)
                {
                menuButton.onClick.AddListener(OnButtonClicked);

                } else
                {
                    return;
                }
            }
#endregion

#region Public Funtions
            public void OnButtonClicked()
            {
                switch (ButtonType)
                {
                    case ButtonType.Play :
                        pageController.TurnPageOff(PageType.TitleScreen);
                        pageController.TurnPageOn(PageType.GameModes);
                        break;
                    case ButtonType.MapSelection:
                        pageController.TurnPageOff(PageType.GameModes);
                        pageController.TurnPageOn(PageType.MapSelection);
                        break;
                    case ButtonType.Options :
                        pageController.TurnPageOff(PageType.TitleScreen);
                        pageController.TurnPageOn(PageType.Options);
                        break;
                    case ButtonType.Customize :
                        pageController.TurnPageOff(PageType.TitleScreen);
                        pageController.TurnPageOn(PageType.Customize);
                        break;
                    case ButtonType.Quit :
                        //Quit
                        break;
                    case ButtonType.Back :
                        pageController.ReturnToMainMenu();
                        break;

                        //Map selection
                    case ButtonType.Lobby :
                        GameManager.LoadLevel("MainMap");
                        break;
                }
            }
#endregion
        }
    }

}
