﻿using UnityEngine;
using Valve.VR;

public class SocketLocation : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float height=0.75f;

    public Transform head = null;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RotateUnderHead();
        PositionUnderHead();        
    }

    private void PositionUnderHead()
    {
        Vector3 adjustedHeight = head.localPosition;
        Debug.Log(adjustedHeight.y);
        adjustedHeight.y = Mathf.Lerp(0.0f, adjustedHeight.y, height);

        transform.localPosition = adjustedHeight;
    }

    private void RotateUnderHead()
    {
        Vector3 adjustRotation = head.localEulerAngles;
        adjustRotation.x = 0;
        adjustRotation.z = 0;

        transform.localEulerAngles = adjustRotation;
    }
}
