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

    private float timer;
    private bool inGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
        waveSystem.StartGame();
        timer = 0.0f;
        inGame = true;
    }

    public void GameOver()
    {
        UIManager.instance.SetUIState(UIState.EndMatch);
        inGame = false;
    }

    public void GoToMainMenu()
    {
        inGame = false;
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

    private void FixedUpdate()
    {
        if (inGame)
        {
            timer += Time.fixedDeltaTime;
            UIManager.instance.UpdateTimer(timer);
        }
        
    }

}
