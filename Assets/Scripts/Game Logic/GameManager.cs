using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string mainMenuSceneName;
    public string gameSceneName;

    public WaveSystem waveSystem;

    public LayerMask obstacleLayer;
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
        waveSystem.StartGame();
    }

    public void GameOver()
    {
        UIManager.instance.SetUIState(UIState.EndMatch);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }


    public bool CanSpawnHere (Vector2 pos)
    {
        if (pos.x > minSpawnX && pos.x < maxSpawnX)
        {
            if (pos.y > minSpawnY && pos.y < maxSpawnY)
            {
                if (Physics2D.OverlapCircle(pos, 0.2f, obstacleLayer))
                {
                    // dont spawn here as it is in obstacle
                }
                else
                {
                    return true;
                }
                
            }
        }
        return false;
    }

    public Vector2 GetClearLocationOnMap(float minX, float maxX, float minY, float maxY)
    {
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        } while (CanSpawnHere(pos) == false);
        return pos;
    }

}
