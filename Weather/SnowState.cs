using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   ласс SnowState
///  Ќаследник класса WeatherState
///  —одержит параметры элементов погоды, которые должны измен€тьс€ дл€ перехода в состо€ние снега.
///  »зменение происходит через выполнение метода ChangeValues в корутине базового класса
/// </summary>
/// 
public class SnowState : MonoBehaviour
{
    [SerializeField] private float particlesDefCount = 0;
    [SerializeField] private float particlesFinalCount = 1000;

}
