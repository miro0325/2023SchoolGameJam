using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Monster; //���� ������Ʈ

    public float maxSpawnDelay = 1; //�ִ� ���� ������
    public float curSpawnDelay = 0; //���� ���� ������

    public Transform spawnPoints; //������ġ


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
