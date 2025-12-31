using kcp2k;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
    public CameraTag cameraTag;

    public bool IsShowBuilding = true;

    private CameraController cameraController;

    private  AudioListener audioListener;

    public Action OnChangedSceneObject; // 不同的相机场景内容不同

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        cameraController = CameraController.Get();
        cameraController.RegisterCamera(cameraTag, this);

        audioListener = GetComponent<AudioListener>();
    }

    public void Setup(bool isActive)
    {
        gameObject.SetActive(isActive);
        gameObject.tag = isActive ? "MainCamera" : "Untagged";
        audioListener.gameObject.SetActive(isActive);
        OnChangedSceneObject?.Invoke();
    }
}
