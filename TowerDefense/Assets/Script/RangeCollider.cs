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
    private void Awake()
    {
        firstTarget = true;
        collisionList = new List<Collider2D>();
        parent = transform.parent.gameObject;
        parentTower = parent.GetComponent<TowerManager>().thisTower;
        timeBetweenAttacks = parentTower.AttackRate;
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
                Debug.Log("Tower Attack");
                parent.GetComponent<TowerManager>().TowerAttack(target);
                timeUntilNextAttack = timeBetweenAttacks; 
            }
        }
        else
        {
            float targetDistanceTraveled = float.MaxValue;
            foreach (Collider2D col in collisionList)
            {
                Debug.Log(col.gameObject.name);
                float enemyInstancedistanceTraveled = col.gameObject.GetComponent<EnemyMovement>().CalculateDistanceTraveled();
                if (enemyInstancedistanceTraveled < targetDistanceTraveled)
                {
                    target = col.gameObject;
                    targetDistanceTraveled = target.gameObject.GetComponent<EnemyMovement>().CalculateDistanceTraveled();
                }
            }
        }
       
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    { 
        Debug.Log("Collision");


        if (target != null)//Checks if there is a target already
        {
            if (collisionList.Count!=0)//checks if there are other enemies within the circle
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
        Debug.Log("Existing");
        collisionList.Remove(collision);
        target =null;
    }
}
