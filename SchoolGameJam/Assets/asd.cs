using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Stage", order = 1)]
public class asd : ScriptableObject
{
    public int stageId;
    public int Time;
    public Monster[] monsters;
}


[Serializable]
public struct Monster
{
    public int typeId;
    public int hp;
    public int damage;
}
