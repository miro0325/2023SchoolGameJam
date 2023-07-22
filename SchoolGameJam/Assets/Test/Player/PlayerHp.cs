using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public static PlayerHp Instance { get; set; }
    
    public int PlayerHP = 10; //플레이어 Hp
    public Text playerHp; //플레이어 HP 텍스트



    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
        Debug.Log(Instance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        playerHp.text = PlayerHP.ToString("F0");

        if (PlayerHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Monster")
    //    {
    //        PlayerHP--;
    //    }
        
    //}
}
