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
        UIUpdate();
    }

    public override void Movement()
    {
        base.Movement();
    }


    public override void Death()
    {
        GameManager.Instance.currentValue++;
        if(GameManager.Instance.currentValue < 0) GameManager.Instance.currentValue = 0;
        Destroy(gameObject);
    }

   
}