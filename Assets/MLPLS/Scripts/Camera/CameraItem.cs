using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
    public CameraController.CameraTag cameraTag;

    public bool IsShowBuilding = true;

    private CameraController cameraController;

    private SceneObjectManager sceneObjectManager;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        cameraController = CameraController.Get();
        cameraController.RegisterCamera(cameraTag, this);

        sceneObjectManager = SceneObjectManager.Get();
        sceneObjectManager.BuildingModel.SetActive(IsShowBuilding);
    }
}
