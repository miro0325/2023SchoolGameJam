using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField] protected float Speed;
    public int Hp = 5; //HP
    public int atkDamage;

    protected bool isAttack = false;

    [SerializeField] protected float attackCooltime = 1f;
    [SerializeField] protected float curAttackCooltime = 0f;
    [SerializeField] protected float attackRange;

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    public virtual void Attack()
    {
        if (isAttack) return;
        curAttackCooltime += Time.deltaTime;
        if(curAttackCooltime >= attackCooltime)
        {
            curAttackCooltime = 0;
            PlayerHp.Instance.PlayerHP -= atkDamage;
        }
    }
    public virtual void AttackRange()
    {

    }

    public abstract void Deasth();

    public virtual void Movement()
    {
        if (Vector2.Distance(transform.position, PlayerHp.Instance.transform.position) < attackRange)
        {
            Attack();
            return;
        }
        Vector3 movement = new Vector3(-Speed, 0, 0);
        transform.Translate(movement * Time.deltaTime);
    }

    public virtual void Damaged(int value)
    {
        Hp -= value;
        if(Hp <= 0)
        {
            Deasth();
        }
    }
}
