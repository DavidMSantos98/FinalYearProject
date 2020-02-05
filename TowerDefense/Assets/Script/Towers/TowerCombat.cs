using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCombat : MonoBehaviour
{
    // Start is called before the first frame update

    Tower towerInQuestion;
    private float timeUntilNextAttack;
    private float timeBetweenEachAttack;
    void Start()
    {
        towerInQuestion = GetComponent<Tower>();
        timeBetweenEachAttack = towerInQuestion.AttackRate;
        timeUntilNextAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamageToTarget(GameObject enemy)
    {
        if (timeUntilNextAttack <= 0)
        {
        }
        else
        {
            timeUntilNextAttack -= Time.deltaTime;
        }
        
    }
}
