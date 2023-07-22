using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMove : MonsterBase
{
    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
    }


    public override void Death()
    {
        Destroy(gameObject);
    }

   
}