using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NetworkSandTableData : NetworkBehaviour
{
    [SerializeField] private List<ManagedObject> sandTableModles = new List<ManagedObject>();

    [SyncVar(hook = nameof(OnChangedIndex))] private int index = 0;

    private void Start()
    {
        
    }

    public void Initialized()
    {
        for (int i = 0; i < sandTableModles.Count; i++)
        {
            SetModelActive(i, i == 0);
        }
    }

    #region SandTable Model Methods

    public ManagedObject GetModel()
    {
        return sandTableModles[index];
    }

    public void SetModelActive(int i, bool b)
    {
        sandTableModles[i].gameObject.NetworkSetActive(b);
    }

    [Command(requiresAuthority = false)] public void CmdNext() => index = (index + 1) % sandTableModles.Count;

    [Command(requiresAuthority = false)] public void CmdBack() => index = (index - 1 + sandTableModles.Count) % sandTableModles.Count;

    #endregion

    #region Network Methods

    public void OnChangedIndex(int oldVal, int newVal)
    {
        Debug.Log($"oldVal: {oldVal}, newVal: {newVal}");
        SetModelActive(oldVal, false);
        SetModelActive(newVal, true);
    }

    #endregion
}
