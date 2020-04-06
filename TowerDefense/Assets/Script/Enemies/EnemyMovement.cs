using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int[] enemyMovementDirections;
    private int east = 0;
    private int south= 1;
    private int west = 2;
    private int north = 3;

    private int nextDirection;
    private int index=0;



    [SerializeField]
    private Enemy[] enemyOptions;

    private Transform currentTile;


    //private GameObject LevelManager;
    private GameObject lm;
    private Transform pt;
    [SerializeField]
    private Transform Goal;

    private float initializationTime;
    public float timeAlive;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        initializationTime = Time.time;
        //the array would be done using the patfinding algorithm

        enemyMovementDirections = new int[] { east,
                                            south, south, south, south, south, south,
                                            east, east,
                                            north, north, north, north, north, north,
                                            east, east,
                                            south, south, south, south, south, south,
                                            east, east,
                                            north, north, north, north, north, north,
                                            east, east, east,
                                            south, south, south, south, south, south,
                                            east, east,
                                            north, north, north,
                                            east, east,
                                            south, south, south, south, south, south,
                                            west, west, west, west, west, west, west, west, west, west, west, west, west, west};

        lm = GameObject.Find("LevelManager");
        pt = GameObject.Find("PathTiles").transform;
        int xvalue = int.Parse(lm.GetComponent<LevelManager>().spawnerX);
        int yvalue = int.Parse(lm.GetComponent<LevelManager>().spawnerY);
        currentTile = lm.GetComponent<LevelManager>().FindTileWithCords(pt, xvalue, yvalue);
        speed = enemyOptions[0].movSpeed;

    }

    void Update()
    {
        timeAlive = Time.time - initializationTime;

        if (CheckIfArrivedTile(transform, currentTile))
        {
                int x = 0;
                int y = 0;
            if (index!= enemyMovementDirections.Length)
            {
                switch (enemyMovementDirections[index])
                {
                    case 0:
                        x = currentTile.GetComponent<Tile>().X + 1;
                        y = currentTile.GetComponent<Tile>().Y;
                        break;

                    case 1:
                        x = currentTile.GetComponent<Tile>().X;
                        y = currentTile.GetComponent<Tile>().Y + 1;
                        break;

                    case 2:
                        x = currentTile.GetComponent<Tile>().X - 1;
                        y = currentTile.GetComponent<Tile>().Y;
                        break;

                    case 3:
                        x = currentTile.GetComponent<Tile>().X;
                        y = currentTile.GetComponent<Tile>().Y - 1;
                        break;
                }
                currentTile = lm.GetComponent<LevelManager>().FindTileWithCords(pt, x, y);
                index++;
            }
        }
            transform.position = Vector2.MoveTowards(transform.position, currentTile.position, speed * Time.deltaTime);
    }


    private bool CheckIfArrivedTile(Transform enemy, Transform tile)
    {
        if (enemy.position == tile.position)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float CalculateDistanceTraveled()
    {
        float distanceTraveled = speed * timeAlive;
        return distanceTraveled;
    }

}
