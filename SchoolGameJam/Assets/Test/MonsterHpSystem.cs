using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpSystem : MonoBehaviour
{
    public int Hp = 5; //HP

    public Text HpText; //HP≈ÿΩ∫∆Æ

    void Update()
    {

        HpText.text = Hp.ToString("F0");
    }
}
