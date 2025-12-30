using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum CameraTag
    {
        Player,
        SandTable,
        Observer,
        Display,
        None
    }

    public CameraTag CurrentCameraTag;

    private Dictionary<CameraTag, CameraItem> cameraDict = new Dictionary<CameraTag, CameraItem>();

    private void Start()
    {
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
        Debug.Log($"SwitchCamera£º {CurrentCameraTag}");
        foreach (var kvp in cameraDict)
        {
            kvp.Value.gameObject.SetActive(kvp.Key == tag);
            kvp.Value.gameObject.tag = kvp.Key == tag ? "MainCamera" : "Untagged";
        }
    }
}
