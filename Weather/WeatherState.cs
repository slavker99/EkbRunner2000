using System.Collections;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
///   ласс WeatherState
///  Ѕазовый класс состо€ний дл€ погодной стейт машины.
///  —одержит метод SetState, который запускает корутину плавного изменени€ параметров погоды.
/// </summary>

public abstract class WeatherState : MonoBehaviour
{
    [ReadOnly] public WeatherType weatherType;
    [SerializeField] protected WeatherStateMachine stateMachine;
    [ReadOnly] public float currentValue = 0;

    public void SetState(float value, float speed)
    {
        StartCoroutine(ChangeStateCorutine(value, speed));
    }

    public void SetStartState()
    {
        StartCoroutine(ChangeValuesCorutine(0));
    }

    protected IEnumerator ChangeStateCorutine(float value, float speed)
    {
        float currentCount = 0;
        float startValue = currentValue;
        while (currentCount < 1)
        {
            currentCount = currentCount + 0.01f;
            currentValue = (float)System.Math.Round(Mathf.Lerp(startValue, value, currentCount), 2);
            yield return StartCoroutine(ChangeValuesCorutine(currentValue));
            //ChangeValues(currentValue);
            yield return new WaitForSeconds(speed / 100);
        }
        yield return null;
    }

    abstract protected IEnumerator ChangeValuesCorutine(float value);
}
