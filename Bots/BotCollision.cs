using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollision : MonoBehaviour
{
    public BotMovement botMovement;

    private void OnTriggerEnter(Collider other) // боковые столкновения
    {
        if (other.tag == "PlayerBody")
        {
            botMovement.isCrash = true;
            //botMovement.SetSpeed(0);
            //botMovement.bodyTextue.material = botMovement.transparentMaterial;
        }
    }
}
