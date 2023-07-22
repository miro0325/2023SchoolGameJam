using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField] protected float Speed;
    public int Hp = 5; //HP
    public int atkDamage;

    public Text MHpText;

    protected bool isAttack = false;

    [SerializeField] protected float attackCooltime = 1f;
    [SerializeField] protected float curAttackCooltime = 0f;
    [SerializeField] protected float attackRange;

    public virtual void Start()
    {
        GameManager.Instance.curEnemys.Add(this.transform);

    }


    void Update()
    {
        MHpText.text = Hp.ToString("F0");
    }

    public virtual void Attack()
    {
        if (isAttack) return;
        curAttackCooltime += Time.deltaTime;
        if(curAttackCooltime >= attackCooltime)
        {
            curAttackCooltime = 0;
            Player.Instance.hp -= atkDamage;
        }
    }
    public virtual void AttackRange()
    {

    }

    public abstract void Death();

    public virtual void Movement()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) < attackRange)
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
            GameManager.Instance.curEnemys.Remove(this.transform);
            Death();
        }
    }
}
