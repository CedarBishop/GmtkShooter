using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string mainMenuSceneName;
    public string gameSceneName;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void GameOver()
    {
        UIManager.instance.SetUIState(UIState.EndMatch);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }


}
