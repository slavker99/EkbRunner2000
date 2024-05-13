using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using static Cinemachine.CinemachineBrain;

/// <summary>
///  Класс RainState
///  Наследник класса WeatherState
///  Содержит параметры элементов погоды, которые должны изменяться для перехода в состояние дождя.
///  Изменение происходит через выполнение метода ChangeValues в корутине базового класса
/// </summary>
/// 
public class RainState : WeatherState
{
    public static bool isRain;
    public static UnityEvent<float> RainEvent = new UnityEvent<float>();

    private Light mainLight;
    [SerializeField] private float mainLightDefVal = 8;
    [SerializeField] private float mainLightFinalVal = 0;

    [SerializeField] private Color mainLightDefColor;
    [SerializeField] private Color mainLightFinColor;

    private ParticleSystem rainParticles;
    [SerializeField] private float particlesDefCount = 0;
    [SerializeField] private float particlesFinalCount = 1000;

    private MeshRenderer cloudsSkybox;
    [SerializeField] private float cloudsDefTransp = 0;
    [SerializeField] private float cloudsFinalTransp = 1;

    private Light skyboxLight;
    [Header("Ночная подсветка скайбокса")]
    [SerializeField] private float skyboxLightDefVal = 5f;
    [SerializeField] private float skyboxLightFinVal = 3f;

    private Material roadMt;
    private Material roadWaterMt;


    private void Awake()
    {
        weatherType = WeatherType.Rain;
        isRain = false;
        if (stateMachine != null)
        {
            mainLight = stateMachine.View.MainLight;
            rainParticles = stateMachine.View.RainParticles;
            cloudsSkybox = stateMachine.View.CloudsSkybox;
            skyboxLight = stateMachine.View.SkyboxLight;

            roadMt = stateMachine.View.RoadMT;
            roadWaterMt = stateMachine.View.RoadWaterMT;
        }
    }

    protected override IEnumerator ChangeValuesCorutine(float value)
    {
        RainEvent.Invoke(value);
        yield return new WaitForFixedUpdate();
        mainLight.intensity = (float)Math.Round(Mathf.Lerp(mainLightDefVal, mainLightFinalVal, value), 2);
        yield return new WaitForFixedUpdate();
        mainLight.color = Color.Lerp(mainLightDefColor, mainLightFinColor, value);
        yield return new WaitForFixedUpdate();
        rainParticles.emissionRate = (float)Math.Round(Mathf.Lerp(particlesDefCount, particlesFinalCount, value), 2);
        yield return new WaitForFixedUpdate();
        var color = cloudsSkybox.material.color;
        var cloudsCurState = Mathf.Lerp(cloudsDefTransp, cloudsFinalTransp, value);
        cloudsSkybox.material.color = new Color(color.r, color.g, color.b, cloudsCurState);
        yield return new WaitForFixedUpdate();
        skyboxLight.intensity = (float)Math.Round(Mathf.Lerp(skyboxLightDefVal, skyboxLightFinVal, value), 2);
        yield return new WaitForFixedUpdate();
        ChangeRainMaterial(value);
        if (value == 0.6f)
        {
            isRain = !isRain;
            //Debug.Log("Ивент SimpleRain: " + isRain);
            stateMachine.View.Flare.enabled = !isRain;
        }
        yield return null;
    }

    private void ChangeRainMaterial(float value)
    {
        roadMt.SetFloat("_Metallic", Mathf.Lerp(0, 0.1f, value));
        roadMt.SetFloat("_Glossiness", Mathf.Lerp(0, 0.8f, value));
       //if (value < 0.5f)
        //    ChangeTransparent(roadWaterMt, 0);
        //else
        //    ChangeTransparent(roadWaterMt, value*value);
    }
    private void ChangeTransparent(Material mt, float val)
    {
        var color = mt.color;
        mt.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, val));
    }
}
