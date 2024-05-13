using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerTuningView : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    public List<ModCategory> Categories;
    private List<Modification> allMods = new List<Modification>();
    private List<Modification> recolorMods = new List<Modification>();
    public string activeModID = "";
    public ColorMod ActiveColor;

    public List<Modification> GetRecolorMods() => recolorMods;

    private void Awake()
    {
        foreach (var category in Categories)
        {
            category.Initialization();
            foreach (var mod in category.Mods)
            {
                allMods.Add(mod);
                mod.SetTuningView(this);
                if (mod.GetNeedRecolor())
                    recolorMods.Add(mod);
            }
        }
    }

    private void Start()
    {
        foreach (var mod in allMods)
        {
            if (mod.GetIsDefaultMod())
                InstallMod(mod.GetId());
        }
        LoadDataFromModel();
        SetView();
    }

    public void LoadDataFromModel()
    {
        var mainModsId = controller.PlayerModel.GetMainMods();
        var purchaseModsId = controller.PlayerModel.GetPurchaseMods();
        foreach (var mod in allMods)
        {
            foreach (var modName in mainModsId)
                if (mod.GetId() == modName)
                    InstallMod(mod.GetId());

            foreach (var modName in purchaseModsId)
                if (mod.GetId() == modName)
                    mod.SetIsPurchased(true);
        }    
    }

    public void SaveDataToModel()
    {
        var mainMods = from mod in allMods
                       where mod.isMain
                       select mod.GetId();
        var purchasedMods = from mod in allMods
                            where mod.isPurchased
                            select mod.GetId();
        controller.PlayerModel.SaveMods(mainMods.ToList(), purchasedMods.ToList());
    }

    public void SetView()
    {
        foreach (var mod in allMods)
        {
            if (mod.isMain)
                InstallMod(mod.GetId());
            else
                mod.SetVisible(false);
        }
    }

    public void ChooseMod(string modID) // мод появляется на машине, но не приобретается
    {
        var mod = GetMod(modID);
        var category = GetCategory(mod.GetCatType());
        if (category.GetUnicMod()) 
            category.UnvisibleMods();
        mod.SetVisible(true);
        activeModID = modID;
    }

    public void InstallMod(string modID) // мод приобретается за валюту
    {
        ChooseMod(modID);
        var mod = GetMod(modID);
        if (!mod.isPurchased)
            mod.SetIsPurchased(true);
        var cat = GetCategory(mod.GetCatType());
        if (cat.GetUnicMod())
            cat.ResetMainMods();
        mod.SetVisible(true);
        mod.SetIsMain(true);
    }

    public void DeinstallMod(string modID)
    {
        var mod = GetMod(modID);
        if (mod.GetIsDefaultMod())
            return;
        var category = GetCategory(mod.GetCatType());
        if (!category.GetUnicMod())
        {
            mod.SetVisible(false);
            mod.SetIsMain(false);
        }
        else
        {
            foreach (var m in category.Mods)
                if (m.GetIsDefaultMod())
                    InstallMod(m.GetId());
        }
    }

    public Modification GetMod(string modID) 
    { 
        foreach (var mod in allMods)
            if (mod.GetId() == modID)
                return mod;
        Debug.Log("Не найдена модификация: " + modID);
        return null;
    }

    public ModCategory GetCategory(string catId)
    {
        foreach (var cat in Categories)
            if (cat.GetId() == catId)
                return cat;
        Debug.Log("Не найдена модификация: " + catId);
        return null;
    }

    private ModCategory GetCategory(CategoryType type)
    {
        foreach (var cat in Categories)
            if (cat.GetCatType() == type)
                return cat;
        Debug.Log("Не найдена категория: "+type.ToString());
        return null;
    }
}
