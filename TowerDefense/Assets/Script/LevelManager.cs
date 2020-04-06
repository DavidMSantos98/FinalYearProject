using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject [] tilePrefabs;
    
    private Transform TowerTiles;
    private Transform PathTiles;
    private Transform DecorativeTiles;

    public GameObject towerToBePlaced;
    public int towerToBePlacedID;

    public string spawnerX;
    public string spawnerY;
    private string goalX;
    private string goalY;

    [SerializeField]
    private GameObject spawnerGO;
    [SerializeField]
    private GameObject goalGO;
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private Enemy[] enemyOptions;
    [SerializeField]
    private GameObject[] towerOptions;

    private GameObject Canvas;

    [SerializeField]
    private TextMeshProUGUI currencyNotification;
    [SerializeField]
    private float timeForCurrencyNotification;
    private float timeUntilHideCurrencyNotification;

    private bool displayCurrencyNotification;
    Color32 currencyNotificationNoAlpha;
    Color32 currencyNotificationColor;

    private bool showCurrencyNotification;

    [SerializeField]
    private GameObject Enemies;

    [SerializeField]
    private Tower[] towerSO;

    public int mapYSize;
    private int mapXSize;

    [SerializeField]
    private GameObject CurrencyGO;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Awake()
    {
        currencyNotificationNoAlpha=currencyNotification.color;
        currencyNotificationNoAlpha.a = 0;
        
        currencyNotificationColor = currencyNotification.color;

        currencyNotification.color = currencyNotificationNoAlpha;

        TowerTiles = GameObject.Find("TowerTiles").transform;
        PathTiles = GameObject.Find("PathTiles").transform;
        DecorativeTiles = GameObject.Find("DecorativeTiles").transform;

        CreateLevel();
        Canvas = transform.GetChild(0).gameObject;
        displayCurrencyNotification = false;
        timeUntilHideCurrencyNotification = timeForCurrencyNotification;
        //towerToBePlacedID = 0;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            towerToBePlacedID = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            towerToBePlacedID = 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            towerToBePlacedID = 2;
        }

        towerToBePlaced = towerOptions[towerToBePlacedID];
        
        if (displayCurrencyNotification)
        {
            if (timeUntilHideCurrencyNotification > 0) 
            {
                currencyNotification.color = currencyNotificationColor;
                timeUntilHideCurrencyNotification -= Time.deltaTime;
            }
            else 
            {
                currencyNotification.color = currencyNotificationNoAlpha;
                timeUntilHideCurrencyNotification = timeForCurrencyNotification;
                displayCurrencyNotification = false;
            }

        }

    }

    public void changeToMGTower()
    {
        towerToBePlacedID = 0;
    }

    public void changeToCannonTower()
    {
        towerToBePlacedID = 1;
    }

    private void CreateLevel()
    {
        string[] mapData = ReadLevelText();

        mapXSize = mapData[0].ToCharArray().Length;
        mapYSize = mapData.Length;

        int SpawnerAndGoalDataSize = 8;

        Vector3 worldStart=Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        
        //Create level
        for(int y=0; y< mapYSize-1; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for(int x=0; x< mapXSize; x++)
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }

        //Create Border
        for(int y = -2; y < mapYSize+1; y++)
        {
            for(int x=-2; x < mapXSize + 2; x++)
            {
                if(x==-2 || x ==-1 || x== mapXSize || x == mapXSize +1 || y == -2 || y ==-1 || y== mapYSize-1 || y == mapYSize)
                {
                    int decorativeTileIndex = UnityEngine.Random.Range(2, 4);
                    PlaceTile(decorativeTileIndex.ToString(), x, y, worldStart);
                }
            }
        }

        for(int j=0; j < SpawnerAndGoalDataSize; j++)
        {
            char[] newTiles = mapData[mapYSize-1].ToCharArray();
            StateSpawnerAndGoalCoordinates(newTiles[j], j);
        }

        GameObject spawner = Instantiate(spawnerGO);
        spawner.transform.position = FindTileWithCords(PathTiles, int.Parse(spawnerX), int.Parse(spawnerY)).position;

        GameObject goal = Instantiate(goalGO);
        goal.transform.position = FindTileWithCords(PathTiles, int.Parse(goalX), int.Parse(goalY)).position;

    }

    private void StateSpawnerAndGoalCoordinates(char value, int check)
    {
        switch (check)
        {
            case 0:
            case 1:
                spawnerX += value;
                break;

           
            case 2:
            case 3:
                spawnerY += value;
                break;

           
            case 4:
            case 5:
                goalX += value;
                break;
            
            case 6:
            case 7:
                goalY += value;
                break;

        }
    }

    private void PlaceTile(string tileType,int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStart.x + TileSize * x + (TileSize/2), worldStart.y - TileSize * y - (TileSize / 2), 0);
        newTile.GetComponent<Tile>().X = x;
        newTile.GetComponent<Tile>().Y = y;

        switch (newTile.tag)
        {
            case "TowerTile":
                newTile.transform.SetParent(TowerTiles);
                break;

            case "PathTile":
                newTile.transform.SetParent(PathTiles);
                break;

            case "DecorativeTiles":
                newTile.transform.SetParent(DecorativeTiles);
                break;
        }
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    public void PlaceTower(GameObject tile)
    {
        if (Canvas.GetComponent<Currency>().currency > towerSO[towerToBePlacedID].Cost)
        {
            displayCurrencyNotification = true;
            currencyNotification.text = "-"+ towerSO[towerToBePlacedID].Cost;
            
            GameObject newTower = Instantiate(towerToBePlaced);

            newTower.transform.position = tile.transform.position;

            tile.GetComponent<Tile>().OnTopOfTileID = towerToBePlacedID;
            tile.GetComponent<Tile>().TowerOnTopOfTile = newTower;

            Enemies.GetComponent<RecordAchievmentValues>().RecordTowerPlacement(newTower, tile);

        }
        else
        {
            displayCurrencyNotification = true;
            currencyNotification.text = "Not enough life orbs for tower ";
        }

    }

    public void RemoveTower(GameObject tile)
    {
        Destroy(tile.GetComponent<Tile>().TowerOnTopOfTile);
        tile.GetComponent<Tile>().OnTopOfTileID = null;
        //Enemies.GetComponent<RecordAchievmentValues>().RemoveRecordTowerPlacement(tower, tile);
        displayCurrencyNotification = true;
        currencyNotification.text = "+" + towerSO[towerToBePlacedID].SellingPrice;
        CurrencyGO.GetComponent<Currency>().AddCurrency(towerSO[towerToBePlacedID].SellingPrice);
    }

    public Transform FindTileWithCords(Transform parentGO, int x, int y)
    {
        Transform intendedTile=null;
        foreach(Transform tile in parentGO)
        {
            if(tile.GetComponent<Tile>().X==x && tile.GetComponent<Tile>().Y == y)
            {
                intendedTile= tile;
            }
        }
        return intendedTile;
    }
}
