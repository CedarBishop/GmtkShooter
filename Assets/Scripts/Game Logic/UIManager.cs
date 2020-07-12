using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState {MainMenu, Tutorial, Game, Pause, EndMatch }

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public GameObject mainMenuUIParent;
    public GameObject tutorialUIParent;
    public GameObject gameUIParent;
    public GameObject pauseUIParent;
    public GameObject endMatchUIParent;
    public GameObject soundSettingParent;

    public LayoutGroup buffTray;
    public LayoutGroup nerfTray;

    public UIState startingUIState = UIState.Game;
    private UIState uiState;

    public Text roundText;
    public Text scoreText;
    public Image healthImage;
    public Text ammoText;
    public Text timerText;

    public Text gameOverScoreText;
    public Text gameOverTimeText;

    public Slider musicSlider;
    public Slider sfxSlider;

    public BuffBadgeManager buffBadge;

    private float gameTime;
    private int gameScore;

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
       // musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
       // sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1.0f);
    }

    public void SetUIState (UIState state)
    {
        uiState = state;

        mainMenuUIParent.SetActive(false);
        tutorialUIParent.SetActive(false);
        gameUIParent.SetActive(false);
        pauseUIParent.SetActive(false);
        endMatchUIParent.SetActive(false);
        soundSettingParent.SetActive(false);

        switch (uiState)
        {
            case UIState.MainMenu:
                mainMenuUIParent.SetActive(true);
                soundSettingParent.SetActive(true);
                Time.timeScale = 1.0f;
                break;
            case UIState.Tutorial:
                tutorialUIParent.SetActive(true);
                Time.timeScale = 1.0f;
                break;
            case UIState.Game:
                gameUIParent.SetActive(true);
                Time.timeScale = 1.0f;
                break;
            case UIState.Pause:
                pauseUIParent.SetActive(true);
                soundSettingParent.SetActive(true);
                Time.timeScale = 0.0f;
                break;
            case UIState.EndMatch:
                endMatchUIParent.SetActive(true);
                UpdateGameOverScore();
                UpdateGameOverTime();
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }
    }

    public void Pause ()
    {
        SetUIState(UIState.Pause);
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_ButtonClick");
    }

    public void Resume ()
    {
        SetUIState(UIState.Game);
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_ButtonClick");
    }

    public void StartGame ()
    {
        GameManager.instance.StartGame();
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_StartButton");
        SetUIState(UIState.Game);
        BuffBadgeManager[] buffBadgeManagers = FindObjectsOfType<BuffBadgeManager>();
        if (buffBadgeManagers != null)
        {
            foreach (var item in buffBadgeManagers)
            {
                Destroy(item.gameObject);
            }
        }
    }

    public void MainMenu ()
    {
        GameManager.instance.GoToMainMenu();
        SetUIState(UIState.MainMenu);
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_ButtonClick");
    }

    public void ReturnToMainMenu ()
    {
        SetUIState(UIState.MainMenu);
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_ButtonClick");
    }

    public void GoToTutorial ()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_ButtonClick");
        SetUIState(UIState.Tutorial);
    }

    public void UpdateMusicVolume ()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.SetMusicVolume(musicSlider.value);
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_ButtonClick");
    }

    public void UpdateSFXVolume ()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.SetSFXVolume(sfxSlider.value);
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_ButtonClick");
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
        print(num);
        healthImage.fillAmount = num / 5f;
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
        timerText.text = "Time " + (time / 60 - 0.5f).ToString("0") + ":" + (time % 60 - 0.5f).ToString("00");
        gameTime = time;
    }

    public void UpdateGameOverScore()
    {
        gameOverScoreText.text = "Score\n" + gameScore.ToString("0");
    }

    public void UpdateGameOverTime()
    {
        gameOverTimeText.text = "Time\n" + (gameTime / 60 - 0.5f).ToString("0") + ":" + (gameTime % 60 - 0.5f).ToString("00");
    }
}
