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

    public GameObject buffBadge;

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

    public void TogglePause ()
    {
        switch (uiState)
        {
            case UIState.MainMenu:
                break;
            case UIState.Game:
                Pause();
                break;
            case UIState.Pause:
                Resume();
                break;
            case UIState.EndMatch:
                break;
            default:
                break;
        }
    }

    public void Quit ()
    {
        Application.Quit();
    }

    public void CreateBuffBadge (Color c, Sprite s, float time)
    {
        GameObject badge = InstantiateBadge();

        /// Set timer
        badge.GetComponent<BuffBadgeManager>().duration = time;

        /// Set Colour
        badge.transform.GetChild(0).GetComponent<Image>().color = c;
        /// Set Background Colour
        Color.RGBToHSV(c, out float hue, out float sat, out float val);
        badge.GetComponent<Image>().color = Color.HSVToRGB(hue - 0.05f, sat + 0.3f, val);

        /// Set Sprite
        badge.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = s;


    }

    public void CreateNerfBadge(Color c, Sprite s, float time)
    {
        GameObject badge = InstantiateBadge();

        /// Set timer
        badge.GetComponent<BuffBadgeManager>().duration = time;

        /// Set Colour
        badge.transform.GetChild(0).GetComponent<Image>().color = c;
        /// Set Background Colour
        Color.RGBToHSV(c, out float hue, out float sat, out float val);
        badge.GetComponent<Image>().color = Color.HSVToRGB(hue - 0.05f, sat + 0.3f, val);

        /// Set Sprite
        badge.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = s;

    }

    private GameObject InstantiateBadge()
    {
        Vector2 spawnPos = new Vector3(Random.Range(0, Screen.width), 100, 0);
        GameObject temp = Instantiate(buffBadge, transform);
        temp.GetComponent<RectTransform>().position = spawnPos;
        return temp;
    }
}
