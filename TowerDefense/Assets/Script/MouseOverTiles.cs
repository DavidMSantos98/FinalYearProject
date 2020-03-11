using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverTiles : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject targetTile;
    private GameObject previousTile;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            for (int i = 0; i < hit.Length; i++)
            {

                if (hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Tiles"))
                {
                    targetTile = hit[i].collider.gameObject;
                    break;
                }
            }

        if (targetTile != null)
        {
            if (previousTile == null)
            {
                targetTile.GetComponent<CheckIfMouseOver>().ChangeTileAlpha();
            }
            else
            {
                if (targetTile != previousTile)
                {
                    targetTile.GetComponent<CheckIfMouseOver>().ChangeTileAlpha();
                    previousTile.GetComponent<CheckIfMouseOver>().ChangeTileAlpha();
                }
            }
            previousTile = targetTile;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (targetTile.tag == "TowerTile")
            {
                if (!GetComponent<PauseGame>().GameisPaused)
                {
                    GetComponent<LevelManager>().PlaceTower(targetTile.transform.position, targetTile);
                }
                
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (targetTile.tag == "TowerTile")
            {
                if (targetTile.GetComponent<Tile>().OnTopOfTileID != null)
                {
                    if (!GetComponent<PauseGame>().GameisPaused)
                    {
                        GetComponent<LevelManager>().RemoveTower(targetTile);
                    }
                    
                }
            }
        }

    }
}
