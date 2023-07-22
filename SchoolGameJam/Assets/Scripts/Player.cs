using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }

    public float AttackTime;

    [SerializeField] Transform worldCanvas;
    private int maxhp;
    public int hp = 5;
    [SerializeField]  GameObject HpBar;
    Transform _HpBar;

    public Image[] skillCoolImages;

    public int AttackPower = 2;
    public GameObject bullet;

    public float damageAmount = 10f; 
    public float skillRange = 5f; //스킬 범위

    bool isSkillUse = false;


    public bool[] requireSkills;
    [SerializeField] private float[] curSkillCooltimes;
    [SerializeField] private float[] maxSkillCooltimes;
    [SerializeField] private bool[] useSkills;


    Coroutine coroutine;
    bool isAttack = false;

    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
        maxhp = hp;
        worldCanvas = GameManager.Instance.worldCanvas;
        _HpBar = Instantiate(HpBar, worldCanvas).transform;
        _HpBar.position = transform.position + offset;
    }
    

    // Update is called once per frame
    void Update()
    {
        PlayerAttack();
        UseSkill();
        UIUpdate();

        AttackTime += Time.deltaTime;
        
        
        
    }

    public void UIUpdate()
    {
        _HpBar.position = transform.position + offset;
        _HpBar.GetChild(0).GetComponent<Image>().fillAmount = (float)hp / (float)maxhp;
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack)
        {
            isAttack = true;
            //transform.position = point2.position;
            //transform.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(ResetPos(0.45f));
            if(!isSkillUse)
            {
                var _b = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();   
                _b.SetTargetPos(GetNearestTarget());

            } else
            {
                StartCoroutine(ISkillBullet());
            }
            //transform.DOMoveX(point.position.x, 0.4f).SetEase(Ease.OutFlash);
            //transform.DOMoveY(point.position.y, 0.4f).SetEase(Ease.InQuad);


        }

    }

    public virtual void Damaged(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            GameManager.Instance.curEnemys.Remove(this.transform);
            Destroy(_HpBar.gameObject);
            Destroy(gameObject);
        }
    }

    

    void UseSkill()
    {
        for(int i = 0; i < maxSkillCooltimes.Length; i++)
        {

            if (!useSkills[i])
            {
                skillCoolImages[i].fillAmount = 0;

                continue;
            }
            curSkillCooltimes[i] = (useSkills[i]) ? curSkillCooltimes[i] + Time.deltaTime : 0;
            skillCoolImages[i].fillAmount = (maxSkillCooltimes[i] -curSkillCooltimes[i]) / maxSkillCooltimes[i];

            if(curSkillCooltimes[i] >= maxSkillCooltimes[i])
            {
                curSkillCooltimes[i] = 0;
                useSkills[i] = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillQ();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SkillW();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SkillE();
        }
    } 

    

    void SkillQ()
    {
        if(!requireSkills[0] && !useSkills[0]) 
        {
            useSkills[0] = true;
            StartCoroutine(ISkillQ());
        }
    }

    IEnumerator ISkillQ()
    {
        AttackPower = 50 * (1 + GameManager.Instance.upgradeDamage);
        yield return new WaitForSeconds(5f);
        AttackPower = 50;

    }

    void SkillW()
    {
        if (!requireSkills[1] && !useSkills[1])
        {
            useSkills[1] = true;
            StartCoroutine(ISkillW());

        }
    }
    IEnumerator ISkillW()
    {
       
            foreach(var enemy in GameManager.Instance.curEnemys)
            {
                enemy.GetComponent<MonsterBase>().Damaged(AttackPower * 3 * (1 + GameManager.Instance.upgradeSkill));
            }
            yield return new WaitForSeconds(0.1f);
        
    }

    void SkillE()
    {
        if (!requireSkills[2] && !useSkills[2])
        {
            useSkills[2] = true;
            isSkillUse = true;
            StartCoroutine(ISkillE());
        }
        //if(Input.GetKeyDown(KeyCode.E))
        //{

        //}
    }

    IEnumerator ISkillE()
    {

        
        yield return new WaitForSeconds(5f);
        isSkillUse = false;

    }

    IEnumerator ISkillBullet()
    {

        for(int i = 0; i < GameManager.Instance.upgradeSkill2 + 1; i++)
        {
            var _b = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
            _b.SetTargetPos(GetNearestTarget());
            yield return new WaitForSeconds(0.1f);
        }

    }

    Transform GetNearestTarget()
    {
        float closetDistance = Mathf.Infinity;
        if (GameManager.Instance.curEnemys.Count == 0) return null; 
        Transform target = GameManager.Instance.curEnemys[0];

        foreach (var t in GameManager.Instance.curEnemys)
        {
            if(t == null) continue;
            float distanceToEnemy = Vector3.Distance(transform.position, t.position);
            Transform tar = t;
            // 현재까지 계산한 가장 가까운 적보다 더 가까운 경우 변수 업데이트
            if (distanceToEnemy < closetDistance)
            {
                closetDistance = distanceToEnemy;
                target = t;
            }
        }
        return target;
    }

    IEnumerator ResetPos(float delay)
    {
        yield return new WaitForSeconds(delay);
        //transform.GetComponent<SpriteRenderer>().enabled = false;
        isAttack = false;
    }
}
