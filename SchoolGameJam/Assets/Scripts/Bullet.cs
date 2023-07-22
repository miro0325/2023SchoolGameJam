using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float launchAngle = 45f; // �߻簢 (�������� 45��)
    public float launchSpeed = 10f; // �߻� �ӵ�

    private Rigidbody2D rb;
    private bool isMoving = false;

    public void SetTargetPos(Transform target)
    {
        this.target = target;
        //transform.DOMoveX(target.position.x, 0.4f).SetEase(Ease.OutFlash);
        //transform.DOMoveY(target.position.y, 0.4f).SetEase(Ease.InQuad);
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
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (target != null)
            {
                Vector3 targetDir = target.position - transform.position;
                rb.velocity = CalculateLaunchVelocity(targetDir);
            }
            else
            {
                // target�� null�� ��� �ʱ� �ӵ��� �������� �ʰ� �߻�ü�� �׳� ������ŵ�ϴ�.
                rb.velocity = Vector2.zero;
            }
           
        }
    }

    Vector2 CalculateLaunchVelocity(Vector2 targetDir)
    {
        
        float projectileDistance = targetDir.y;
        targetDir.y = 0;
        float distance = targetDir.magnitude;
        float radianAngle = launchAngle * Mathf.Deg2Rad;
        float g = Physics.gravity.y;
        float launchVelocity = Mathf.Sqrt((distance * g) / Mathf.Sin(2 * radianAngle));

        
        Vector2 launchVelocityXZ = targetDir.normalized * launchVelocity;

        
        float launchVelocityY = Mathf.Sqrt(-2 * g * projectileDistance);

       
        return launchVelocityXZ + Vector2.up * launchVelocityY;
    }

}