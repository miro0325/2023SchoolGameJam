using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }

    public int hp = 5;

    public int AttackPower = 2;
    public GameObject bullet;

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
            //transform.position = point2.position;
            //transform.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(ResetPos(0.45f));
            var _b = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();   
            _b.SetTargetPos(GetNearestTarget());
            //transform.DOMoveX(point.position.x, 0.4f).SetEase(Ease.OutFlash);
            //transform.DOMoveY(point.position.y, 0.4f).SetEase(Ease.InQuad);


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
