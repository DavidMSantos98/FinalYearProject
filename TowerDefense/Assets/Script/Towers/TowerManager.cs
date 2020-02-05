using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : TowerParametersOthers
{
    private float range;
    private float attackRate;
    private int cost;
    private float sellingPrice;
    private float projSpeed;
    private float damage;

    public int towerIndex;
    [SerializeField]
    private Tower[] tower;

    [HideInInspector]
    public Tower thisTower;
    private GameObject targetEnemy;

    private LayerMask enemiesLayer;

    [SerializeField]
    private GameObject mgBullet;
    [SerializeField]
    private GameObject LevelManager;



    void Awake()
    {
        enemiesLayer = LayerMask.NameToLayer("Enemy");
        towerIndex = LevelManager.GetComponent<LevelManager>().towerToBePlacedID;
        thisTower = tower[towerIndex];
        damage = thisTower.Damage;
        projSpeed = thisTower.projectileSpeed;
    }

    public void TowerAttack(GameObject enemy)
    {
        targetEnemy = enemy;

        switch (towerIndex)
        {
            case 0:
                MachineGunAttack();
                break;

            case 1:
                CannonAttack();
                break;

            case 2:
                LaserAttack();
                break;
        }
    }

    private void MachineGunAttack()
    {
        GameObject bullet = Instantiate(mgBullet);
        bullet.transform.position = transform.position;//+offset
        bullet.GetComponent<HomingBullet>().SetValues(targetEnemy, projSpeed, damage);
    }
    private void CannonAttack()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(targetEnemy.transform.position, CannonDamageRange, enemiesLayer);
        foreach(Collider2D enemyCol in enemyColliders)
        {
            targetEnemy.GetComponent<EnemyCombat>().TakeDamage(CannonCollateralDamage);
        }
        targetEnemy.GetComponent<EnemyCombat>().TakeDamage(damage);
    }

    private void LaserAttack()
    {

        targetEnemy.GetComponent<EnemyCombat>().TakeDamage(damage);
    }
}
