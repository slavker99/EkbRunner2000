using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public static class DataSaver
{
    public static string ListToString(List<string> list)
    {
        var str = "";
        foreach (var mod in list)
            str = str + ";" + mod;
        return str;
    }

    public static List<string> StringToList(string str)
    {
        List<string> list = new List<string>();
        list = str.Split(';').ToList();
        return list;
    }

    public static void SaveScores(int scores)
    {
        PlayerPrefs.SetInt("Scores", scores);
        PlayerPrefs.Save();
        Debug.Log($"Scores {scores} сохранены в Prefs");
    }

    public static int LoadScores()
    {
        return PlayerPrefs.GetInt("Scores");
    }

    public static void SaveMods(List<string> purchasedMods, List<string> installedMods)
    {
        PlayerPrefs.SetString("PurchasedMods", ListToString(purchasedMods));
        PlayerPrefs.SetString("InstalledMods", ListToString(installedMods));
        PlayerPrefs.Save();
        Debug.Log($"Mods сохранены в Prefs");
    }

    public static void LoadMods(ref List<string> purchasedMods, ref List<string> installedMods)
    {
        purchasedMods = StringToList(PlayerPrefs.GetString("PurchasedMods"));
        installedMods = StringToList(PlayerPrefs.GetString("InstalledMods"));
        Debug.Log($"Загружен purchasedMods {PlayerPrefs.GetString("PurchasedMods")}");

    }

    // СОХРАНЕНИЯ ДЛЯ ЯНДЕКС ИГР

    public static void SaveDataInCloud(int allScores, List<string> purchasedMods, List<string> installedMods)
    {
        if (YandexGame.SDKEnabled)
        {
            YandexGame.savesData.scores = allScores;
            YandexGame.savesData.purchaseMods = purchasedMods;
            YandexGame.savesData.mainMods = installedMods;
            YandexGame.SaveProgress();
            Debug.Log("Данные сохранены в облако");
        }
    }
    public static void LoadDataFromCloud()
    {
        if (YandexGame.SDKEnabled)
        {
            YandexGame.LoadProgress();
            var allScores = YandexGame.savesData.scores;
            var purchasedMods = YandexGame.savesData.purchaseMods;
            var installedMods = YandexGame.savesData.mainMods;
        }
    }


}
