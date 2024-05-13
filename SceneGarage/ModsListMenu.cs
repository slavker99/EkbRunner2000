using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModsListMenu : MonoBehaviour
{
    [SerializeField] private bool debugMode;
    [SerializeField] private Text headText;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<Button> arrowButtons;
    [SerializeField] private List<Text> buttonsText;
    public UnityEvent<string> ButtonEvent;
    [SerializeField] private List<TuningObject> modObjects;

    private int startValue = 0;
    private int endValue = 8;

    private void Awake()
    {
        foreach (var btn in buttons)
            btn.gameObject.SetActive(false);
        //headText.text = "";
        foreach (var btn in arrowButtons)
            btn.interactable = false;
    }

    public void SetHeadText(string text)
    {
        headText.text = text;
    }

    public void UpArrow()
    {
        if (startValue > 0)
        {
            startValue--;
            endValue--;
            SetContent();
        }
    }

    public void DownArrow()
    {
        if (endValue < modObjects.Count)
        {
            startValue++;
            endValue++;
            SetContent();
        }
    }

    public void SetContent(List<TuningObject> objects = null)
    {
        if (objects != null)
        {
            //headText.text = objects.GetModName();
            startValue = 0;
            endValue = 8;
            modObjects = objects;
            if (objects.Count > 7)
                foreach (var btn in arrowButtons)
                    btn.interactable = true;
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            if ((startValue + i) < modObjects.Count)
            {
                buttons[i].gameObject.SetActive(true);
                if (debugMode) Debug.Log("Кнопке " + buttons[i].name + " назначен номер мода " + (startValue + i));
                buttonsText[i].text = modObjects[startValue + i].GetModName();
            }
            else
                buttons[i].gameObject.SetActive(false);
        }
    }


    public void ButtonListClick(int i)
    {
        if (debugMode) Debug.Log("Кнопка мода с id " + modObjects[i + startValue].GetId() + " нажата");
        if ((i + startValue) < modObjects.Count)
            ButtonEvent.Invoke(modObjects[i + startValue].GetId());
           // GarageManagementScr.SetMod(Mods[i + startValue]);
    }

}
