using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState {MainMenu, Game, Pause, EndMatch }

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public GameObject mainMenuUIParent;
    public GameObject gameUIParent;
    public GameObject pauseUIParent;
    public GameObject endMatchUIParent;

    public UIState startingUIState = UIState.Game;
    private UIState uiState;

    public Text roundText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetUIState(startingUIState);
    }

    public void SetUIState (UIState state)
    {
        if (uiState == state)
        {
            return;
        }

        uiState = state;

        mainMenuUIParent.SetActive(false);
        gameUIParent.SetActive(false);
        pauseUIParent.SetActive(false);
        endMatchUIParent.SetActive(false);

        switch (uiState)
        {
            case UIState.MainMenu:
                mainMenuUIParent.SetActive(true);
                Time.timeScale = 1.0f;
                break;
            case UIState.Game:
                gameUIParent.SetActive(true);
                Time.timeScale = 1.0f;
                break;
            case UIState.Pause:
                pauseUIParent.SetActive(true);
                Time.timeScale = 0.0f;
                break;
            case UIState.EndMatch:
                endMatchUIParent.SetActive(true);
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }
    }

    public void Pause ()
    {
        SetUIState(UIState.Pause);
    }

    public void Resume ()
    {
        SetUIState(UIState.Game);
    }

    public void StartGame ()
    {
        GameManager.instance.StartGame();
        SetUIState(UIState.Game);
    }

    public void MainMenu ()
    {
        GameManager.instance.GoToMainMenu();
        SetUIState(UIState.MainMenu);
    }

    public void Quit ()
    {
        Application.Quit();
    }

    public void CreateBuffBadge (Color c, Sprite s, float time)
    {
        /// Set Colour

        /// Set Sprite

        /// Set Badge Colour

        /// Set Decayed Badge Colour

        /// Start timer

        //// Update Badge Colour Fill Slider to fade away.

    }

    public void CreateNerfBadge(Color c, Sprite s, float time)
    {
        /// Set Colour

        /// Set Sprite

        /// Set Badge Colour

        /// Set Decayed Badge Colour

        /// Start timer

        //// Update Badge Colour Fill Slider to fade away.

    }

    private void InstantiateBadge()
    {
        //GameObject Instantiate
    }
}
