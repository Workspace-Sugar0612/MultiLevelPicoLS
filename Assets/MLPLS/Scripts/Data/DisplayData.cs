using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DisplayStruct
{
    public string ClassName;

    public List<ObjectStruct> ObjectList;

    public void SetActiveByIndex(int index, bool active)
    {
        if (index < 0 || index >= ObjectList.Count) return;
        for (int i = 0; i < ObjectList.Count; i++) 
        {
            ObjectList[i].DisplayObject.SetActive(active && index == i);
        }
    }
}

[Serializable]
public class ObjectStruct
{
    [HideInInspector]
    public string ObjectName;

    public GameObject DisplayObject;
}

public class DisplayData : MonoBehaviour
{
    [SerializeField] private List<DisplayStruct> displayDataList;

    [HideInInspector] public List<DisplayStruct> DisplayDataList { get => displayDataList; }

    [HideInInspector] public int CurrentClassIndex = 0;

    [HideInInspector] public int CurrentObjectIndex = 0;

    public void Initialized()
    {
        foreach (DisplayStruct display in displayDataList)
        {
            foreach (ObjectStruct obj in display.ObjectList)
            {
                ManagedObject managedObject = obj.DisplayObject.GetComponent<ManagedObject>();
                if (managedObject != null)
                {
                    obj.ObjectName = managedObject.ManagedObjectName;
                }
            }
        }

        if (displayDataList.Count > 0 && displayDataList[0].ObjectList.Count > 0)
        {
            CurrentClassIndex = 0;
            CurrentObjectIndex = 0;
            SetObjectActiveByIndex(0, 0, true);
        }
    }

    public void SetObjectActiveByIndex(int classIndex, int objectIndex, bool active)
    {
        if (classIndex < 0 || classIndex >= displayDataList.Count) return;

        DisplayStruct display = displayDataList[classIndex];

        display.SetActiveByIndex(objectIndex, true);

        CurrentClassIndex = classIndex;
        CurrentObjectIndex = objectIndex;
    }
}
