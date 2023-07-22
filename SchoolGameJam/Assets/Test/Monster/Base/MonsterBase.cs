using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] Transform worldCanvas;
    public int Hp = 5; //HP
    private int maxHP;
    public int atkDamage;
    [SerializeField] protected GameObject hpBar;
    Transform _hpBar;
    bool isAttack = false;
    float curAttackCooltime = 0;
    [SerializeField] float attackCooltime = 1;
    [SerializeField] float attackRange = 2.5f;


    [SerializeField] Vector3 offset;
    public virtual void Start()
    {
        GameManager.Instance.curEnemys.Add(this.transform);
        maxHP = Hp;
        _hpBar = Instantiate(hpBar, worldCanvas).transform;
        _hpBar.position = transform.position + offset;
    }

    public void UIUpdate()
    {
        _hpBar.position = transform.position + offset;
        _hpBar.GetChild(0).GetComponent<Image>().fillAmount = (float)Hp / (float)maxHP;
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
