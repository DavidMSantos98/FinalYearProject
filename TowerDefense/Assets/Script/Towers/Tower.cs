using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Tower", menuName ="Tower")]
public class Tower : ScriptableObject
{
    public float Damage;
    public float Range;
    public float AttackRate;
    public int Cost;
    public int SellingPrice;
    public GameObject tower;
    //public RuntimeAnimatorController towerAnimatorController;

}
