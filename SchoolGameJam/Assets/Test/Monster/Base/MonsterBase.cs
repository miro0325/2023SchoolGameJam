using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    bool isDeath = false;

    [SerializeField] int earn;
    [SerializeField] GameObject earnUI;

    [SerializeField] Sprite deathSprite;

    Animator animator;


    [SerializeField] Vector3 offset;

    public virtual void Start()
    {
        GameManager.Instance.curEnemys.Add(this.transform);
        maxHP = Hp;
        worldCanvas = GameManager.Instance.worldCanvas;
        _hpBar = Instantiate(hpBar, worldCanvas).transform;
        _hpBar.position = transform.position + offset;
        animator = GetComponent<Animator>();
    }

    public void UIUpdate()
    {
        
        if(_hpBar != null)
        {
            _hpBar.position = transform.position + offset;
            _hpBar.GetChild(0).GetComponent<Image>().fillAmount = (float)Hp / (float)maxHP;

        }
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
        if(isDeath) return;
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
        if (isDeath) return;

        Hp -= value;
        var ui = Instantiate(earnUI, worldCanvas);
        ui.transform.position = transform.position;
        ui.GetComponent<Text>().text = value.ToString();
        if (Hp <= 0)
        {
           isDeath = true;
            GameManager.Instance.curEnemys.Remove(this.transform);
            Destroy(_hpBar.gameObject);
            GameManager.SetCoin(GameManager.GetCoin() + earn);
            animator.enabled = false;

            StartCoroutine(IDeath());
        }
    }

    IEnumerator IDeath()
    {

        var a = transform.GetComponent<SpriteRenderer>();
        a.sprite = deathSprite;
        var t = a.DOFade(0,0.6f);
        yield return t.WaitForCompletion();
        Death();
    }
}
