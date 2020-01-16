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

    void Start()
    {
        collisionHappened = false;
        spearTipIsStuck = false;
        spearTipRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
