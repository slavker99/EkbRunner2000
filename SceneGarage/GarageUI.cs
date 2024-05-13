using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GarageUI : MonoBehaviour
{
    public ModsListMenu ModsMenu;
    public ModsListMenu CatMenu;

    public Button BuyBtn;
    public Text BuyBtnText;
    public Button InstallBtn;
    public Text InstallBtnText;
    public Button ExitBtn;
    public BlackBackground backgroundScr;

    public Text ScoresText;
    public Text PriceText;
    public Text PlayerNameText;

    public void Initialization()
    {
        ResetModInfo();
        if (YandexGame.playerName == "unauthorized")
            PlayerNameText.text = "����������������";
        else
            PlayerNameText.text = "�����: " + YandexGame.playerName;

    }

    public void UpdateData(int playerScores)
    {
        ScoresText.text = "����: " + playerScores.ToString();
    }

    public void ResetModInfo()
    {
        PriceText.text = "";
        BuyBtn.enabled = false;
        InstallBtn.enabled = false;
    }

    public void SetModInfo(Modification mod, int playerScores)
    {
        PriceText.text = "����: " + mod.GetPrice().ToString();
        if (mod.isMain)
        {
            //InstallBtn.enabled = false;
            PriceText.text = "�����������";
            InstallBtnText.text = "�����";
        }
        else
        {
            InstallBtn.enabled = true;
            InstallBtnText.text = "����������";
        }

        if (mod.isPurchased)
        {
            BuyBtn.enabled = false;
            BuyBtnText.text = "�����������";
            InstallBtn.enabled = true;
        }
        else
        {
            BuyBtn.enabled = true;
            BuyBtnText.text = "������";
            InstallBtn.enabled = false;
        }

        if (playerScores < mod.GetPrice())
            BuyBtn.enabled = false;

        if ((mod.GetIsDefaultMod()) && (mod.isMain))
            InstallBtn.enabled = false;

        UpdateData(playerScores);
    }

    public void SetCategories(List<TuningObject> categories)
    {
        CatMenu.SetContent(categories);

        //UpdateData();
    }

    public void SetMods(ModCategory cat)
    {
        List<TuningObject> mods = new List<TuningObject>();
        foreach (var mod in cat.Mods)
            mods.Add(mod);
        ModsMenu.SetContent(mods);
        //UpdateData();
    }


}
