using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    public Slider healthbar;

    [SerializeField]
    private Enemy[] enemyOptions;

    public Enemy enemyInQuestion;
    private float health;
    void Awake()
    {
        int index = 0;
        enemyInQuestion = enemyOptions[index];
        health = enemyInQuestion.health;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health; ;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) 
    {
        Debug.Log("Damage Taken");
        health -= damage;
    }
}
