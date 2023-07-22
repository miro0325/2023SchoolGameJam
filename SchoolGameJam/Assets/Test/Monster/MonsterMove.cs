using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMove : MonoBehaviour
{
    public float Speed = 0.05f; //���� �̵� �ӵ�

    void Update()
    {
        MonsterMoveSystem(); //�̵�
    }

    void MonsterMoveSystem() //�̵�
    {
        Vector3 movement = new Vector3(-Speed, 0, 0);
        transform.Translate(movement * Time.deltaTime);
    }
}