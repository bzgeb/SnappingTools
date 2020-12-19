using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SnapOnStart : MonoBehaviour
{
    /*
    void Start()
    {
        if (Application.isPlaying)
        {
            LayerMask groundLayerMask = LayerMask.NameToLayer($"Ground");
            if (SnappingUtils.PlaceOnGround(transform.position, groundLayerMask, out Vector3 point, out _))
            {
            }
            enabled = false;
        }
    }

    void Update()
    {
        Graphics.DrawMesh();
    }
    */
}
