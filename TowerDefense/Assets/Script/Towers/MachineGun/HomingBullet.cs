using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private GameObject target;
    private float speed;
    private float damage;

    private Vector2 bulletPosition;
    private Vector2 targetPosition;
    private bool targetHasDied;
    private Vector2 lastRecordedTargetPosition;

    private Rigidbody2D bulletRB;
    private float maxTimeAlive = 2;
    
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        targetHasDied = false;
        targetPosition = target.transform.position;
    }

    void FixedUpdate()
    {
        if (maxTimeAlive <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            maxTimeAlive -= Time.deltaTime;
        }


        if (target == null)
        {
            targetHasDied = true;
            lastRecordedTargetPosition = targetPosition;
        }

        if (!targetHasDied)
        {
            targetPosition = target.transform.position;
        }
        else
        {
            targetPosition = lastRecordedTargetPosition;
        }

        bulletPosition = transform.position;

        Vector2 directionalVector = targetPosition- bulletPosition;
        directionalVector.Normalize();

        bulletRB.velocity = directionalVector * speed;

        if(bulletPosition == targetPosition)
        {
           Destroy(gameObject);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<EnemyCombat>().TakeDamage(damage);
        Destroy(gameObject);
    }

    public void SetValues(GameObject targetEnemy,float projSpeed, float projDamage)
    {
        target = targetEnemy;
        speed = projSpeed;
        damage = projDamage;
    }
}
