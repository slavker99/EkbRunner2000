using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManagement : MonoBehaviour
{
    public static bool isStart = true;
    public static bool isGame = false;
    public static bool isGameMenu = false;

    [SerializeField] private bool DevelopMode = false;
    [SerializeField] private PlayerController playerController;
    public GameSceneUI gameUI;
    public SegmentManager SegmentManager;
    public SoundsController soundsControllerScr;
    [SerializeField] private TrafficSpawnerDynamic trafficSpawner;
    [SerializeField] private BlackBackground background;
    [SerializeField] private BlackBackground gameOverbackground;

    private double scores;

    private void Awake()
    {
        isStart = true;
    }
    private void Start()
    {
         StartLevel();
    }

    private IEnumerator StartGameCorutine() // �������� ����� ������� ������ �����
    {
        yield return new WaitForFixedUpdate();
        playerController.ToStartState();
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(background.ChangeTransparentCorutine(1, 0, true)); // ��������� ������ �����
        yield return StartCoroutine(gameUI.StartHUDCorutine()); // ������ 3 2 1
        isGame = true;
        isStart = false;
        playerController.StartControlling();
        yield return null;
    }

    private IEnumerator EndGameCorutine()
    {
        yield return StartCoroutine(background.ChangeTransparentCorutine(0, 1));
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    public void StartLevel()
    {
        isGameMenu = false;
        gameUI.CloseAllMenu();
        Time.timeScale = 1;
        SegmentManager.SpawnNextSegment();
        SegmentManager.SpawnNextSegment();
        SegmentManager.SpawnNextSegment();
        for (int i = 0; i < 5; i++)
            SegmentManager.SpawnNextSegment();
        if (!DevelopMode)
            StartCoroutine(StartGameCorutine());
    }

    public void ResumeLevel() // ����� �� �������� ����
    {
        soundsControllerScr.ResumeSounds();
        gameUI.ResumeToGame();
        isGameMenu = false;
        isGame = true;
        Time.timeScale = 1;
    }

    public void ExitOnMenu() // ����� � ������� ����
    {
        Time.timeScale = 1;
        isGameMenu = false;
        isGame = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseLevel()
    {
        // ����� � ����
        if (!isGameMenu)
        {
            soundsControllerScr.PauseSounds();
            gameUI.OpenMenu();
            isGameMenu = true;
            isGame = false;
            Time.timeScale = 0;
        }

        // ����� �� ����
        else if (isGameMenu)
        {
            ResumeLevel();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isStart)
                PauseLevel();
        }

        //if ((Player.currentLives <= 0) && (!Player.isGameOver)) //���������� ���� ���� ��������� �����
        //{
        //    Player.isGameOver = true;
        //    Player.isControlled = false;
        //    Player.changingLane = false;
        //    StartCoroutine(StartTimer(1));
        //}
    }

    public void SetScores(int scores)
    {
        gameUI.SetScores(scores);
    }

    public void GameOver()
    {
        StartCoroutine(gameOverbackground.ChangeTransparentCorutine(0, 1));
        trafficSpawner.OffTraffic();
        //Time.timeScale = 0;
        gameUI.OpenGameOverMenu(playerController.PlayerModel.GetCurrentScores());
        scores = 0;
    }

    public void RestartLevel()
    {
        isGame = false;
        isGameMenu = false;
        SceneManager.LoadScene("Game");
    }

    public void LoadSettings()
    {
        if (YandexGame.SDKEnabled)
        {
            YandexGame.LoadProgress();
            SoundsController.soundsLevel = YandexGame.savesData.soundValue;
            SoundsController.musicLevel = YandexGame.savesData.musicValue;
        }
    }

    public void SaveSettings()
    {
        if (YandexGame.SDKEnabled)
        {
            YandexGame.savesData.soundValue = SoundsController.soundsLevel;
            YandexGame.savesData.musicValue = SoundsController.musicLevel;
            YandexGame.SaveProgress();
        }
    }
}
