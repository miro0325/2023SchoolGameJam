using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public int AttackPower = 2; //공격력

    public float AttackSpeed = 0.5f; //공격 속도

    public Transform Player; //플레이어 위치값
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
    }
    void PlayerAttack()
    {
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");
    }
}
