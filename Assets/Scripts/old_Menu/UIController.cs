using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region MainMenu Fields
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button CustomizationButton;
    [SerializeField] private Button QuitButton;
    #endregion

    #region OptionsMenu Fields
    [SerializeField] private Slider MasterVolume;
    [SerializeField] private Slider MusicVolume;
    [SerializeField] private Button PMBackButton;
    #endregion

    #region GameModesMenu Fields
    [SerializeField] private Button GMBackButton;
    #endregion

    private void Start()
    {
        //OptionsTab fields
        PMBackButton.onClick.AddListener(HandleBackClickedPM);

        //GameModesTab fields
        GMBackButton.onClick.AddListener(HandleBackClickedGM);

        //MainMenu fields
        PlayButton.onClick.AddListener(HandlePlayClicked);
        OptionsButton.onClick.AddListener(HandleOptionsClicked);
        CustomizationButton.onClick.AddListener(HandleCustomizationClicked);
        QuitButton.onClick.AddListener(HandleQuitClicked);
    }
    
    #region BackButton Functions
    void HandleBackClickedPM()
    {
        //Switch to game modes tab
        UIManager.Instance.OptionsBackButton();
    }
    void HandleBackClickedGM()
    {
        UIManager.Instance.GameModesBackButton();
    }
    #endregion

    #region MainMenu Functions
    void HandlePlayClicked()
    {
        //Switch to game modes tab
        UIManager.Instance.GameModesTab();
    }
    void HandleOptionsClicked()
    {
        //Switch to options tab
        UIManager.Instance.OptionsTab();
    }
    void HandleCustomizationClicked()
    {
        //Switch to character customization tab
        UIManager.Instance.CharacterCustomization();
    }
    void HandleQuitClicked()
    {
        //Switch to quit tab
        UIManager.Instance.QuitGame();
    }
    #endregion
}
