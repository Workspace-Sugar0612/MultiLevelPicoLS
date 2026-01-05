using Mirror;
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
            ObjectList[i].DisplayObject?.gameObject?.NetworkSetActive(active && index == i);
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

public class DisplayData : NetworkBehaviour
{
    [SerializeField] private List<DisplayStruct> displayDataList;

    [HideInInspector] public List<DisplayStruct> DisplayDataList { get => displayDataList; }

    [HideInInspector] [SyncVar] private int currentClassIndex = 0;

    [HideInInspector] [SyncVar] private int currentObjectIndex = 0;

    [SerializeField] private Animator displayAnim;

    public int CurrentClassIndex { get => currentClassIndex; }

    public int CurrentObjectIndex { get => currentObjectIndex; }

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
            CmdSetClassIndex(0);
            CmdSetClassIndex(0);
            LocalSetObjectActiveByIndex(0, 0, true);
        }   
    }


    #region Network Methods

    [Command(requiresAuthority = false)]
    public void CmdSetObjectActiveByIndex(int classIndex, int objectIndex, bool active)
    {
        if (classIndex < 0 || classIndex >= displayDataList.Count) return;

        CmdSetClassIndex(classIndex);
        CmdSetObjectIndex(objectIndex);

        RpcSetActiveOfDisplayStruct(classIndex, objectIndex, active);
    }

    [ClientRpc]
    private void RpcSetActiveOfDisplayStruct(int classIndex, int objectIndex, bool active)
    {
        DisplayStruct display = displayDataList[classIndex];
        if (objectIndex < 0 || objectIndex >= display.ObjectList.Count) return;
        
        for (int i = 0; i < display.ObjectList.Count; i++)
        {
            display.ObjectList[i].DisplayObject?.gameObject?.NetworkSetActive(active && objectIndex == i);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSetIsDisplay(bool display)
    {
        RpcSetIsDisplay(display);
    }

    [ClientRpc]
    public void RpcSetIsDisplay(bool display)
    {
        Action action = display ? Display : Putback;
        action?.Invoke();
    }

    [Command(requiresAuthority = false)]
    public void CmdSetClassIndex(int classIndex)
    {
        currentClassIndex = classIndex;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetObjectIndex(int objectIndex)
    {
        currentObjectIndex = objectIndex;
    }

    #endregion

    #region Local Methods

    private void LocalSetObjectActiveByIndex(int classIndex, int objectIndex, bool active)
    {
        if (classIndex < 0 || classIndex >= displayDataList.Count) return;

        DisplayStruct display = displayDataList[classIndex];

        display.SetActiveByIndex(objectIndex, true);
    }

    public void Display()
    {
        SetDisplayAnimation("isDisplay", true);
        SetDisplayAnimation("isPutback", false);
    }

    public void Putback()
    {
        SetDisplayAnimation("isDisplay", false);
        SetDisplayAnimation("isPutback", true);
    }

    private void SetDisplayAnimation(string displayParam, bool b)
    {
        displayAnim.SetBool(displayParam, b);
    }

    #endregion
}
