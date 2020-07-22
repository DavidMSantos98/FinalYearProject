using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TowerStateMachine : MonoBehaviour
{
    [SerializeField]
    private Sprite[] towerDirections;

    private SpriteRenderer towerSpriteRenderer;

    [SerializeField]
    private float directionOffSet;

    int[,] directionCategories;

    private GameObject newTarget;

    private Vector2 newDirection;
    
    private int newDirectionCategorised;
    private int? oldDirectionCategorised;


    enum Vertical
    {
        neutral=0,
        up=1,
        down=2
    }

    enum Horizontal
    {
        neutral=0,
        left=1,
        right=2
    }


    void Start()
    {
        oldDirectionCategorised = null;

        towerSpriteRenderer = GetComponent<SpriteRenderer>();
        
        directionCategories = new int[3,3];

        matchSpriteArrayIndexWithDirections();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void matchSpriteArrayIndexWithDirections()
    {
        //you can not have a tower direction with neutral vertical and neutral horizontal direction - [0,0]
        directionCategories[0, 1] = 0; //looking left 
        directionCategories[0, 2] = 1; //looking right

        directionCategories[1, 0] = 2; //looking up
        directionCategories[1, 1] = 3; //looking up and left
        directionCategories[1, 2] = 4; //looking up and right

        directionCategories[2, 0] = 5; //looking down
        directionCategories[2, 1] = 6; //looking down and left
        directionCategories[2, 2] = 7; //looking down and right
    }

    public void GetDirectionVectorBetweenTowerAndTarget(GameObject target)
    {
        Vector2 betweenTowerAndTarget = target.transform.position - gameObject.GetComponent<TowerManager>().towerProjectilePosition;
        CheckIfDirectionChanged(betweenTowerAndTarget.normalized);
    }

    public void CheckIfDirectionChanged(Vector2 direction)
    {
        newDirection = direction;
        DefineNewDirectionCategory();
        
            if (oldDirectionCategorised != newDirectionCategorised)
            {
                towerSpriteRenderer.sprite = towerDirections[newDirectionCategorised];
            }            

        oldDirectionCategorised = newDirectionCategorised;
    }


    private void DefineNewDirectionCategory()
    {        
        Vertical verticalDirection;

        if (newDirection.y > directionOffSet)
        {
            verticalDirection = Vertical.up;
        }
        else
        {
            if (newDirection.y < -directionOffSet)
            {
                verticalDirection = Vertical.down;
            }
            else
            {
                verticalDirection = Vertical.neutral;
            }
        }

        Horizontal horizontalDirection;

        if (newDirection.x > directionOffSet)
        {
            horizontalDirection = Horizontal.right;
        }
        else
        {
            if (newDirection.x <-directionOffSet)
            {
                horizontalDirection = Horizontal.left;
            }
            else
            {
                horizontalDirection = Horizontal.neutral;
            }
        }

        try
        {
            newDirectionCategorised = directionCategories[(int)verticalDirection, (int)horizontalDirection];
        }
        catch(InvalidCastException e)
        {
            Debug.Log("Error, neutral horizontal and vertical tower direction");
        }
    }
}