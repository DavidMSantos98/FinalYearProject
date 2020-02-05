using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMouseOver : MonoBehaviour
{
    private float alphaValueWhenMouseIsOverTile;
    Color tileColor;
    Color highlightedColor;
    public GameObject LevelManager;
    private bool mouseOver = false;

    
    void Start()
    {
        alphaValueWhenMouseIsOverTile = 0.5f;
        tileColor = gameObject.GetComponent<SpriteRenderer>().color;
        highlightedColor = tileColor;
        highlightedColor.a = alphaValueWhenMouseIsOverTile;
    }

    void Update()
    {
        if (mouseOver)
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnRightClickMouseDown();
            }
        }
    }


    void OnRightClickMouseDown()
    {
        if (gameObject.tag == "TowerTile")
        {
            if (gameObject.GetComponent<Tile>().OnTopOfTileID != null)
            {
                LevelManager.GetComponent<LevelManager>().RemoveTower(gameObject);
            }
            else
            {
                Debug.Log("There is no tower on this tile");
            }
        }
    }

    void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = highlightedColor;
        mouseOver = true;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = tileColor;
        mouseOver = false;
    }

    void OnMouseDown()
    {
        if (gameObject.tag == "TowerTile")
        {
            if (gameObject.GetComponent<Tile>().OnTopOfTileID==null)
            {
                LevelManager.GetComponent<LevelManager>().PlaceTower(transform.position, gameObject);
                gameObject.GetComponent<Tile>().OnTopOfTileID = LevelManager.GetComponent<LevelManager>().towerToBePlacedID;
                Debug.Log(gameObject.GetComponent<Tile>().OnTopOfTileID);
            }
            else
            {
                Debug.Log("Tile already has a tower in it");
            }
        }
        else
        {
            Debug.Log("Not a tower tile");
        }
    }

}
