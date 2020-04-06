using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetails : MonoBehaviour
{
    public GameObject TowerGO { get; set; }
    public GameObject Tile { get; set; }
    public TowerDetails(GameObject tower, GameObject tile)
    {
        TowerGO = tower;
        Tile = tile;
    }

}
