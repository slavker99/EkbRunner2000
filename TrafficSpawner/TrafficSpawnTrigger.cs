using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawnTrigger : MonoBehaviour
{
    [SerializeField] private TrafficSpawnerDynamic spawner;
    [SerializeField] private Transform spawnPos;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spawner.SpawnTrafficOptimized(spawnPos);
            this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y - 4.75f, 0);
        }
    }
}
