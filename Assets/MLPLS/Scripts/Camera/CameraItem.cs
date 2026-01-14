using kcp2k;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraItem : MonoBehaviour, ICameraRender
{
    public CameraTag cameraTag;

    public bool IsShowBuilding = true;

    public bool IsShowDisplay = true;

    [Range(0.0f, 1.0f)] public float DisplayAlpha = 1.0f;

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

    public void SetDisyplayAlpha(GameObject display, float alpha, float duration)
    {
        display.GetComponent<DisplayObject>()!.SetAlphaTransition(alpha, duration);
    }
}
