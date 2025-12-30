using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pico;
using Unity.XR.PXR;

public class MRMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PXR_MixedReality.EnableVideoSeeThroughEffect(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
