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

    public LayoutGroup buffTray;
    public LayoutGroup nerfTray;

    public UIState startingUIState = UIState.Game;
    private UIState uiState;

    public Text roundText;
    public Text scoreText;
    public Text healthText;
    public Text ammoText;
    public Text timerText;

    public BuffBadgeManager buffBadge;

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
        BuffBadgeManager badge = InstantiateBadge(buffTray);

        /// Set timer
        badge.duration = time;

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
        BuffBadgeManager badge = InstantiateBadge(nerfTray);

        /// Set timer
        badge.duration = time;

        /// Set Colour
        badge.transform.GetChild(0).GetComponent<Image>().color = c;
        /// Set Background Colour
        Color.RGBToHSV(c, out float hue, out float sat, out float val);
        badge.GetComponent<Image>().color = Color.HSVToRGB(hue - 0.05f, sat + 0.3f, val);

        /// Set Sprite
        badge.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = s;

    }

    private BuffBadgeManager InstantiateBadge(LayoutGroup group)
    {
        //Vector2 spawnPos = new Vector3(Random.Range(0, Screen.width), 100, 0);
        BuffBadgeManager badge = Instantiate(buffBadge, group.transform);
        //temp.GetComponent<RectTransform>().position = spawnPos;
        return badge;
    }

    public void UpdateHealth (int num)
    {
        healthText.text = "Health: " + num;
    }

    public void UpdateWave(int num)
    {
        roundText.text = "Wave: " + num;
        if (num <= 0)
        {
            roundText.text = "";
        }
    }

    public void UpdateScore (int num)
    {
        scoreText.text = "Score: " + num;
    }

    public void UpdateAmmo (int num)
    {
        ammoText.text = "Ammo: " + num;
        if (num <= 0)
        {
            ammoText.text = "Reloading";
        }
    }

    public void UpdateTimer (float time)
    {
        timerText.text = "Time: " + time.ToString("F1");
    }
}
