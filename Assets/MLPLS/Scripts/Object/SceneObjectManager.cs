using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectManager : MonoBehaviour
{
    private static SceneObjectManager instance;

    public static SceneObjectManager Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SceneObjectManager>();
        }
        return instance;
    }

    [SerializeField] private List<ManagedObjectPackageBody> sceneObjectList = new List<ManagedObjectPackageBody>();

    public bool IsInitialized = false;

    private void Awake()
    {
        Initialized();
    }

    private void Initialized()
    {
        //sceneObjectList.Clear();
        //foreach (ManagedObjectTag tag in Enum.GetValues(typeof(ManagedObjectTag)))
        //{
        //    ManagedObjectPackageBody body = new ManagedObjectPackageBody(tag);
        //    sceneObjectList.Add(body);
        //}
        IsInitialized = true;
    }

    #region SceneObjectList Method

    public void AddObject(ManagedObjectTag tag, ManagedObject @object)
    {
        ManagedObjectPackageBody body = GetObjectPackageBody(tag);
        body.ManagedObjectList.Add(@object);
    }

    public ManagedObjectPackageBody GetObjectPackageBody(ManagedObjectTag tag)
    {
        foreach (ManagedObjectPackageBody body in sceneObjectList)
        {
            if (body.Tag == tag)
            {
                return body;
            }
        }
        return null;
    }

    #endregion

    #region Object Method

    public ManagedObject GetObjectByName(ManagedObjectTag tag, string name)
    {
        ManagedObjectPackageBody body = GetObjectPackageBody(tag);
        foreach (ManagedObject obj in body.ManagedObjectList)
        {
            if (obj.ManagedObjectName == name)
            {
                return obj;
            }
        }
        return null;
    }

    public void SetActiveForObjectWithTag(ManagedObjectTag tag, bool isActive)
    {
        ManagedObjectPackageBody body = GetObjectPackageBody(tag);
        // Debug.Log($"SetActiveForObjectWithTag: {tag} to {isActive}, body.ManagedObjectList£º{body.ManagedObjectList.Count}");
        foreach (ManagedObject obj in body.ManagedObjectList)
        {
            obj.gameObject.NetworkSetActive(isActive);
        }
    }

    public bool IsLoaded()
    {
        int totalCount = 0;
        int sceneObjectCount = GameObject.FindObjectsOfType<ManagedObject>().Length;
        foreach (ManagedObjectPackageBody body in sceneObjectList)
        {
            totalCount += body.ManagedObjectList.Count;
        }
        return totalCount == sceneObjectCount;
    }

    #endregion
}
