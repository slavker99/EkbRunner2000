using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject GameMenu;
    public GameObject HUD; //игровой интерфейс
    public SoundButton soundBtn;
    public LivesPanel livesPanel;
    [SerializeField] private ScoresPanel scoresPanel;
    [SerializeField] private VhsTimer vhsTimer;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject startInstruction;
    [SerializeField] private GameOverMenu gameOverMenu;

    public BlackBackground backgroundScr;

    public Text StartCounter;

    [Header("Смена цвета худа")]
    [SerializeField] private Material mainMaterial;
    [SerializeField] private Material defMaterial;
    [SerializeField] private Material rainMaterial;
    [SerializeField] private Material nightMaterial;
    [SerializeField] private Material bonuceMt;

    private void Awake()
    {
        CloseAllMenu();
    }

    private void Start()
    {
        NightState.NightEvent.AddListener(ChangeNightUiColor);
        RainState.RainEvent.AddListener(ChangeRainUiColor);
        mainMaterial.color = defMaterial.color;
        bonuceMt.color = new Color(mainMaterial.color.r, mainMaterial.color.g, mainMaterial.color.b, 1);
    }

    private void ChangeNightUiColor(float val)
    {
        mainMaterial.color = Color.Lerp(defMaterial.color, nightMaterial.color, val);
        bonuceMt.color = new Color(mainMaterial.color.r, mainMaterial.color.g, mainMaterial.color.b, 1);
    }

    private void ChangeRainUiColor(float val)
    {
        mainMaterial.color = Color.Lerp(defMaterial.color, rainMaterial.color, val);
        bonuceMt.color = new Color(mainMaterial.color.r, mainMaterial.color.g, mainMaterial.color.b, 1);
    }

    public IEnumerator StartHUDCorutine() // сценарий после нажатия кнопки старт
    {
        CloseAllMenu();
        startText.SetActive(true);
        startInstruction.SetActive(true);
        StartCounter.gameObject.SetActive(true);
        StartCounter.enabled = true;
        StartCounter.text = "3";
        yield return new WaitForSeconds(1f);
        StartCounter.text = "2";
        yield return new WaitForSeconds(1f);
        StartCounter.text = "1";
        yield return new WaitForSeconds(1f);
        StartCounter.enabled = false;
        startText.SetActive(false);
        startInstruction.SetActive(false);
        HUD.SetActive(true);
        vhsTimer.StartTimer();
        yield return null;
    }

    public void SetScores(int value)
    {
        scoresPanel.SetScores(value);
    }

    public void SetLives(int value)
    {
        livesPanel.SetLives(value);
    }

    public void OpenMenu()
    {
        CloseAllMenu();
        GameMenu.SetActive(true);
    }

    public void ResumeToGame()
    {
        CloseAllMenu();
        HUD.SetActive(true);
    }

    public void OpenSettings()
    {
        CloseAllMenu();
        settingsMenu.SetActive(true);
    }

    public void ApplySettings()
    {
        //soundsController.ChangeVolume(SoundVolumeSlider.value);
    }

    public void OpenGameOverMenu(int scores)
    {
        CloseAllMenu();
        gameOverMenu.SetInfo(vhsTimer.VHSTime, scores);
        gameOverMenu.gameObject.SetActive(true);
    }

    public void CloseAllMenu()
    {
        HUD.SetActive(false);
        GameMenu.SetActive(false);
        //settingsMenu.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
    }
}
