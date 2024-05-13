using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeatherController : MonoBehaviour
{
    [SerializeField] private WeatherStateMachine machine;

    [SerializeField] private float speed = 1;
    [SerializeField] private float val = 0;

    [Button] private void SetNight()
    {
        machine.SetState(WeatherType.Night, val, speed);
    }

    [Button] private void SetRain()
    {
        machine.SetState(WeatherType.Rain, val, speed);
    }
}
