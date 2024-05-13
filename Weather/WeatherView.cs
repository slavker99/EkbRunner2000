using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ����� WeatherView
///  �������� ���������� �������� �������� �������, ������� ����� ����������.
///  � ��� ���������: ���������, ������� (�����, ����), ��������� �����.
///  ����� ���� ����� �������� �� ����������� ���� �������� ����� �� �������.
/// </summary>

public class WeatherView : MonoBehaviour
{
    [SerializeField] private bool randomSkyboxRotate = true;
    // ������� ������
    [Header("���������")]
    public MeshRenderer CloudsSkybox;
    public GameObject FonCity;

    [Header("�������")]
    public ParticleSystem RainParticles;
    public ParticleSystem SnowParticles;

    [Header("���������")]
    public Light MainLight;
    public Light ZakatSkyboxLight;
    public Light NightLight;
    public Light NightRoadLight;
    public Light SkyboxLight;
    public Transform Sun;
    public LensFlare Flare;

    [Header("���������")]
    public Material FonCityMt;
    public List<Material> NightMaterials;

    [Header("��������� ������")]
    public Material RoadMT;
    public Material RoadWaterMT;

    [Header("��� ����������� �� �������")]
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
