using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float launchAngle = 45f; // 발사각 (최적값은 45도)
    public float launchSpeed = 10f; // 발사 속도

    [SerializeField] private Rigidbody2D rb;
    private bool isMoving = false;

    public void SetTargetPos(Transform target)
    {
        this.target = target;
        Vector2 middlePos = ((transform.position + target.position) / 2) + Vector3.up * 2;
        StartCoroutine(ShootBulletWithBegior(transform.position, middlePos, target.position, Vector2.Distance(transform.position,target.position) * 0.05f));
        isMoving = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            MonsterBase monster = collision.GetComponent<MonsterBase>();
            monster.Damaged(Player.Instance.AttackPower);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isMoving)
        //{
        //    if (target != null)
        //    {
        //        Vector3 targetDir = target.position - transform.position;
                
                
        //        rb.velocity = CalculateLaunchVelocity(targetDir);
        //    }
        //    else
        //    {
        //        // target이 null인 경우 초기 속도를 설정하지 않고 발사체를 그냥 정지시킵니다.
        //        rb.velocity = Vector2.zero;
        //    }
           
        //}
    }

    //Vector2 CalculateLaunchVelocity(Vector2 targetDir)
    //{
        
    //    float projectileDistance = targetDir.y;
        
    //    targetDir.y = 0;
    //    float distance = targetDir.magnitude;
    //    float radianAngle = launchAngle * Mathf.Deg2Rad;
    //    float g = Physics.gravity.y;
    //    float launchVelocity = Mathf.Sqrt((distance * g) / Mathf.Sin(2 * radianAngle));
        
    //    Vector2 launchVelocityXZ = targetDir.normalized * launchVelocity;

        
    //    float launchVelocityY = Mathf.Sqrt(-2 * g * projectileDistance);
    //    Debug.Log(launchVelocity);

    //    return launchVelocityXZ + Vector2.up * launchVelocityY;
    //}


    private Vector3 GetBegior(Vector3 startPos, Vector3 MiddlePos, Vector3 EndPos, float normalizedVal)
    {
        var line1 = Vector3.Lerp(startPos, MiddlePos, normalizedVal);
        var line2 = Vector3.Lerp(MiddlePos, EndPos,normalizedVal);
        return Vector3.Lerp(line1, line2, normalizedVal);
    }

    IEnumerator ShootBulletWithBegior(Vector3 startPos, Vector3 MiddlePos, Vector3 EndPos, float duration)
    {
        float time = 0;
        Vector2 targetPos = target.position;
        while (time < duration)
        {
            if (target == null) Destroy(this.gameObject);
            else targetPos = target.position;
            time += Time.deltaTime;
            var normlizedTime = time/ duration;
            var curPos = GetBegior(startPos, MiddlePos, targetPos, normlizedTime);
            transform.position = curPos;
            yield return null;
        }
        transform.position = EndPos;
    }
}
