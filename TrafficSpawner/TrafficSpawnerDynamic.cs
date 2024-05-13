using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TrafficSpawnerDynamic : MonoBehaviour
{
    [Header("Основные Настройки")]
    public float trafficSpeed = 30;
    public float speedCoef = 0.0004f;
    public TrafficSpawnTrigger trigger;
    public Transform trafficContainer;
    [ReadOnly] public int BotsCount = 0;

    [Header("Объекты")]
    public List<GameObject> smallCars;
    public List<GameObject> bigCars;
    public List<GameObject> trams;
    public List<GameObject> trolleys;
    public List<GameObject> scoreBoosters;

    public List<TrafficSpawnPos> TrafficPresets;

    private Dictionary<TypeSpawnTransport, List<GameObject>> carsDict;

    private void Awake()
    {
        carsDict = new Dictionary<TypeSpawnTransport, List<GameObject>>()
        {
            { TypeSpawnTransport.SmallCar, smallCars },
            { TypeSpawnTransport.BigCar, bigCars },
            { TypeSpawnTransport.Tramway, trams },
            { TypeSpawnTransport.Trolley, trolleys },
            { TypeSpawnTransport.ScoreBooster, scoreBoosters }
        };

        foreach (var preset in TrafficPresets)
            preset.Initialization();
    }

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        //rb.angularVelocity = new Vector3(0.0f, -0.0002f * trafficSpeed, 0.0f);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, trafficSpeed * -speedCoef, 0));
    }

    public void SpawnTraffic(Transform presetPos)
    {
        var num = Random.Range(0, TrafficPresets.Count);
        var presetObj = Instantiate(TrafficPresets[num].gameObject, Vector3.zero, presetPos.rotation, this.transform);
        var posPreset = presetObj.GetComponent<TrafficSpawnPos>();
        var posPresetMovement = presetObj.GetComponent<BotMovement>();
        //posPresetMovement.SetSpeed(trafficSpeed);
        foreach (var pos in posPreset.positions)
            SpawnCar(pos);
        Destroy(presetObj);
    }

    public void SpawnTrafficOptimized(Transform presetPos)
    {
        StartCoroutine(SpawnTrafficCorutine(presetPos));
    }

    private IEnumerator SpawnTrafficCorutine(Transform presetPos)
    {
        var num = Random.Range(0, TrafficPresets.Count);
        var presetObj = Instantiate(TrafficPresets[num].gameObject, Vector3.zero, presetPos.rotation, this.transform);
        var posPreset = presetObj.GetComponent<TrafficSpawnPos>();
        var posPresetMovement = presetObj.GetComponent<BotMovement>();
        foreach (var pos in posPreset.positions)
        {
            SpawnCar(pos);
            yield return new WaitForFixedUpdate();
        }
        Destroy(presetObj);
        yield return null;
    }

    public void SpawnCar(CarSpawnPosition pos)
    {
        int num = Random.Range(0, carsDict[pos.type].Count);
        var car = Instantiate(carsDict[pos.type][num], Vector3.zero, pos.transform.rotation, trafficContainer.transform);
        var carScript = car.GetComponent<BotMovement>();
        carScript.SetParams(pos.transformPos, pos.orientation, trafficSpeed);
    }

    public void OffTraffic()
    {
        trafficContainer.gameObject.SetActive(false);
    }
}

public enum TypeSpawnTransport
{
    SmallCar,
    BigCar,
    Trolley,
    Tramway,
    ScoreBooster
}
