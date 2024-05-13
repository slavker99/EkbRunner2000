using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

/// <summary>
///  Класс NightState
///  Наследник класса WeatherState
///  Содержит параметры элементов погоды, которые должны изменяться для перехода в ночное состояние погоды.
///  Изменение происходит через выполнение метода ChangeValues в корутине базового класса
/// </summary>
/// 
public class NightState : WeatherState
{
    public static bool isNight = false;
    public static UnityEvent<float> NightEvent = new UnityEvent<float>();
    public static UnityEvent<bool> SimpleNightEvent = new UnityEvent<bool>();

    private Light mainLight;
    [Header("Основной свет")]
    [SerializeField] private float mainLightDefVal = 4;
    [SerializeField] private float mainLightFinVal = 0;
    [SerializeField] private float mainLightDefAngleX = 10;
    [SerializeField] private float mainLightFinAngleX = -10;

    private Transform sun;
    [Header("Солнце")]
    [SerializeField] private float sunDefPosY = 209;
    [SerializeField] private float sunFinPosY = -59;

    
    private Light skyboxZakatLight;
    [Header("Подсветка скайбокса от солнца")]
    [SerializeField] private float skyboxZakatDefVal = 8;
    [SerializeField] private float skyboxZakatFinVal = 0;

    private Light nightLight;
    [Header("Ночная подсветка окон")]
    [SerializeField] private float nightLightDefVal = 0;
    [SerializeField] private float nightLightFinVal = 8;

    private Light nightRoadLight;
    [Header("Ночная подсветка дороги")]
    [SerializeField] private float nightRoadLightDefVal = 0;
    [SerializeField] private float nightRoadLightFinVal = 0.4f;

    private Light skyboxLight;
    [Header("Ночная подсветка скайбокса")]
    [SerializeField] private float skyboxLightDefVal = 0;
    [SerializeField] private float skyboxLightFinVal = 1f;

    [Header("Цвет глобальной подсветки")]
    [SerializeField] private Color globalIntensityDefColor;
    [SerializeField] private Color globalIntensityFinColor;

    private Material fonCityMt;
    [Header("Цвет фона города")]
    [SerializeField] private Color fonCityDefColor;
    [SerializeField] private Color fonCityFinColor;

    private MeshRenderer cloudsSkybox;
    [Header("Прозрачность облаков")]
    [SerializeField] private float cloudsDefTransp = 0;
    [SerializeField] private float cloudsFinalTransp = 0.5f;

    private void Awake()
    {
        weatherType = WeatherType.Night;
        currentValue = 0;
        isNight = false;
        if (base.stateMachine != null)
        {
            mainLight = base.stateMachine.View.MainLight;
            nightLight = base.stateMachine.View.NightLight;
            nightRoadLight = base.stateMachine.View.NightRoadLight;
            skyboxZakatLight = base.stateMachine.View.ZakatSkyboxLight;
            skyboxLight = base.stateMachine.View.SkyboxLight;
            sun = base.stateMachine.View.Sun;
            fonCityMt = stateMachine.View.FonCityMt;
            cloudsSkybox = stateMachine.View.CloudsSkybox;
        }

        foreach (var mt in stateMachine.View.NightMaterials)
        {
            ChangeTransparent(mt, 0.3f);
        }
    }

    protected override IEnumerator ChangeValuesCorutine(float value)
    {
        NightEvent.Invoke(value);
        if (value == 0.6f)
        {
            isNight = !isNight;
            SimpleNightEvent.Invoke(isNight);
            Debug.Log("Ивент SimpleNight: " + isNight);
            stateMachine.View.Flare.enabled = !isNight;
        }
        yield return new WaitForFixedUpdate();
        mainLight.intensity = (float)Math.Round(Mathf.Lerp(mainLightDefVal, mainLightFinVal, value), 2);
        yield return new WaitForFixedUpdate();
        skyboxZakatLight.intensity = (float)Math.Round(Mathf.Lerp(skyboxZakatDefVal, skyboxZakatFinVal, value), 2);
        yield return new WaitForFixedUpdate();
        nightRoadLight.intensity = (float)Math.Round(Mathf.Lerp(nightRoadLightDefVal, nightRoadLightFinVal, value), 2);
        yield return new WaitForFixedUpdate();
        skyboxLight.intensity = (float)Math.Round(Mathf.Lerp(skyboxLightDefVal, skyboxLightFinVal, value), 2);
        yield return new WaitForFixedUpdate();
        RenderSettings.ambientLight = Color.Lerp(globalIntensityDefColor, globalIntensityFinColor, value);
        yield return new WaitForFixedUpdate();
        fonCityMt.color = Color.Lerp(fonCityDefColor, fonCityFinColor, value);
        yield return new WaitForFixedUpdate();
        sun.transform.localPosition = new Vector3(
            sun.transform.localPosition.x,
            Mathf.Lerp(sunDefPosY, sunFinPosY, value),
            sun.transform.localPosition.z);
        yield return new WaitForFixedUpdate();
        mainLight.transform.localRotation = Quaternion.Euler(
            Mathf.Lerp(mainLightDefAngleX, mainLightFinAngleX, value),
            mainLight.transform.localEulerAngles.y,
            mainLight.transform.localEulerAngles.z);
        yield return new WaitForFixedUpdate();
        var color = cloudsSkybox.material.color;
        cloudsSkybox.material.color = new Color(color.r, color.g, color.b, Mathf.Lerp(cloudsDefTransp, cloudsFinalTransp, value));
        yield return new WaitForFixedUpdate();
        foreach (var mt in stateMachine.View.NightMaterials)
        {
            ChangeTransparent(mt, value);
        }
        yield return null;
    }

    private void ChangeTransparent(Material mt, float val)
    {
        var color = mt.color;
        mt.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, val));
    }
}
