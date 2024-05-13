using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoresText;

    public void SetInfo(DateTime time, int scores)
    {
        timeText.text = time.TimeOfDay.ToString();
        scoresText.text = scores.ToString();
    }
}
