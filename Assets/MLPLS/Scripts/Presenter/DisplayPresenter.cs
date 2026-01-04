using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPresenter : MonoBehaviour
{
    [SerializeField] private DisplayData displayData;

    [SerializeField] private DisplayGUI displayGUI;

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
    }

    private void OnClickClassButton(int clickIndex)
    {
        Debug.Log("OnClickClassButton: " + clickIndex);
        displayData.CurrentClassIndex = clickIndex;
        displayGUI.InitObjectListScrollRect(displayData.DisplayDataList[clickIndex].ObjectList);
    }

    private void OnClickObjectButton(int clickIndex)
    {
        Debug.Log("OnClickObjectButton: " + clickIndex);
        displayData.CurrentObjectIndex = clickIndex;
        displayData.SetObjectActiveByIndex(displayData.CurrentClassIndex, clickIndex,
            true);

        displayGUI.SetActiveForObjectList(false);
    }
}
