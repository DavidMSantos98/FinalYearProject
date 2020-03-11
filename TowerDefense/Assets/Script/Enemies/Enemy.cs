using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public float movSpeed;
    public float health;
    public GameObject enemyGO;
    public int id;
    public int damage;

}
