using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : TowerParametersOthers
{
    public float towerHeightOfset;
    public Vector3 towerProjectilePosition;
    private Vector2 projectileInitialPosition;
    private float range;
    public int cost;
    private float sellingPrice;
    
    public float attackRate;
    private float projSpeed;
    private float damage;
    [HideInInspector]
    public int towerId;

    public int towerIndex;
    
    public Tower tower;

    private GameObject targetEnemy;

    private LayerMask enemiesLayer;

    [SerializeField]
    private GameObject projectile;
    private GameObject LevelManager;
    private GameObject Canvas;


    void Awake()
    {
        LevelManager = GameObject.Find("LevelManager");
        enemiesLayer = LayerMask.NameToLayer("Enemy");
        towerIndex = LevelManager.GetComponent<LevelManager>().towerToBePlacedID;

        towerHeightOfset = 1;
        towerId = tower.id;
        damage = tower.Damage;
        projSpeed = tower.projectileSpeed;
        attackRate = tower.AttackRate;
        cost = tower.Cost;
        Canvas = LevelManager.transform.GetChild(0).gameObject;
        Canvas.GetComponent<Currency>().SubtractCurrency(cost);
    }

    public void TowerAttack(GameObject enemy)
    {
        targetEnemy = enemy;
        towerProjectilePosition = new Vector3(0, towerHeightOfset);
        projectileInitialPosition = transform.position + towerProjectilePosition;

        switch (tower.name)
        {
            case "MachineGun":  
                MachineGunAttack();
                break;

            case "Cannon":
                CannonAttack();
                break;

            case "Laser":
                LaserAttack();
                break;
        }
    }

    private void MachineGunAttack()
    {
        GameObject bullet = Instantiate(projectile);
        bullet.transform.position = projectileInitialPosition;
        bullet.GetComponent<HomingBullet>().SetValues(targetEnemy, projSpeed, damage);
    }
    private void CannonAttack()
    {
        GameObject cannonBall = Instantiate(projectile);
        cannonBall.transform.position = projectileInitialPosition;
        cannonBall.GetComponent<CannonProjectile>().SetValues(targetEnemy, projSpeed, damage);
    }

    private void LaserAttack()
    {
        targetEnemy.GetComponent<EnemyCombat>().TakeDamage(damage);
    }
}
