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

    public IEnumerator InitCoroutine()
    {
        sceneObjectManager = SceneObjectManager.Get();
       
        yield return new WaitUntil(() => sceneObjectManager.IsLoaded());

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
            Action cameraAction = () => 
            {
                sceneObjectManager.SetActiveForObjectWithTag(ManagedObjectTag.Environment, pair.Value.IsShowBuilding);
                List<ManagedObject> list = sceneObjectManager.GetObjectPackageBody(ManagedObjectTag.Display).ManagedObjectList;
                list.ForEach(obj => 
                {
                    DisplayObject displayObj = obj as DisplayObject;
                    if (displayObj != null)
                    {
                        displayObj.Alpha = pair.Value.DisplayAlpha;
                    }
                });
            };
            pair.Value.OnChangedSceneObject += pair.Key == tag ? cameraAction : null;
            pair.Value.Setup(pair.Key == tag);
        }
    }
}
