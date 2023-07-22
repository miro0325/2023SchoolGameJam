using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSystem : MonoBehaviour
{
    public int Hp = 5; //HP

    public Text HpText; //HP≈ÿΩ∫∆Æ

    void Update()
    {

        

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon")
        {
            Hp -= PlayerAttackSystem.Instance.AttackPower;
            
        }
    }
}
