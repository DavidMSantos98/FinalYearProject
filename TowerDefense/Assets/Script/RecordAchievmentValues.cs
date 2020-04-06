using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordAchievmentValues : MonoBehaviour
{

    public int[] enemiesADefeatedPerWave;
    public int[] enemiesBDefeatedPerWave;

    public int numberOfEnemyADefeated;
    public int numberOfEnemyBDefeated;

    public int numberOfEnemyADefeatedThisWave;
    public int numberOfEnemyBDefeatedThisWave;

    public int numberOfTowerAPlaced;
    public int numberOfTowerBPlaced;

    private ArrayList allTowersGOPlacedThisRound;
    private ArrayList allTowersTileslacedThisRound;

    public ArrayList[] allTowersGOPlacedEachRound;
    public ArrayList[] allTowersTileslacedEachRound;

    [SerializeField]
    private GameObject WaveManager;

    [SerializeField]
    private GameObject TowerTiles;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        int totalWaveCountIndex = WaveManager.GetComponent<WaveManager>().numberOfWaves;
        enemiesADefeatedPerWave = new int[totalWaveCountIndex];
        enemiesBDefeatedPerWave = new int[totalWaveCountIndex];

        numberOfEnemyADefeated = 0;
        numberOfEnemyBDefeated = 0;

        numberOfEnemyADefeatedThisWave = 0;
        numberOfEnemyBDefeatedThisWave = 0;


        allTowersGOPlacedThisRound = new ArrayList();
        allTowersTileslacedThisRound = new ArrayList();


        allTowersGOPlacedEachRound = new ArrayList[totalWaveCountIndex];
        allTowersTileslacedEachRound = new ArrayList[totalWaveCountIndex];

        numberOfTowerAPlaced = 0;
        numberOfTowerBPlaced = 0;

        Time.timeScale=1;
    }

    public void RecordEnemyDefeat(int id)
    {
      
        if (id == 0) { numberOfEnemyADefeated++; }
        if (id == 1) { numberOfEnemyBDefeated++; }
    }

    public void RecordTowerPlacement(GameObject tower,GameObject tile)
    {
        if (tower.GetComponent<TowerManager>().towerId==0)
        {
            numberOfTowerAPlaced++;
        }
        if(tower.GetComponent<TowerManager>().towerId == 1)
        {
            numberOfTowerBPlaced++;
        }

        allTowersGOPlacedThisRound.Add(tower);
        allTowersTileslacedThisRound.Add(tile);
    }

    public void RemoveRecordTowerPlacement(GameObject tower, GameObject tile)
    {
        foreach (GameObject existingTower in allTowersGOPlacedThisRound)
        {
            if(existingTower == tower)
            {
                allTowersGOPlacedThisRound.Remove(existingTower);
                break;
            }  
        }

        foreach (GameObject towerTile in allTowersTileslacedThisRound)
        {
            if (towerTile == tile)
            {
                allTowersTileslacedThisRound.Remove(towerTile);
                break;
            }
        }
    }
                                     
    public void RecordEndOfWave(int wave)
    {
        ResetValues();
    }

    private void ResetValues()
    {
        numberOfEnemyADefeatedThisWave = 0;
        numberOfEnemyBDefeatedThisWave = 0;

        allTowersGOPlacedThisRound.Clear();
        allTowersTileslacedThisRound.Clear();
    }
    
}
