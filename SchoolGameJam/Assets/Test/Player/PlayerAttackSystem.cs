using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public static PlayerAttackSystem Instance { get; set; }

    
    public int AttackPower = 2; //공격력

    

    public Transform point; //공격한 후 위치값
    public Transform point2; // 공격 전 위치값

    Coroutine coroutine;

    bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerAttack();

    }
    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttack)
        {
            isAttack = true;
            transform.position = point2.position;
            transform.GetComponent<SpriteRenderer>().enabled = true;

            if (coroutine != null) StopCoroutine(coroutine);
            transform.DOMoveX(point.position.x, 0.4f).SetEase(Ease.OutFlash);
            transform.DOMoveY(point.position.y, 0.4f).SetEase(Ease.InQuad);
            coroutine = StartCoroutine(ResetPos(0.45f));

            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            MonsterBase monsterBase = collision.GetComponent<MonsterBase>();
            monsterBase.Damaged(AttackPower);
        }
    }   


    IEnumerator ResetPos(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.GetComponent<SpriteRenderer>().enabled = false;
        isAttack = false;
    }
}
