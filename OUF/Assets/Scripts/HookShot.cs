using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer hookRopeLine;
    public float line_width = 0.2f;
    public Rigidbody2D spearTip;
    private bool ropeTravelling;
    private Vector2 spearTipDestination;

    public float projectilePower;
    void Start()
    {
        ropeTravelling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ropeTravelling = true;
            CreateRope();
            ProjectSpearTip();
        }

        if (ropeTravelling)
        {
            hookRopeLine.SetPosition(0, transform.position);
            hookRopeLine.SetPosition(1, spearTip.position);
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
        spearTip.AddForce(projectilePower * CalculateSpearTipDestination(), ForceMode2D.Impulse);
    }

    private Vector2 CalculateSpearTipDestination()
    {
        Vector2 betweenOriginAndDestination = spearTipDestination - spearTip.position;
        float magnitudeOfbOADVector = Mathf.Sqrt(Mathf.Pow(spearTipDestination.x - spearTip.position.x, 2) - Mathf.Pow(spearTipDestination.y - spearTip.position.y, 2));
        Vector2 directionVector = betweenOriginAndDestination.normalized; //Also known as unit vector, or normalized vector
        return directionVector;
    }   


}

