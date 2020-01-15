using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer hookRopeLine;
    public float line_width = 0.2f;
    public Rigidbody2D spearTip;
    private Rigidbody2D playerRB;
    public bool STDetatched;
    private Vector2 spearTipDestination;
    public bool spearTipIsStuck;

    public float projectilePower;
    public float ropeResistance;
    private bool ropeTravelling;

    public float timeBetweenSTProjectionAndCollection;
    private float timeUntilProjectionCollection;

    public float sTCollectionRange;
    private bool STIsCollectible;
    void Start()
    {
        STIsCollectible = false;
        STDetatched = false;
        spearTip.gameObject.SetActive(false);
        playerRB = GetComponent<Rigidbody2D>();
        timeUntilProjectionCollection = timeBetweenSTProjectionAndCollection;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0 )&& spearTipIsStuck==false)
        {
            spearTip.gameObject.SetActive(true);
            spearTip.GetComponent<SpearTIpScript>().spearIsStickable = true;
            STDetatched = true;
            CreateRope();
            ProjectSpearTip();
            ropeTravelling = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            spearTip.bodyType = RigidbodyType2D.Dynamic;
            spearTip.GetComponent<SpearTIpScript>().spearIsStickable = false;
            spearTipIsStuck = false;
            spearTip.GetComponent<SpearTIpScript>().tipIsStuck = false;
        }

        CheckPlayerSTCollection();

        if (ropeTravelling)
        {

        }

        if (STDetatched)
        {
            hookRopeLine.SetPosition(0, transform.position);
            hookRopeLine.SetPosition(1, spearTip.position);

            timeUntilProjectionCollection -= Time.deltaTime;
            Debug.Log(timeUntilProjectionCollection);

            if (timeUntilProjectionCollection <= 0)
            {
                STIsCollectible = true;
            }
        }

        if (spearTipIsStuck)
        {
            Vector2 playerPos = transform.position;//transform.position is set as Vector3
            Vector2 betweenPlayerAndSpearTip = playerPos - spearTip.position;
            Vector2 ropeDirectionVector = betweenPlayerAndSpearTip.normalized;

            playerRB.AddForce(ropeResistance * -ropeDirectionVector, ForceMode2D.Force);

        }
    }

    private void CreateRope()
    {
        hookRopeLine = GetComponent<LineRenderer>();
        if (!hookRopeLine)
        {
            hookRopeLine = gameObject.AddComponent<LineRenderer>();
        }
        hookRopeLine.startWidth = line_width;
        hookRopeLine.endWidth = line_width;

        spearTipDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hookRopeLine.SetPosition(0, transform.position);
        hookRopeLine.SetPosition(1, spearTip.position);
    }

    private void ProjectSpearTip()
    {
        spearTip.AddForce(projectilePower * CalculateSpearTipDestination(), ForceMode2D.Force);
    }

    private Vector2 CalculateSpearTipDestination()
    {
        Vector2 betweenOriginAndDestination = spearTipDestination - spearTip.position;
        Vector2 projectileDirectionVector = betweenOriginAndDestination.normalized; //Also known as unit vector, or normalized vector
        return projectileDirectionVector;
    }

    public void TipGotStuck()
    {
        spearTipIsStuck = true;
        spearTip.bodyType = RigidbodyType2D.Static;
    }

    private void CheckPlayerSTCollection()
    {
        if (spearTip.gameObject.activeSelf)
        {
            Vector2 distanceBetweenSTAndPlayer = playerRB.position - spearTip.position;
            float distanceBetweenPlayerAndSpearTip = distanceBetweenSTAndPlayer.magnitude;

            if (spearTipIsStuck != true && distanceBetweenPlayerAndSpearTip <= sTCollectionRange && STIsCollectible==true)
            {
                spearTip.gameObject.SetActive(false);
                spearTip.GetComponent<SpearTIpScript>().ChangeSpearTipParenting();
                STIsCollectible = false;
                timeUntilProjectionCollection = timeBetweenSTProjectionAndCollection;
                hookRopeLine.enabled = false;
            }

        }

    }
}

