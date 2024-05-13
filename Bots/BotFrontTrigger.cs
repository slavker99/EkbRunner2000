using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFrontTrigger : MonoBehaviour
{
    // Ќужно чтобы бот останавливалс€ перед преп€тстви€ми
    public BotMovement botMovement;
    public bool isFrontTrigger;

    private void OnTriggerEnter(Collider other)
    {
        //if ((other.tag == "BotBody") || (other.tag == "PlayerBody") || (other.tag == "StopTrigger"))
        //{
        //    isFrontTrigger = true;
        //    botMovement.SetSpeed(0);
        //     //Debug.Log(other.gameObject.name);
        //}
        //if (other.tag == "StopTriggerTramway")
        //{
        //    if ((botMovement.typeTransport == TypeTransport.Tramway) || (botMovement.typeTransport == TypeTransport.Trolley))
        //    {
        //        isFrontTrigger = true;
        //        botMovement.SetSpeed(0);
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //if ((other.tag == "BotBody") || (other.tag == "PlayerBody") || (other.tag == "StopTrigger"))
        //{
        //    botMovement.isFrontTrigger = false;
        //    botMovement.SetSpeed(botMovement.defaultSpeed);
        //    //Debug.Log(other.gameObject.name + "exit");
        //}
    }
}
