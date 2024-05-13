using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GarageManagement : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GarageUI UI;
    [SerializeField] private ModsListMenu modMenu;
    [SerializeField] private ModsListMenu catMenu;
    [SerializeField] private CategoriesListMenu catIconsMenu;
    [SerializeField] private GarageCameraController cameraController;
    private PlayerTuningView playerTuningView;

    private void Awake()
    {
        playerTuningView = playerController.PlayerView.TuningView;
        UI.Initialization();
        StartCoroutine(UI.backgroundScr.ChangeTransparentCorutine(1, 0, true));
    }
    void Start()
    {
        modMenu.ButtonEvent.AddListener(SetMod);
        catMenu.ButtonEvent.AddListener(SetCategory);
        catIconsMenu.ButtonEvent.AddListener(SetCategory);
        List<TuningObject> categories = new List<TuningObject>();
        foreach (var cat in playerController.PlayerView.TuningView.Categories)
            categories.Add(cat);
        catIconsMenu.SetContent(categories);
        UI.SetCategories(categories);
        //playerController.PlayerModel.AddScores(200000);
        UI.UpdateData(playerController.PlayerModel.GetAllScores());
        //YandexGame.CloseVideoEvent += Add1000Scores;
    }

    public void SetCategory(string catId)
    {
         UI.SetMods(playerTuningView.GetCategory(catId));
         UI.ResetModInfo();
         playerTuningView.SetView();
         cameraController.ChangeCameraPos(playerTuningView.GetCategory(catId).CameraPosition, playerTuningView.GetCategory(catId).CameraTarget);
         modMenu.SetHeadText(playerTuningView.GetCategory(catId).GetModName());
    }

    public void SetMod(string modId)
    {
        playerTuningView.SetView();
        var mod = playerTuningView.GetMod(modId);
        playerTuningView.ChooseMod(mod.GetId());
        UI.SetModInfo(mod, playerController.PlayerModel.GetAllScores());
        if (mod.CameraPosition)
            cameraController.ChangeCameraPos(mod.CameraPosition, mod.CameraTarget);
    }

    public void BuyMod()
    {
        var mod = playerTuningView.GetMod(playerTuningView.activeModID);
        if (playerController.PlayerModel.GetAllScores() >= mod.GetPrice())
        {
            Debug.Log("Приобретён мод: " + mod.GetId());
            playerController.PlayerModel.AddScores(-mod.GetPrice());
            playerTuningView.InstallMod(mod.GetId());
            UI.SetModInfo(mod, playerController.PlayerModel.GetAllScores());
            playerTuningView.SaveDataToModel();
            playerController.PlayerModel.SaveDataInPrefs();
        }
    }

    public void InstallMod()
    {
        var mod = playerTuningView.GetMod(playerTuningView.activeModID);
        if (mod.isMain)
            playerTuningView.DeinstallMod(mod.GetId());
        else 
            playerTuningView.InstallMod(mod.GetId());
        UI.SetModInfo(mod, playerController.PlayerModel.GetAllScores());
        playerTuningView.SaveDataToModel();
        playerController.PlayerModel.SaveDataInPrefs();
    }


    public void ShowReklama()
    {
        YandexGame.RewVideoShow(1);
    }

    public void Add1000Scores()
    {
        playerController.PlayerModel.ChangeCurScores(1000);
        UI.UpdateData(playerController.PlayerModel.GetAllScores());
        playerController.PlayerModel.SaveDataInPrefs();
    }

    public void ExitOnMenu()
    {
        StartCoroutine(ExitCorutine());
    }

    public IEnumerator ExitCorutine()
    {
        playerTuningView.SaveDataToModel();
        playerController.PlayerModel.SaveDataInPrefs();
        yield return StartCoroutine(UI.backgroundScr.ChangeTransparentCorutine(0, 1));
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }
}
