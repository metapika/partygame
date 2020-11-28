using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

namespace UnityCore {

    namespace Menu {

        public class GameManager : Manager<GameManager>
        {
            public enum GameState
            {
                MENU,
                PREGAME,
                RUNNING,
                ENDSCREEN
            }

            //create local games
            //load to lobby
            //start a game when there are over 2 players connected
            //manage game rounds
            //manage player points
            #region Gamemanager Fields
            
            public GameObject[] SystemPrefabs;
            public Events.EventGameState OnGameStateChanged;

            [SerializeField] private CinemachineVirtualCamera mainCinemachine;

            List<GameObject> _instancedSystemPrefabs;
            List<AsyncOperation> _loadOperations;
            GameState _currentGameState = GameState.PREGAME;

            string _currentLevelName = string.Empty;

            public GameState CurrentGameState
            {
                get { return _currentGameState; }
                private set { _currentGameState = value; }
            }

            #endregion

            private void Start()
            {
                _instancedSystemPrefabs = new List<GameObject>();
                _loadOperations = new List<AsyncOperation>();

                mainCinemachine = GameObject.Find("CinemachineCamera");
            }

            private void Update()
            {
                if(_currentGameState == GameState.PREGAME)
                {
                    //mainCinemachine.GetComponent<CinemachineVirtualCamera>().m_Follow = ;
                    mainCinemachine.transform.position = Vector3.zero;
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

            void UpdateState(GameState state)
            {
                GameState previousGameState = _currentGameState;
                _currentGameState = state;

                OnGameStateChanged.Invoke(_currentGameState, previousGameState);
            }

            void GameSettings()
            {
                if (_currentGameState != GameState.PREGAME)
                    return;
                //get game settings depending on sliders / buttons - uh uh dont care stay mad
            }
        }
    }
}
