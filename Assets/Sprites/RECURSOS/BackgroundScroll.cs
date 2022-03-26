using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class BackgroundScroll : MonoBehaviour
{
    private Vector2 offset;
    private Material _material;
    public float xVelocity, yVelocity;
    
    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }
    void Update()
    {
        _material.mainTextureOffset += offset * Time.deltaTime;
    }
}
