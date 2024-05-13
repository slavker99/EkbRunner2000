using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInputController : MonoBehaviour
{
    public UnityEvent<float> UpMovEvent = new UnityEvent<float>();
    public UnityEvent<float> DownMovEvent = new UnityEvent<float>();
    public UnityEvent LeftMovEvent = new UnityEvent();
    public UnityEvent RightMovEvent = new UnityEvent();

    private void Update()
    {
        InputControlling();
    }

    private void InputControlling()
    {
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
            LeftMovEvent.Invoke();
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
            RightMovEvent.Invoke();
        var vertAxisVal = Input.GetAxis("Vertical");
        if (vertAxisVal > 0)
            UpMovEvent.Invoke(vertAxisVal);
        if (vertAxisVal < 0)
            DownMovEvent.Invoke(vertAxisVal);

    }
}
