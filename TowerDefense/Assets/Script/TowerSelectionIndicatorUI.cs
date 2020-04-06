using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionIndicatorUI : MonoBehaviour
{
    [SerializeField]
    Transform MGtransform;

    [SerializeField]
    Transform CannonTransform;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position = MGtransform.position;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = CannonTransform.position;
        }
    }
}
