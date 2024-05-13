using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using YG;

/// <summary>
/// Класс PlayerModel
/// Cодержит основные параметры игрока
/// От сюда берет данные игровой HUD
/// </summary>

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private bool debugMode;

    [Header("Жизни")]
    [SerializeField] private int maxLives = 7;
    public int currentLives { get; private set; }
    public UnityEvent<int> ChangeLivesEvent = new UnityEvent<int>();

    [Header("Скорость")]
    private float currentSpeed = 0;
    [SerializeField] private float crashSpeed = 20;
    [SerializeField] private float defaultSpeed = 30;
    [SerializeField] private float startSpeed = 40;
    [SerializeField] private float maxSpeed = 100;
    [SerializeField] private float accelerationCoef = 0.8f;
    [SerializeField] private float turningCoef = 3f;

    [Header("Очки")]
    private int currentScores = 0;

    [Header("Данные для сохранения")]
    private static List<string> purchasedMods = new List<string> ();
    private static List<string> installedMods = new List<string> ();
    private static int allScores = 0;

    public float GetCurrentSpeed() => currentSpeed;
    public float GetCrashSpeed() => crashSpeed;
    public float GetDefaultSpeed() => defaultSpeed;
    public float GetStartSpeed() => startSpeed;
    public float GetMaxSpeed() => maxSpeed;
    public int GetAllScores() => allScores;
    public int GetCurrentScores() => currentScores;
    public float GetAccelerationCoef() => accelerationCoef;
    public float GetTurningCoef() => turningCoef;

    private void Awake()
    {
        LoadDataInPrefs();
        if (debugMode) Debug.Log("PlayerModel: Количество очков игрока - " + allScores);
    }

    public List<string> GetMainMods()
    {
        var mods = new List<string> ();
        foreach (var mod in installedMods)
            mods.Add(mod.Clone().ToString());
        return mods;
    }

    public List<string> GetPurchaseMods()
    {
        var mods = new List<string>();
        foreach (var mod in purchasedMods)
            mods.Add(mod.Clone().ToString());
        return mods;
    }

    public void SetStartValues()
    {
        SetLives(maxLives);
        currentScores = 0;
    }

    public void SetСurrentSpeed(float val)
    {
        if (val >= 0)
            currentSpeed = val;
    }

    public void AddСurrentScroes(int val)
    {
        currentScores += val;
    }

    public void SetLives(int val)
    {
        if (val >= 0)
        {
            ChangeLivesEvent.Invoke(val);
            currentLives = val;
            if (debugMode) Debug.Log("Жизни игроки изменены на: " + val);
        }
    }

    public void TakeAwayLive()
    {
        if (currentLives > 0)
            SetLives(currentLives - 1);
    }


    public void SaveGameValues()
    {
        allScores += currentScores;
        Debug.Log("Current scores: " + currentScores + " сохранены. AllScores = " + allScores);
    }

    public void SaveMods(List<string> main, List<string> purchased)
    {
        installedMods = new List<string>();
        purchasedMods = new List<string>();
        foreach (var modName in main)
            installedMods.Add(modName.Clone().ToString()); // чтобы значения не передавались по ссылке
        foreach (var modName in purchased)
            purchasedMods.Add(modName.Clone().ToString());
    }

    public void SaveDataInPrefs()
    {
        DataSaver.SaveScores(allScores);
        DataSaver.SaveMods(purchasedMods, installedMods);
        if (debugMode) Debug.Log("PurchasedMods сохранен: " + DataSaver.ListToString(purchasedMods));
        if (debugMode) Debug.Log("InstalledMods сохранен: " + DataSaver.ListToString(installedMods));
    }
    public void LoadDataInPrefs()
    {
        allScores = DataSaver.LoadScores();
        DataSaver.LoadMods(ref purchasedMods, ref installedMods);

        if (debugMode)
        {
            WriteLineLists();
            Debug.Log("Данные из Prefs загружены");
        }
    }

    [Button] public void ClearCloud()
    {
        if (YandexGame.SDKEnabled)
        {
            YandexGame.savesData.scores = 0;
            YandexGame.savesData.purchaseMods = new List<string>();
            YandexGame.savesData.mainMods = new List<string>();
            YandexGame.SaveProgress();
            Debug.Log("Облако очищено");
        }
    }

    private void WriteLineLists()
    {
        var str = "Установленные моды: ";
        foreach (var mod in installedMods)
            str = str + "; " + mod;
        Debug.Log(str);
        str = "Купленные моды: ";
        foreach (var mod in purchasedMods)
            str = str + "; " + mod;
        Debug.Log(str);
    }

    public void ChangeCurScores(int value)
    {
        currentScores += value;
    }

    public void AddScores(int value)
    {
        if (debugMode) Debug.Log("Игроку начислены очки: " + value);
        allScores += value;
        if (debugMode) Debug.Log("Количество очков игрока: " + allScores);
    }
}
