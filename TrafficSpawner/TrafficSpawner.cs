using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    [Header("Основные Настройки")]
    public float lane1Speed = 50;
    public float lane2Speed = 50;
    public float lane3Speed = 30;
    public float lane4Speed = 30;
    public int carsChanseSpawn = 40;
    public int tramwayChanseSpawn = 8;
    public int MaxBotsCount = 70;
    [ReadOnly] public int BotsCount = 0;

    [Header("Объекты")]
    public List<Transform> fastCarList;
    public List<Transform> slowCarList;
    public List<Transform> slowCarAndTrolleyList;
    public List<Transform> tramList;
    public GameObject trafficContainer;

    public void SpawnTraffic(TrafficSpawnPos positions)
    {
        if (positions.SpawnTramway)
        {
            //SpawnLane(positions.ForwardL1, tramList, true, tramwayChanseSpawn, lane4Speed);
            //SpawnLane(positions.BackL1, tramList, false, tramwayChanseSpawn, lane4Speed);
        }
        else
        {
            //SpawnLane(positions.ForwardL1, fastCarList, true, carsChanseSpawn, lane1Speed);
            //SpawnLane(positions.BackL1, fastCarList, false, carsChanseSpawn, lane1Speed);
        }

        //SpawnLane(positions.ForwardL2, fastCarList, true, carsChanseSpawn, lane2Speed);
        //SpawnLane(positions.BackL2, fastCarList, false, carsChanseSpawn, lane2Speed);
        //SpawnLane(positions.ForwardL3, slowCarList, true, carsChanseSpawn, lane3Speed);
        //SpawnLane(positions.BackL3, slowCarList, false, carsChanseSpawn, lane3Speed);

        if (positions.SpawnTrolley) // если сегмент нормал то к спавну в правую полосу добавляются троллейбусы
        {
            //SpawnLane(positions.ForwardL4, slowCarAndTrolleyList, true, carsChanseSpawn, lane4Speed);
            //SpawnLane(positions.BackL4, slowCarAndTrolleyList, false, carsChanseSpawn, lane4Speed);
        }
        else
        {
            //SpawnLane(positions.ForwardL4, slowCarList, true, carsChanseSpawn, lane4Speed);
            //SpawnLane(positions.BackL4, slowCarList, false, carsChanseSpawn, lane4Speed);
        }
    }

    public void SpawnLane(List<Transform> lane, List<Transform> cars, bool orientation, int chanseSpawn, float speed)
    {
        foreach (var pos in lane)
        {
            int numCar = Random.Range(0, cars.Count);
            int chanse = Random.Range(0, 100);
            if (chanse < chanseSpawn)
            {
                if (BotsCount < MaxBotsCount)
                {
                    if (orientation)
                    {
                        SpawnCar(cars[numCar], pos, orientation, speed);
                        BotsCount++;
                    }
                }
            }
        }
    }

    public void SpawnCar(Transform carObject, Transform pos, bool orientation, float speed)
    {
        Quaternion rotationCar = Quaternion.Euler(new Vector3(0, pos.transform.rotation.eulerAngles.y + 90, 0));
        var car = Instantiate(carObject, Vector3.zero, rotationCar, trafficContainer.transform);
        var carScript = car.GetComponent<BotMovement>();
        carScript.SetParams(pos, orientation, speed);
    }

}

public enum TypeTransport
{
    Car,
    Trolley,
    Tramway
}
