using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMove : MonoBehaviour
{
    public float Speed = 0.05f; //몬스터 이동 속도

    void Update()
    {
        MonsterMoveSystem(); //이동
    }

    void MonsterMoveSystem() //이동
    {
        Vector3 movement = new Vector3(-Speed, 0, 0);
        transform.Translate(movement * Time.deltaTime);
    }
}