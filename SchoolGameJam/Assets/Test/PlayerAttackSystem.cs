using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public int AttackPower = 2; //���ݷ�

    public float AttackSpeed = 0.5f; //���� �ӵ�

    public Transform Player; //�÷��̾� ��ġ��
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
