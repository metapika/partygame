using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Camera _dummyCamera;
    
    #region Tabs
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject GameModes;
    #endregion

    private void Start()
    {
        //_mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }
    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        //visual representation
    }
    private void Update()
    {
        //start game
    }
    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }
    
    #region Button functions
    public void GameModesTab()
    {
        //Go to game modes tab
        MainMenu.SetActive(false);
        GameModes.SetActive(true);
    }
    public void OptionsTab()
    {
        //Go to options tab
        Options.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void CharacterCustomization()
    {
        //Go to character customizations tab
    }
    public void QuitGame()
    {
        // implement features for quitting
        Application.Quit();
    }
    public void OptionsBackButton()
    {
        //fuck go back
        Options.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void GameModesBackButton()
    {
        //fuck go back
        GameModes.SetActive(false);
        MainMenu.SetActive(true);
    }
    #endregion
}
