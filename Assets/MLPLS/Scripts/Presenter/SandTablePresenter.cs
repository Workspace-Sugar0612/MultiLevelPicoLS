using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTablePresenter : MonoBehaviour
{
    [SerializeField] private SandTableGUI sandTableGUI;

    [SerializeField] private NetworkSandTableData sandTableData;

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        sandTableData.Initialized();
        sandTableGUI.OnClickLeftButton += BackModel;
        sandTableGUI.OnClickRightButton += NextModel;
    }

    private void NextModel()
    {
        sandTableData.CmdNext();
    }

    private void BackModel()
    {
        sandTableData.CmdBack();
    }
}
