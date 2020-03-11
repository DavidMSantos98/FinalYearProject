using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCollider : MonoBehaviour
{
    private GameObject parent;
    private Tower parentTower;
    private float timeBetweenAttacks;
    private float timeUntilNextAttack;
    private GameObject target;
    private List<Collider2D> collisionList;
    private bool firstTarget;
    private bool towerCanAttack;

    private Vector2 directionToEnemy;
    private void Awake()
    {
        firstTarget = true;
        collisionList = new List<Collider2D>();
        parent = transform.parent.gameObject;
        //parentTower = parent.GetComponent<TowerManager>().tower;
        timeBetweenAttacks = parent.GetComponent<TowerManager>().attackRate;
        timeUntilNextAttack = 0;
    }

    private void Update()
    {
        if (timeUntilNextAttack <= 0)
        {
            towerCanAttack = true;
        }
        else
        {
            towerCanAttack = false;
            timeUntilNextAttack -= Time.deltaTime;
        }

        if (target != null)//if there is a target
        {
            if (towerCanAttack)//attack target
            {
                parent.GetComponent<TowerManager>().TowerAttack(target);
                timeUntilNextAttack = timeBetweenAttacks;
            }
        }
        else
        {
            float targetDistanceTraveled = float.MaxValue;
            foreach (Collider2D col in collisionList)
            {
                float enemyInstancedistanceTraveled = col.gameObject.GetComponent<EnemyMovement>().CalculateDistanceTraveled();
                if (enemyInstancedistanceTraveled < targetDistanceTraveled)
                {
                    target = col.gameObject;
                    targetDistanceTraveled = target.gameObject.GetComponent<EnemyMovement>().CalculateDistanceTraveled();
                    directionToEnemy = (target.transform.position - transform.position).normalized;//
                }
            }

            
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            parent.GetComponent<TowerStateMachine>().CheckIfDirectionChanged(CalculateEnemyDirection());
        }
            

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != null)//Checks if there is a target already
        {
            if (collisionList.Count != 0)//checks if there are other enemies within the circle
            {
                target = collision.gameObject;//choses the enemy that has entered the circle has the new target
            }

        }


        if (!collisionList.Contains(collision))
        {
            collisionList.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionList.Remove(collision);
        target = null;
    }

    private Vector2 CalculateEnemyDirection()
    {
        Vector2 direction = target.transform.position - transform.position; //+ offset
        return direction.normalized;
    }
}