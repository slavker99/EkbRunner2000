using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Класс WeatherView
///  Содержит визуальные элементы погодной системы, которые могут изменяться.
///  К ним относятся: скайбоксы, частицы (дождь, снег), источники света.
///  Также этот класс отвечает за перемещение этих объектов вслед за игроком.
/// </summary>

public class WeatherView : MonoBehaviour
{
    [SerializeField] private bool randomSkyboxRotate = true;
    // Объекты погоды
    [Header("Скайбоксы")]
    public MeshRenderer CloudsSkybox;
    public GameObject FonCity;

    [Header("Частицы")]
    public ParticleSystem RainParticles;
    public ParticleSystem SnowParticles;

    [Header("Освещение")]
    public Light MainLight;
    public Light ZakatSkyboxLight;
    public Light NightLight;
    public Light NightRoadLight;
    public Light SkyboxLight;
    public Transform Sun;
    public LensFlare Flare;

    [Header("Материалы")]
    public Material FonCityMt;
    public List<Material> NightMaterials;

    [Header("Материалы Дороги")]
    public Material RoadMT;
    public Material RoadWaterMT;

    [Header("Для перемещения за игроком")]
    [SerializeField] private Transform sunTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform lightsTransform;
    [SerializeField] private Transform skyboxesTransform;
    [SerializeField] private Transform particlesTransform;

    private void Awake()
    {
        if (randomSkyboxRotate)
            transform.Rotate(0,Random.Range(0,360),0);
    }

    private void Update()
    {
        if (playerTransform != null)
            MoveObjects();
    }

    private void MoveObjects()
    {
        transform.position = playerTransform.position;
        //skyboxesTransform.SetLocalPositionAndRotation(playerTransform.position, Quaternion.Euler(Vector3.zero));
        particlesTransform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
        //lightsTransform.localPosition = playerTransform.position;
    }
}
