using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Manager<GameManager>
{
    //check for every system that is apparent
    //UI management
    //create local games
    //load to lobby
    //start a game when there are over 2 players connected
    //manage game rounds
    //manage player points

    public GameObject[] SystemPrefabs;
    List<GameObject> _instancedSystemPrefabs;
    List<AsyncOperation> _loadOperations;
    string _currentLevelName = string.Empty;
    
    private void Start()
    {
        _instancedSystemPrefabs = new List<GameObject>();
        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        //UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuAnimationComplete);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
        }

        Debug.Log("Load Complete.");
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        _currentLevelName = levelName;
    }

    protected void OnDestroy()
    {
        if (_instancedSystemPrefabs == null)
            return;
        
        for (int i = 0; i < _instancedSystemPrefabs.Count; ++i)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }
    
    public void StartGame()
    {
        LoadLevel("Lobby");
    }
    
    public void TogglePause()
    {
        //toggle pause / no slowing down, just the ui prompt for quiting
    }
}
