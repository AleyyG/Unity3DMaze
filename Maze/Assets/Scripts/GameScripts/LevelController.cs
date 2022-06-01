using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public bool gameActive = true;

    public GameObject startingMenu, gameMenu, gameOverMenu, finishMenu;
    public Text levelText, nextLevelText;
    public TMP_Text starTextFinish, starTextStart, starTextMarket;
    public Image star1, star2, star3;
    public new Light light;
    public Image transitionImage;
    int level;
    int totalStarsCount;
    // Start is called before the first frame update
    void Start()
    {
        Current = this;
        level = PlayerPrefs.GetInt("currentLevel");
        totalStarsCount = PlayerPrefs.GetInt("stars");

        levelText.text = (level + 1).ToString();
        nextLevelText.text = (level + 2).ToString();


        SetStarText();
    }

    void Update()
    {

    }
    public void SetStarText()
    {
        starTextFinish.text = " " + totalStarsCount.ToString();
        starTextStart.text = " " + totalStarsCount.ToString();
        starTextMarket.text = " " + totalStarsCount.ToString();
    }
    public void StartLevel()
    {
        SoundManager.Current.Click();
        BallMovement.Current.ChangeSpeed(BallMovement.Current.speedValue);
        startingMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameActive = true;

        if (PlayerPrefs.GetInt("currentLevel") % 5 == 4)
        {
            RenderSettings.skybox = null;
            RenderSettings.ambientIntensity = 0;
            RenderSettings.ambientSkyColor = Color.black;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            BallMovement.Current.gameObject.GetComponent<Light>().enabled = true;
            light.gameObject.SetActive(false);
        } //karanlık mod
    }

    public void RestartLevel()
    {
        SoundManager.Current.Click();
        BallMovement.Current.heartCounter = 0;
        SetStarText();
        LevelLoader.Current.ChangeLevel(SceneManager.GetActiveScene().name);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameActive = true;
    }

    public void NextLevel()
    {
        SoundManager.Current.Click();
        if (level == 29)
            SceneManager.LoadScene("Last");
        LevelLoader.Current.ChangeLevel("Level " + (level + 1));
        gameMenu.SetActive(false);
        BgMusic.Current.HighVolumeMusic();
    }
    public void FinishGame()
    {
        if (BallMovement.Current.starsCount == 1)
        {
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }
        else if (BallMovement.Current.starsCount == 2)
        {
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true);
            star3.gameObject.SetActive(false);
        }
        else if (BallMovement.Current.starsCount == 3)
        {
            star1.gameObject.SetActive(true);
            star2.gameObject.SetActive(true);
            star3.gameObject.SetActive(true);
        }
        totalStarsCount += BallMovement.Current.starsCount;
        PlayerPrefs.SetInt("stars", totalStarsCount);
        SoundManager.Current.Confetti();
        SoundManager.Current.ClapSounds();
        SetStarText();
        PlayerPrefs.SetInt("currentLevel", level + 1);
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        gameActive = false;
    }
}
