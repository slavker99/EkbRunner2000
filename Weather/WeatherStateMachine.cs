using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/// <summary>
///  Класс WeatherStateMachine
///  Основной класс погодной системы.
///  Содержит состояния погоды и изменяет их.
/// </summary>

public class WeatherStateMachine : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;
    [SerializeField] private bool AutoChangeWeather = false;
    [SerializeField] private bool resetWeather = true;

    [Header("Объекты погоды")]
    public WeatherView View;

    [Header("Состояния")]
    [SerializeField] private List<WeatherState> states;
    private List<WeatherState> currentStates;
    private List<WeatherType> wTypesList = new List<WeatherType> { WeatherType.Rain, WeatherType.Night };
    private Dictionary<WeatherType, WeatherState> statesDictionary = new Dictionary<WeatherType, WeatherState>();

    private void Start()
    {
        foreach (var state in states)
            statesDictionary.Add(state.weatherType, state);

        if (AutoChangeWeather)
        {
            StartCoroutine(ChangeWeatherCorutine());
        }

        if (resetWeather)
            foreach (var state in states)
                state.SetStartState();
    }

    public void SetState(WeatherType weather, float value, float speed)
    {
        statesDictionary[weather].SetState(value, speed);
    }

    private IEnumerator ChangeWeatherCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(30,50));
            WeatherType curWeather = wTypesList[Random.Range(0, wTypesList.Count)];
            if (debugMode) Debug.Log("Началась смена погоды " + curWeather.ToString() + " на значение 1");
            SetState(curWeather, 1, Random.Range(30, 60));
            while (statesDictionary[curWeather].currentValue < 1)
            {
                yield return new WaitForSeconds(1);
                if (debugMode) Debug.Log("Ожидание смены погоды");
            }
            if (debugMode) Debug.Log("Стартовал перерыв между сменами погоды: 20 сек");
            yield return new WaitForSeconds(Random.Range(20, 60));
            if (debugMode) Debug.Log("Началась смена погоды " + curWeather.ToString() + " на значение 0");
            SetState(curWeather, 0, Random.Range(30, 50));
            while (statesDictionary[curWeather].currentValue > 0)
            {
                yield return new WaitForSeconds(1);
                if (debugMode) Debug.Log("Ожидание смены погоды");
            }
            if (debugMode) Debug.Log("Стартовал перерыв между сменами погоды: 20 сек");
            yield return new WaitForSeconds(25);
        }
    }

    private IEnumerator RainCorutine()
    {
        yield return new WaitForSeconds(10);
        while (true)
        {
            yield return new WaitForSeconds(20);
            SetState(WeatherType.Rain, 0.8f, 10);
            yield return new WaitForSeconds(40);
            SetState(WeatherType.Rain, 0, 10);
            yield return new WaitForSeconds(40);
        }
    }

    private IEnumerator NightCorutine()
    {
        yield return new WaitForSeconds(20);
        while (true)
        {
            yield return new WaitForSeconds(20);
            SetState(WeatherType.Night, 1, 20);
            yield return new WaitForSeconds(280);
            SetState(WeatherType.Night, 0, 20);
            yield return new WaitForSeconds(80);
        }
    }

    private IEnumerator FastNight()
    {
        yield return new WaitForSeconds(5);
        SetState(WeatherType.Night, 1, 2);
    }
}

public enum WeatherType
{
    Default,
    Rain,
    Snow,
    Night
}
