using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMove : MonsterBase
{
    

    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
    }


    public override void Deasth()
    {
        Destroy(gameObject);
    }

   
}