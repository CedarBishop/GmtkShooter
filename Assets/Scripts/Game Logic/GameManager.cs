using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string mainMenuSceneName;
    public string gameSceneName;

    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;

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


    public bool IsWithinMap (Vector2 pos)
    {
        if (pos.x > minSpawnX && pos.x < maxSpawnX)
        {
            if (pos.y > minSpawnY && pos.y < maxSpawnY)
            {
                return true;
            }
        }
        return false;
    }

}
