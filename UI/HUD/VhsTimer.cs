using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VhsTimer : MonoBehaviour
{
    public DateTime VHSTime = new DateTime();
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject redPoint; 

    public void StartTimer()
    {
        StopAllCoroutines();
        StartCoroutine(TimeCounter());
    }

    private void OnEnable()
    {
        StartTimer();
    }

    private IEnumerator TimeCounter()
    {
        while (true) 
        {
            VHSTime = VHSTime.AddSeconds(1);
            timeText.text = VHSTime.TimeOfDay.ToString();
            redPoint.SetActive(!redPoint.activeSelf);
            yield return new WaitForSeconds(1);
        }
    }
}
