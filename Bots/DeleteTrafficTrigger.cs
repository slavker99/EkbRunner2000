using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTrafficTrigger : MonoBehaviour
{
    public Transform Player;

    private void Update()
    {
        transform.rotation = Player.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "BotBody") || (other.tag == "ScoreBooster"))
        {
            Destroy(other.transform.parent.parent.gameObject);
        }

        if (other.tag == "PosPreset")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

}
