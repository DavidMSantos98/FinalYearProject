using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBasedRendering : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 5000;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnlyOnce = false;
    private Renderer rend;
    void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rend.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
}
