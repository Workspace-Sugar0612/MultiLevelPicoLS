using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayObject : ManagedObject
{
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float alpha;

    public float Alpha
    {
        get => alpha;
        set 
        {
            alpha = value;
            renderer.material.SetFloat("_AlphaScale", alpha);
        }
    }

    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
}
