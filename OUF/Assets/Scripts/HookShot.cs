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
    void Start()
    {
        STDetatched = false;
        spearTip.gameObject.SetActive(false);
        playerRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0 )&& spearTipIsStuck==false)
        {
            spearTip.gameObject.SetActive(true);
            STDetatched = true;
            CreateRope();
            ProjectSpearTip();
        }
        if (Input.GetMouseButtonDown(1))
        {
            spearTip.bodyType = RigidbodyType2D.Dynamic;
            spearTipIsStuck = false;
            spearTip.GetComponent<SpearTIpScript>().tipIsStuck = false;
        }

        if (STDetatched)
        {
            hookRopeLine.SetPosition(0, transform.position);
            hookRopeLine.SetPosition(1, spearTip.position);
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

}

