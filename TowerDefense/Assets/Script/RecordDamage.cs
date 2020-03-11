using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordDamage : MonoBehaviour
{
    private GameObject currencyCanvas;

    [SerializeField]
    private Enemy[] enemies;

    private int[,] enemiesArray;

    private void Start()
    {
        currencyCanvas = GameObject.Find("CurrencyCanvas");
        enemiesArray = new int[enemies.Length,2];
        SetEnemyArrayID();
    }

    void SetEnemyArrayID()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemiesArray[i,0] = i;
        }
    }

    public void EnemyReachedGoal(int id, int damage)
    {
        for (int i = 0; i < enemiesArray.GetLength(0); i++)
        {
            if(enemiesArray[i, 0]== id)
            {
                enemiesArray[i, 1] += damage;
            }
        }

        currencyCanvas.GetComponent<Currency>().SubtractCurrency(damage);
    }
}
