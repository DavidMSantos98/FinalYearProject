using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfMouseOver : MonoBehaviour
{
    private float alphaValueWhenMouseIsOverTile;
    Color tileColor;
    Color highlightedColor;
    Color currentColor;
    private GameObject LevelManager;
    private bool mouseOver;


    
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
        alphaValueWhenMouseIsOverTile = 0.5f;
        tileColor = gameObject.GetComponent<SpriteRenderer>().color;
        highlightedColor = tileColor;
        highlightedColor.a = alphaValueWhenMouseIsOverTile;
        currentColor=tileColor;
    }

   
    public void ChangeTileAlpha()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color == tileColor)
        {
            currentColor = highlightedColor;
        }
        if (gameObject.GetComponent<SpriteRenderer>().color == highlightedColor)
        {
            currentColor = tileColor;
        }
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;
    }
}

