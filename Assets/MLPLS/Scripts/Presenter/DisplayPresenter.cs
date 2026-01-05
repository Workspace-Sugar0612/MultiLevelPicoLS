using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPresenter : NetworkBehaviour
{
    [SerializeField] private DisplayData displayData;

    [SerializeField] private DisplayGUI displayGUI;

    [SyncVar] private bool isDisplay = true;

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        displayData.Initialized();
        displayGUI.Initialized(displayData.DisplayDataList);

        displayGUI.OnClickClassButton += OnClickClassButton;
        displayGUI.OnClickObjButton += OnClickObjectButton;
        displayGUI.OnClickDisplayButton += OnClickDisplayButton;
    }

    private void OnClickClassButton(int clickIndex)
    {
        displayData.CmdSetClassIndex(clickIndex);
        displayGUI.InitObjectListScrollRect(displayData.DisplayDataList[clickIndex].ObjectList);
    }

    private void OnClickObjectButton(int clickIndex)
    {
        displayData.CmdSetObjectIndex(clickIndex);
        displayData.CmdSetObjectActiveByIndex(displayData.CurrentClassIndex, clickIndex,
            true);

        displayGUI.SetActiveForObjectList(false);
    }

    private void OnClickDisplayButton()
    {
        displayData.CmdSetIsDisplay(isDisplay);
        displayGUI.SetDisplayButtonText(isDisplay);
        CmdSetIsDisplay(!isDisplay);
    }

    #region Member Variables

    [Command(requiresAuthority = false)]
    private void CmdSetIsDisplay(bool display)
    {
        isDisplay = display;
    }

    #endregion
}
