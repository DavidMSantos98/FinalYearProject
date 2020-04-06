using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreatePlayerDefeatMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemies;

    [SerializeField]
    private GameObject LevelManager;

    [SerializeField]
    private GameObject WaveManager;

    [SerializeField]
    private GameObject Currency;
    private int waveCount;

    [SerializeField]
    private TMPro.TMP_Dropdown waveDropdown;

    
    List<string> availableWaves;

    private string waveOptionString;
    private char waveNumberCharacter;
    private int waveNumber;
    private string[] waveStringArray;

    [SerializeField]
    private GameObject TemporaryHolderTowers;


    void Start()
    {
        
        availableWaves = new List<string>();

        waveCount = WaveManager.GetComponent<WaveManager>().numberOfWaves;
        waveStringArray = new string[waveCount];
        for (int i = 0; i < waveCount; i++)
        {
            waveStringArray[i]=("Wave " + (i+1).ToString());
        }

        foreach (string waveString in waveStringArray)
        {
            availableWaves.Add(waveString);
        }

        waveDropdown.ClearOptions();
        waveDropdown.AddOptions(availableWaves);
    }

    // Update is called once per frame
    void Update()
    {
        waveOptionString = waveDropdown.options[waveDropdown.value].text;
        waveNumberCharacter = waveOptionString[waveOptionString.Length - 1];
        double waveNumberDouble = Char.GetNumericValue(waveNumberCharacter);
        waveNumber = (int)waveNumberDouble-1;


    }

    public void PressRestartButton()
    {
        LevelManager.GetComponent<PauseGame>().Resume();
        Currency.GetComponent<Currency>().currency=Currency.GetComponent<Currency>().initialCurrencyValue;
        WaveManager.GetComponent<WaveManager>().waveCounter = waveNumber;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ReInstantiateTowers(Enemies.GetComponent<RecordAchievmentValues>().allTowersGOPlacedEachRound, Enemies.GetComponent<RecordAchievmentValues>().allTowersTileslacedEachRound);
    }

    private void ReInstantiateTowers(ArrayList[] allTowersGOPlacedEachRound, ArrayList[] allTowersTileslacedEachRound)
    {
        ArrayList towersGOToInstantiate = allTowersGOPlacedEachRound[waveNumber];
        ArrayList towersTileToInstantiate = allTowersTileslacedEachRound[waveNumber];

        Debug.Log(towersGOToInstantiate.Count);

        for (int i=0; i< towersGOToInstantiate.Count; i++)
        {
            GameObject newTower = Instantiate(towersGOToInstantiate[i] as GameObject);

            GameObject newTile = towersTileToInstantiate[i] as GameObject;
            newTower.transform.position = newTile.transform.position;

            newTile.GetComponent<Tile>().OnTopOfTileID = newTower.GetComponent<Tower>().id;
            newTile.GetComponent<Tile>().TowerOnTopOfTile = newTower;
        }
    }
}
