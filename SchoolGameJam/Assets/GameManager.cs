using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Monster; //몬스터 오브젝트

    public float maxSpawnDelay = 1; //최대 스폰 딜레이
    public float curSpawnDelay = 0; //현재 스폰 딜레이

    public Transform spawnPoints; //스폰위치


    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
    }
    void SpawnEnemy()
    {
        Instantiate(Monster, spawnPoints.position, spawnPoints.rotation);
    }
}
