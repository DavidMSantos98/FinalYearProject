using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject EnemiesHolder;

    [SerializeField]
    private GameObject PlayerRecords;

    private string[] waveString;

    private int[] currentWaveEnemies;
    private int[] currentWaveTimes;

    private float timeUntilSpawning;

    private int enemyIndex;

    private int waveXSize;

    private Vector3 goalPosition;

    public int numberOfWaves;
    public int? waveCounter;
    
    [SerializeField]
    private float newWaveDisplayTime;
    private float durationNewWaveDisplay;

    [SerializeField]
    private GameObject waveDisplayGO;
    [SerializeField]
    private TextMeshProUGUI waveDisplay;

    Color32 newAlpha;

    [SerializeField]
    private float percentageOfTimeIncreasingAlpha, percentageOfTimeDecreasingAlpha;
    private float timeIncreaseAlpha;
    private float timeDecreaseAlpha;

    [SerializeField]
    private float rateOfAlphaIncrease, rateOfAlphaDecrease;

    private float floatValueOfAlpha;

    private bool allEnemiesOfCurrentWaveHaveBeenSpawned;

    private bool startingNextWave;

    private bool valuesEstablished;

    private void Awake()
    {
        numberOfWaves = CountNumberOfWaves();
    }

    void Start()
    {
        goalPosition = GameObject.Find("SpawnPoint(Clone)").transform.position;

        startingNextWave = true;
        durationNewWaveDisplay = newWaveDisplayTime;

        if (percentageOfTimeIncreasingAlpha > 100) { percentageOfTimeIncreasingAlpha = 100; }
        if (percentageOfTimeDecreasingAlpha > 100) { percentageOfTimeDecreasingAlpha = 100; }

        if (percentageOfTimeIncreasingAlpha <0) { percentageOfTimeIncreasingAlpha = 0; }
        if (percentageOfTimeDecreasingAlpha < 0) { percentageOfTimeDecreasingAlpha = 0; }

        timeIncreaseAlpha = newWaveDisplayTime - (newWaveDisplayTime * (percentageOfTimeIncreasingAlpha / 100));
        timeDecreaseAlpha = newWaveDisplayTime * (percentageOfTimeDecreasingAlpha / 100);

        //waveDisplay = waveDisplayGO.GetComponent<TextMeshProUGUI>();
            
        newAlpha = waveDisplay.color;
        newAlpha.a = 0;
        waveDisplay.color = newAlpha;

        allEnemiesOfCurrentWaveHaveBeenSpawned = false;

        valuesEstablished = false;

        waveCounter = 1;
    }
    void Update()
    {
        //startingNextWave = true;
        //numberOfWaves = 3;
        //waveCounter = 1;
        if (startingNextWave)
        {
            if (waveCounter<= numberOfWaves)
            {
                waveDisplay.text = "Wave " + waveCounter.ToString();
                DisplayWaveCounter();
            }
            else
            {
                //game completed
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }

        }else
        {

            if (!valuesEstablished)
            {
                SetValuesForNewWave();
                valuesEstablished = true;
            }

            if (enemyIndex < waveXSize)
            {
                if (timeUntilSpawning <= 0)
                {
                    GameObject prefab = Instantiate(enemies[currentWaveEnemies[enemyIndex]], goalPosition, transform.rotation) as GameObject;
                    prefab.transform.parent = EnemiesHolder.transform;
                   
                    if(enemyIndex== currentWaveEnemies.Length - 1)
                    {
                        allEnemiesOfCurrentWaveHaveBeenSpawned = true;
                    }
                    enemyIndex++;

                    if (enemyIndex <= currentWaveEnemies.Length-1)
                    {
                        timeUntilSpawning = currentWaveTimes[enemyIndex];
                    }
                    
                    
                }
                else
                {
                    timeUntilSpawning -= Time.deltaTime;
                }
            }

            if (allEnemiesOfCurrentWaveHaveBeenSpawned && EnemiesHolder.transform.childCount == 0)
            {
                NextWave();
            }
        }
    }

    void NextWave()
    {
        int wave = (int)waveCounter;
        PlayerRecords.GetComponent<RecordAchievmentValues>().RecordEndOfWave(wave);
        startingNextWave = true;
        allEnemiesOfCurrentWaveHaveBeenSpawned = false;
        valuesEstablished = false;
        enemyIndex = 0;
        waveCounter++;
    }

    void DisplayWaveCounter()
    {
        if (durationNewWaveDisplay <= 0)
        {
            startingNextWave = false;


            if (waveDisplay.color.a != 0)
            {

                newAlpha.a = 0;
                waveDisplay.color = newAlpha;
            }

            durationNewWaveDisplay = newWaveDisplayTime;
        }
        else
        {

            floatValueOfAlpha = Convert.ToSingle(newAlpha.a);

            if (durationNewWaveDisplay> timeIncreaseAlpha)
            {
                //increase alpha
                if (floatValueOfAlpha + rateOfAlphaIncrease <= 255)
                {
                    
                    floatValueOfAlpha += rateOfAlphaIncrease;
                }
                else
                {
                    floatValueOfAlpha = 255;
                }

            }
            if(durationNewWaveDisplay< timeDecreaseAlpha)
            {
                //decrease alpha
                if (floatValueOfAlpha - rateOfAlphaDecrease >= 0)
                {
                    
                    floatValueOfAlpha -= rateOfAlphaDecrease;
                }
                else
                {
                    floatValueOfAlpha = 0;
                }
            }

            newAlpha.a = Convert.ToByte(floatValueOfAlpha);
            waveDisplay.color = newAlpha;

            durationNewWaveDisplay -= Time.deltaTime;
        }
    }

    void SetValuesForNewWave()
    {
        enemyIndex = 0;
        //waveCounter = waveNumber;
        waveString = ReadWavesText("Wave" + waveCounter.ToString());
        SetEnemiesAndTime(waveString);
        timeUntilSpawning = currentWaveTimes[enemyIndex];
    }



    string[] ReadWavesText(string waveFileName)
    {
        TextAsset wave = Resources.Load("Waves/" + waveFileName) as TextAsset;
        string data = wave.text.Replace(Environment.NewLine, string.Empty);
        return data.Split('-');
    }

    void SetEnemiesAndTime(string[] waveData)
    {
        char enemyString;
        char timeString;

        waveXSize = waveData[0].ToCharArray().Length;

        currentWaveEnemies = new int[waveXSize];
        currentWaveTimes = new int[waveXSize];

        for (int i = 0; i < 2; i++)
        {
            for(int j = 0; j < waveXSize; j++)
            {
                if (i== 0)
                {
                    enemyString = waveData[i][j];
                    currentWaveEnemies[j] = (int)Char.GetNumericValue(enemyString);
                }
                if (i == 1)
                {
                    timeString = waveData[i][j];
                    currentWaveTimes[j] = (int)Char.GetNumericValue(timeString);
                }
            }
        }
    }

    int CountNumberOfWaves()
    {
        object[] waves = Resources.LoadAll("Waves");
        return waves.Length;
    }
}
