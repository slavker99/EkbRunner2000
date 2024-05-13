using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// Класс PlayerController
/// Основной класс игрока, из него происходит контроль всех действий и состояний
/// Управление игроком осуществляется с помощью изменения трёх основных классов:
/// - PlayerView - отвечает за внешний вид игрока (модель, анимации, звук)
/// - PlayerModel - содержит основные параметры игрока
/// - PlayerMovement - отвечает за способ перемещения, перестроения, разгон/торможение и т.д.
/// Также содержит вспомогательный класс ChangingLaneQueue
/// Он помогает обрабатывать несколько последовательных перестроение подряд
/// </summary>

public class PlayerController : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerModel PlayerModel;
    public PlayerView PlayerView;
    [SerializeField] private GameInputController inputController;
    [SerializeField] private GameManagement gameManagement;
    [SerializeField] private Transform startPoint;
    [SerializeField] private ChangingLaneQueue changingLaneQueue;

    public bool staticMode = false;
    public bool isControlled { get; protected set; }
    public bool isMoving { get; protected set; }
    public bool isCrash { get; protected set; }
    public bool isGameOver { get; protected set; }
    public Direction isChangingLane = Direction.None;

    private void Awake()
    {
        changingLaneQueue.Initialization(PlayerMovement, this);
        if (!staticMode)
            isMoving = true;
        else
            isMoving = false;
    }

    private void Start()
    {
        if (inputController)
        {
            inputController.UpMovEvent.AddListener(MoveUp);
            inputController.DownMovEvent.AddListener(MoveDown);
            inputController.LeftMovEvent.AddListener(MoveLeft);
            inputController.RightMovEvent.AddListener(MoveRight);
        }
    }

    private void Update()
    {
        PlayerModel.SetСurrentSpeed(PlayerMovement.GetCurrentSpeed());
    }

    public void MoveUp(float value)
    {
        if (isControlled)
        {
            PlayerMovement.MoveUp(value);
            PlayerView.SetBodyXRotation(value);
        }
    }

    public void MoveDown(float value)
    {
        if (isControlled)
        {
            PlayerMovement.MoveDown(value);
            PlayerView.SetBodyXRotation(value * 1.7f);
        }
    }

    public void MoveLeft()
    {
        if (isControlled)
            changingLaneQueue.AddAction(Direction.Left);
    }

    public void MoveRight()
    {
        if (isControlled)
            changingLaneQueue.AddAction(Direction.Right);
    }

    public void ToStartState()
    {
        if (!staticMode)
        {
            isMoving = true;
            PlayerModel.SetStartValues();
            PlayerMovement.ToStartState(startPoint);
        }
    }

    public void StartControlling()
    {
        isControlled = true;
    }

    public void CrashState()
    {
        PlayerModel.TakeAwayLive();
        PlayerMovement.CrashState();
        changingLaneQueue.ClearQueue();
        if (PlayerModel.currentLives == 0)
        {
            GameOverState();
            return;
        }
        StartCoroutine(CrashStateCoruntine());
        PlayerView.CrashState(isChangingLane);
    }

    public IEnumerator CrashStateCoruntine()
    {
        isControlled = false;
        isCrash = true;
        yield return new WaitForSeconds(2);
        isControlled = true;
        isCrash = false;
        yield return null;
    }

    public void GetBonuceState()
    {
        PlayerModel.AddСurrentScroes(10);
        gameManagement.SetScores(PlayerModel.GetCurrentScores());
        PlayerView.BonuceState();
    }

    public void GameOverState()
    {
        gameManagement.GameOver();
        SetSpeed(0);
        isControlled = false;
        isMoving = false;
        PlayerModel.SaveGameValues();
        PlayerModel.SaveDataInPrefs();
        PlayerView.GameOverState();
        //PlayerMovement.GameOverState();

    }

    public void SetSpeed(float speed)
    {
        PlayerMovement.SetSpeed(speed);
    }
}
