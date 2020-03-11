using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    private string[] waveString;

    private int[] currentWaveEnemies;
    private int[] currentWaveTimes;

    private float timeUntilSpawning;

    private int enemyIndex;

    private int waveXSize;

    private Vector3 goalPosition;

    private bool firstFrame;

    private int numberOfWaves;
    void Start()
    {
        enemyIndex = 0;
        numberOfWaves = CountNumberOfWaves();
        waveString =ReadWavesText("Wave1");
        SetEnemiesAndTime(waveString);
        timeUntilSpawning = currentWaveTimes[enemyIndex];
        firstFrame = true;
        goalPosition = GameObject.Find("SpawnPoint(Clone)").transform.position;
    }
    void Update()
    {
        if (firstFrame)
        {

            firstFrame = false;
        }

        if (enemyIndex< waveXSize-1)
        {
            if (timeUntilSpawning <= 0)
            {
                GameObject prefab = Instantiate(enemies[currentWaveEnemies[enemyIndex]], goalPosition,transform.rotation) as GameObject;
                enemyIndex++;
                timeUntilSpawning = currentWaveTimes[enemyIndex];
            }
            else
            {
                timeUntilSpawning -= Time.deltaTime;
            }
        }
        if (enemyIndex == waveXSize - 1) 
        {

        }
    }

    int CountNumberOfWaves()
    {
        UnityEngine.Object[] waves = Resources.LoadAll(Application.dataPath+"/resources");
        return waves.Length - 1;
    }

    string[] ReadWavesText(string waveFileName)
    {
        TextAsset wave = Resources.Load(waveFileName) as TextAsset;
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

        for (int i = 0; i < waveData.Length; i++)
        {
            for(int j = 0; j < waveXSize; j++)
            {
                if (i== 0)
                {
                    enemyString = waveData[i][j];
                    currentWaveEnemies[j] = (int)Char.GetNumericValue(enemyString);
                }
                if (i != 0)
                {
                    timeString = waveData[i][j];
                    currentWaveTimes[j] = (int)Char.GetNumericValue(timeString);
                }
            }
        }
    }
}
