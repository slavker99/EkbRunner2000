using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс PlayerBodyCollision
/// Реагирует на столкновения/подбор очков и оповещает PlayerController
/// </summary>

public class PlayerBodyCollision : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "BotBody") || (other.tag == "Border"))
        {
            controller.CrashState();
        }

        if (other.tag == "ScoreBooster")
        {
            controller.GetBonuceState();
        }
    }
}
