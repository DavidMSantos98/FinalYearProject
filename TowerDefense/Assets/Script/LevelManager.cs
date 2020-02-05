﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject [] tilePrefabs;
    
    [SerializeField]
    private Transform TowerTiles;
    [SerializeField]
    private Transform PathTiles;
    
    public GameObject tower;
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
    private Tower[] towerOptions;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Start()
    {
        CreateLevel();
        towerToBePlacedID = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            CreateEnemy(0);
        }

        if (Input.GetKeyDown("1"))
        {
            towerToBePlacedID = 0;
        }
        if (Input.GetKeyDown("2"))
        {
            towerToBePlacedID = 1;
        }
        if (Input.GetKeyDown("3"))
        {
            towerToBePlacedID = 2;
        }
    }

    private void CreateEnemy(int i)
    {
        GameObject Enemy = Instantiate(enemyOptions[i].enemyGO);
        Enemy.transform.position = spawnerGO.transform.position;
    }

    private void CreateLevel()
    {

        string[] mapData = ReadLevelText();

        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        int SpawnerAndGoalDataSize = 8;

        Vector3 worldStart=Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for(int y=0; y< mapYSize-1; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for(int x=0; x< mapXSize; x++)
            {

                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
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

        Debug.Log(spawnerX);
        Debug.Log(spawnerY);

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

        if (newTile.tag == "TowerTile")
        {
            newTile.transform.SetParent(TowerTiles);
        }
        if (newTile.tag == "PathTile")
        {
            newTile.transform.SetParent(PathTiles);
        }

    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    public void PlaceTower(Vector2 towerPosition, GameObject tile)
    {
        GameObject newTower = Instantiate(towerOptions[towerToBePlacedID].tower);
        newTower.transform.position = towerPosition;
        

        tile.GetComponent<Tile>().OnTopOfTileID = towerToBePlacedID;
        tile.GetComponent<Tile>().TowerOnTopOfTile = newTower;
    }

    public void RemoveTower(GameObject tile)
    {
        tile.GetComponent<Tile>().OnTopOfTileID = null;
        //not sure if should also set refernce to tower gameobject to null
        Destroy(tile.GetComponent<Tile>().TowerOnTopOfTile);

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