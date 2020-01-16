using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotV2 : MonoBehaviour
{
    // Start is called before the first frame update
    //sT stands for spear tip
    private bool sT_IsStuck;
    private bool sT_IsSticky;
    private bool sT_IsDetatched;

    private LineRenderer rope;
    public float ropeWidth;
    public float ropeResistance;

    private Vector2 userClickPosition;
    private Vector2 spearTipPosition;
    private Vector2 playerPosition;
    private Vector2 betweenPlayerAndSpearTip;

    private Rigidbody2D playerRigidbody;

    public float projectilePower;
    Vector2 directionVector;

    public Transform spearTip;
    private Rigidbody2D spearTipRigidbody;

    public float requiredDistanceToCollectsT;

    private float timeUntilCollisonIsOn;
    public float timeBetweensTUnstickAndCollisionIsOn;

    private float timeUntilsTCanBeCollected;
    public float timeBetweensTLaunchAndAbilityToBeCollected;

    private Vector2 sTInitialLocalPosition;

    void Start()
    {
        spearTip.gameObject.SetActive(false);
        playerRigidbody = GetComponent<Rigidbody2D>();
        spearTipRigidbody = spearTip.GetComponent<Rigidbody2D>();
        sT_IsStuck = false;
        rope = GetComponent<LineRenderer>();
        rope.enabled = false;
        timeUntilCollisonIsOn = timeBetweensTUnstickAndCollisionIsOn;
        timeUntilsTCanBeCollected = timeBetweensTLaunchAndAbilityToBeCollected;
        sTInitialLocalPosition = spearTip.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        spearTipPosition = (Vector2)spearTip.position;
        playerPosition = (Vector2)transform.position;
        betweenPlayerAndSpearTip = spearTipPosition - playerPosition;

        if (Input.GetMouseButtonDown(0) && sT_IsDetatched == false)
        {
            LaunchST();
        }
        
        if (Input.GetMouseButtonDown(1) && spearTip.GetComponent<SpearTipScriptV2>().spearTipIsStuck == true)
        {
            UnStucksT();
        }

        if (sT_IsDetatched == true)
        {
            DrawRope();
            ChecIfsTIsCollectible();
        }

        if (sT_IsDetatched == true && spearTip.GetComponent<SpearTipScriptV2>().spearTipIsStuck == false && spearTip.GetComponent<SpearTipScriptV2>().collisionHappened == false)
        {
            ApplyProjectionPower();
        }

        if (spearTip.GetComponent<SpearTipScriptV2>().spearTipIsStuck == true)
        {
            //Apply rope resistance
            playerRigidbody.AddForce(ropeResistance * betweenPlayerAndSpearTip.normalized, ForceMode2D.Force);
        }
    }


    private void UnStucksT()
    {
        spearTipRigidbody.bodyType = RigidbodyType2D.Dynamic;
        spearTip.GetComponent<SpearTipScriptV2>().spearTipIsStuck = false;
    }

    private void ChecIfsTIsCollectible()
    {        
        if (timeUntilsTCanBeCollected <= 0)
        {
            if (spearTip.GetComponent<SpearTipScriptV2>().spearTipIsStuck == false)
            {
                float distanceBetweenPlayerAndsT = betweenPlayerAndSpearTip.magnitude;
                if (distanceBetweenPlayerAndsT <= requiredDistanceToCollectsT)
                {
                    CollectsT();
                    timeUntilsTCanBeCollected = timeBetweensTLaunchAndAbilityToBeCollected;
                }
            }
        }
        else
        {
            timeUntilsTCanBeCollected -= Time.deltaTime;
        }
    }

    private void CollectsT()
    {
        rope.enabled = false;
        spearTip.gameObject.SetActive(false);
        ResetSpearTipAttributes();
        ResetSpearTipPosition();

    }

    private void ResetSpearTipAttributes()
    {
        spearTip.GetComponent<SpearTipScriptV2>().collisionHappened = false;
        spearTipRigidbody.bodyType = RigidbodyType2D.Kinematic;
        sT_IsDetatched = false;
        spearTip.SetParent(transform, true);
        timeUntilCollisonIsOn = timeBetweensTUnstickAndCollisionIsOn;
    }

    private void ResetSpearTipPosition()
    {
        spearTip.localPosition = sTInitialLocalPosition;
    }

    private void LaunchST()
    {
        userClickPosition=(Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spearTip.gameObject.SetActive(true);
        sT_IsDetatched = true;
        spearTipRigidbody.bodyType = RigidbodyType2D.Dynamic;

        CreateRope();
        ProjectST();
    }

    private void CreateRope()
    {
        rope.enabled = true;
        rope.startWidth = ropeWidth;
        rope.endWidth = ropeWidth;
        DrawRope();
    }

    private void DrawRope()
    {
        rope.SetPosition(0, playerPosition);
        rope.SetPosition(1, spearTipPosition);
    }

    private void ProjectST()
    {
        //Calculate direction vector
        Vector2 betweenOriginAndDestination = userClickPosition - spearTipPosition;
        directionVector = betweenOriginAndDestination.normalized;

        //Detatch from parent
        spearTip.SetParent(null, true);
        ApplyProjectionPower();
    }

    private void ApplyProjectionPower()
    {
        spearTipRigidbody.AddForce(projectilePower * directionVector, ForceMode2D.Force);
    }
}
