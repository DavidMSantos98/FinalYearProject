using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTipScriptV2 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool spearTipIsStuck;
    public bool collisionHappened;
    private Rigidbody2D spearTipRB;
    
    public Transform player;
    private float ropeLength;
    private float distanceBetweenSTAndPlayer;
    private Vector2 previousPos;

    void Start()
    {
        previousPos = Vector2.zero;
        distanceBetweenSTAndPlayer = 0;
        ropeLength = player.GetComponent<HookShotV2>().ropeLength;
        collisionHappened = false;
        spearTipIsStuck = false;
        spearTipRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // CheckSpearTipDistanceToPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionHappened == false)
        {
            collisionHappened = true;
            spearTipIsStuck = true;
            spearTipRB.bodyType = RigidbodyType2D.Static;
        }
            
    }

    private void CheckSpearTipDistanceToPlayer()
    {
        distanceBetweenSTAndPlayer = player.GetComponent<HookShotV2>().distanceBetweenPlayerAndsT;
        if (distanceBetweenSTAndPlayer>= ropeLength)
        {
            Debug.Log(distanceBetweenSTAndPlayer);
            spearTipRB.velocity = Vector2.zero;
            spearTipRB.position = previousPos;
        }
        previousPos = spearTipRB.position;
    }
}
