using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private RectTransform strelka;
    [SerializeField] private PlayerController playerController;

    private void Update()
    {
        if (playerController)
            SetSpeed(playerController.PlayerModel.GetCurrentSpeed());

    }

    public void SetSpeed(float speed)
    {
        strelka.rotation = Quaternion.Euler(0, 0, -speed * 1.5f);
    }
}
