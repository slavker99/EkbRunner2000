using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopTrigger : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    [SerializeField] private float coef;
    [SerializeField] private bool isFullStop = false;
    //public float speed;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BotBody")
        {
            if (isFullStop)
                controller.SetSpeed(controller.PlayerModel.GetDefaultSpeed());
            else if (controller.PlayerModel.GetCurrentSpeed() > controller.PlayerModel.GetDefaultSpeed() + 5)
            {
                controller.SetSpeed(controller.PlayerModel.GetCurrentSpeed() - controller.PlayerModel.GetCurrentSpeed() / coef);
            }
        }
    }
}
