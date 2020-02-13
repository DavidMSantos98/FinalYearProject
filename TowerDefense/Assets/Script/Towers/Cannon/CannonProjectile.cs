using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{

    public float laserDamageRange;
    public float CannonDamageRange;
    public float CannonCollateralDamage;

    private GameObject target;
    private float speed;
    private float damage;

    private Vector2 bulletPosition;
    private Vector2 targetPosition;
    private bool targetHasDied;
    private Vector2 lastRecordedTargetPosition;

    private Rigidbody2D cannonRB;
    private GameObject cannonBallAoE;

    private LayerMask enemiesLayer;
    private bool continueProjectileTranslation;
    private float distanceForExplosion;

    void Start()
    {
        laserDamageRange = 5;
        CannonDamageRange = 10f;
        CannonCollateralDamage = 2;
        enemiesLayer = LayerMask.NameToLayer("Enemy");
        cannonRB = GetComponent<Rigidbody2D>();
        targetHasDied = false;
        targetPosition = target.transform.position;
        distanceForExplosion = 0.5f;
    }

    void FixedUpdate()
    {

        if (target == null && continueProjectileTranslation==false)
        {
            continueProjectileTranslation = false;
        }
        else
        {
            continueProjectileTranslation = true;
        }

        if (continueProjectileTranslation == true)
        {
            if (target != null)
            {
                targetPosition = target.transform.position;
            }
            else
            {
                targetPosition = lastRecordedTargetPosition;
            }

            bulletPosition = transform.position;

            Vector2 directionalVector = targetPosition - bulletPosition;
            directionalVector.Normalize();

            cannonRB.velocity = directionalVector * speed;

            if ((bulletPosition - targetPosition).magnitude <= distanceForExplosion)
            {
                DealAoEDamage();
            }
            lastRecordedTargetPosition = targetPosition;
        }

    }

    private void DealAoEDamage()
    {
        Debug.Log("Bomb Arrived Destination");
        if (target != null)
        {
            target.GetComponent<EnemyCombat>().TakeDamage(damage);
        }

        Debug.Log(CannonDamageRange + " is cannon damage range");
        Collider2D[] enemiesNearby = Physics2D.OverlapCircleAll(transform.position, CannonDamageRange);
        foreach( Collider2D enemyCol in enemiesNearby)
        {
            if (enemyCol.gameObject.tag == "Enemy")
            {
                enemyCol.gameObject.GetComponent<EnemyCombat>().TakeDamage(CannonCollateralDamage);
            }
        }
        Destroy(gameObject);
    }

    public void SetValues(GameObject targetEnemy, float projSpeed, float projDamage)
    {
        target = targetEnemy;
        speed = projSpeed;
        damage = projDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            DealAoEDamage();
        }
    }
}
