﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InterdimensionalTransport : MonoBehaviour
{
    public Material[] materials;
    public MeshRenderer portalWindow;

    void Start()
    {
        foreach (var mat in materials)
        {
            mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name != "ARCoreCamera") return;

        if (transform.position.z >= other.transform.position.z - 0.01f)
        {
            // Outside of other world
            Debug.Log("Outside of other world");
            if (portalWindow.enabled == false)
            {
                portalWindow.enabled = true;
            }
            foreach (var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.Equal);
            }
        }
        else
        {
            // Inside other dimension
            Debug.Log("Inside Other World");
            if (portalWindow.enabled == true)
            {
                portalWindow.enabled = false;
            }
            foreach (var mat in materials)
            {
                mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
            }
        }
    }
    private void OnDestroy()
    {
        foreach (var mat in materials)
        {
            mat.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
