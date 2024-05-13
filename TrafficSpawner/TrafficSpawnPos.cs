using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawnPos : MonoBehaviour
{
    public bool SpawnTramway;
    public bool SpawnTrolley;

    [Header("Позиции")]
    public List<CarSpawnPosition> positions;


    public void Initialization()
    {
        positions = new List<CarSpawnPosition>();
        foreach (var pos in this.transform.GetComponentsInChildren<Transform>())
            if (pos.tag == "PosCar")
            {
                positions.Add(pos.GetComponent<CarSpawnPosition>());
            }
    }
}
