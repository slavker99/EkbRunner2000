using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterAnimation : MonoBehaviour
{
    public Transform body;

    private void Update()
    {
        if (body != null)
            body.rotation = Quaternion.Euler(0, body.rotation.eulerAngles.y + 1f, 0);
    }
}
