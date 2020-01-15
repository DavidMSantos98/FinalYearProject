using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTIpScript : MonoBehaviour
{
    public LayerMask stickableObjects;
    public bool tipIsStuck;
    private bool previousTipIsStuck;
    private Vector2 positionOffset;
    private Vector2 playerPosition;
    public GameObject player;
    private Rigidbody2D ST_RB;
    public float ropeRange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tipIsStuck = true;
        player.GetComponent<HookShot>().spearTipIsStuck = true;
    }

    private void Start()
    {
        ST_RB = GetComponent<Rigidbody2D>();
        tipIsStuck =false;
        positionOffset = new Vector2(0.91f, 0.1f);
    }
    private void Update()
    {
        if (player.GetComponent<HookShot>().STDetatched)
        {
            transform.SetParent(null, true);
        }

        if (tipIsStuck==true)
        {
            player.GetComponent<HookShot>().TipGotStuck();
        }

        Vector2 betweenSTandPlayer= transform.position - player.transform.position;
        float distanceBetweenSTAndPlayer = betweenSTandPlayer.magnitude;
        
        if (distanceBetweenSTAndPlayer > ropeRange)
        {
            ST_RB.velocity = Vector2.zero;
        }
        
    }
    
}
