using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonsterBase
{
    public override void Death()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        UIUpdate();
    }

    public override void Movement()
    {
            transform.Translate(transform.right * -1 * Speed);
        if(transform.position.x < 6f)
        {
            transform.position = new Vector2(6f, transform.position.y);
        }
    }
}
