using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManagedObjectTag
{
    None,
    Player,
    Item,
    Environment
}

[Serializable]
public class ManagedObjectPackageBody
{
    public ManagedObjectTag Tag;
    public List<ManagedObject> ManagedObjectList = new List<ManagedObject>();

    public ManagedObjectPackageBody() { }

    public ManagedObjectPackageBody(ManagedObjectTag tag)
    {
        Tag = tag;
    }

}

public class ManagedObject : MonoBehaviour
{
    public string ManagedObjectName;

    public ManagedObjectTag ObjectTag;

    private SceneObjectManager _sceneObjectManager;

    public bool IsInitialized = false;

    private void Awake()
    {
        StartCoroutine(InitCoroutine());
    }

    private IEnumerator InitCoroutine()
    {
        _sceneObjectManager = SceneObjectManager.Get();

        yield return new WaitUntil(() => _sceneObjectManager.IsInitialized);

        _sceneObjectManager.AddObject(ObjectTag, this);

        IsInitialized = true;
    }

    private void Start()
    {
        
    }
}
