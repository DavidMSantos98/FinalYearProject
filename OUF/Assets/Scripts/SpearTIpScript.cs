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

    public bool spearIsStickable;

    private Vector2 previousPos;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (spearIsStickable)
        {
            tipIsStuck = true;
            player.GetComponent<HookShot>().spearTipIsStuck = true;
        }

    }

    private void Start()
    {
        ST_RB = GetComponent<Rigidbody2D>();
        tipIsStuck =false;
        positionOffset = new Vector2(0.91f, 0.1f);
        previousPos = ST_RB.position;
        spearIsStickable = true;
    }
    private void Update()
    {
        if (player.GetComponent<HookShot>().STDetatched)
        {
            ChangeSpearTipParenting();
        }

        if (tipIsStuck==true)
        {
            player.GetComponent<HookShot>().TipGotStuck();
        }

        float xDistance = Mathf.Abs(ST_RB.position.x - player.transform.position.x);
        float yDistance= Mathf.Abs(ST_RB.position.y - player.transform.position.y);

        if (xDistance > ropeRange || yDistance > ropeRange)
        {
            ST_RB.position = previousPos;
            ST_RB.velocity = Vector2.zero;
            spearIsStickable = false;
        }
        previousPos = ST_RB.position;
    }

    public void ChangeSpearTipParenting()
    {
        if(transform.parent== player.transform)
        {
            transform.SetParent(null, true);
        }
        else
        {
            transform.SetParent(player.transform, true);
        }
    }
    
}
