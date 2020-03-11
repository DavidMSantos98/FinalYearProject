using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    public Slider healthbar;

    [SerializeField]
    private Enemy enemyInQuestion;
    
    private float health;
    private int damage;
    private int id;
    private Rigidbody2D thisRB;

    private GameObject currencyCanvas;
    void Awake()
    {
        id = enemyInQuestion.id;
        damage = enemyInQuestion.damage;
        health = enemyInQuestion.health;
        thisRB = GetComponent<Rigidbody2D>();
        thisRB.useFullKinematicContacts = true;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Goal"))
        {
            collision.gameObject.GetComponent<RecordDamage>().EnemyReachedGoal(id, damage);
            Destroy(gameObject);
        }
    }
}
