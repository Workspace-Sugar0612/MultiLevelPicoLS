using kcp2k;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraTag
{
    Player,
    SandTable,
    Observer,
    Display,
    None
}

public class CameraController : MonoBehaviour
{
    private static CameraController instance;

    public static CameraController Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<CameraController>();
        }
        return instance;
    }

    public CameraTag CurrentCameraTag;

    private Dictionary<CameraTag, CameraItem> cameraDict = new Dictionary<CameraTag, CameraItem>();

    private SceneObjectManager sceneObjectManager;

    private void Start()
    {
        //Initialize();
        StartCoroutine(InitCoroutine());
    }

    private void Initialize()
    {
        sceneObjectManager = SceneObjectManager.Get();

        SwitchCamera(CurrentCameraTag);
    }

    private IEnumerator InitCoroutine()
    {
        sceneObjectManager = SceneObjectManager.Get();

        bool isSceneObjectInitialized = true;
        foreach (ManagedObjectTag tag in Enum.GetValues(typeof(ManagedObjectTag)))
        {
            List<ManagedObject> list = sceneObjectManager.GetObjectPackageBody(tag).ManagedObjectList;
            foreach (ManagedObject obj in list)
            {
                if (!obj.IsInitialized)
                {
                    isSceneObjectInitialized = false;
                    break;
                }
            }
        }

        yield return new WaitUntil(() => isSceneObjectInitialized);

        SwitchCamera(CurrentCameraTag);
    }

    public void RegisterCamera(CameraTag tag, CameraItem camera)
    {
        if (!cameraDict.ContainsKey(tag))
        {
            cameraDict.Add(tag, camera);
        }
    }

    public void UnregisterCamera(CameraTag tag)
    {
        cameraDict.Remove(tag);
    }

    public void SwitchCamera(CameraTag tag)
    {
        foreach (var pair in cameraDict)
        {
            Action cameraAction = () => { sceneObjectManager.SetActiveForObjectWithTag(ManagedObjectTag.Environment, pair.Value.IsShowBuilding); };
            pair.Value.OnChangedSceneObject += pair.Key == tag ? cameraAction : null;
            pair.Value.Setup(pair.Key == tag);
        }
    }
}
