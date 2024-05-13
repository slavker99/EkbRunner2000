using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCollision : MonoBehaviour
{
    [SerializeField] private BotMovement bot;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private BonuceView bonuceView;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            StartCoroutine(bonuceView.QuitCorutine());
            //bot.DestroySelf();
        }
    }
}
