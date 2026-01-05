using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGUI : MonoBehaviour
{
    #region UI 组件

    [SerializeField]
    private GameObject classScrollRect;

    [SerializeField]
    private GameObject classButtonPrefab;

    [SerializeField]
    private Transform classButtonParent;

    [SerializeField]
    private GameObject objectListScrollRect;

    [SerializeField]
    private GameObject objButtonPrefab;

    [SerializeField]
    private Transform objButtonParent;

    [SerializeField]
    private Button displayButton;

    #endregion

    public Action<int> OnClickClassButton;

    public Action<int> OnClickObjButton;

    public Action OnClickDisplayButton;

    private List<GameObject> objectButtonList = new List<GameObject>();

    private void Start()
    {

    }

    #region Initialized
    public void Initialized(List<DisplayStruct> list)
    {
        InitClassScrollRect(list);

        displayButton.onClick.AddListener(() => {
            OnClickDisplayButton?.Invoke();
        });
    }

    private void InitClassScrollRect(List<DisplayStruct> list)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            DisplayStruct display = list[i];
            GameObject buttonObj = Instantiate(classButtonPrefab, classButtonParent);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = display.ClassName;

            int clickIndex = i;
            button.onClick.AddListener(() => {
                OnClickClassButton?.Invoke(clickIndex);
            });
        }
        classScrollRect.SetActive(true);
        objectListScrollRect.SetActive(false);
    }

    public void InitObjectListScrollRect(List<ObjectStruct> objectList)
    {
        foreach (var obj in objectButtonList)
        {
            obj.SetActive(false);
            Destroy(obj);
        }
        objectButtonList.Clear();

        for (int i = 0; i < objectList.Count; ++i)
        {
            ObjectStruct objectStruct = objectList[i];
            GameObject buttonObj = Instantiate(objButtonPrefab, objButtonParent);
            Button button = buttonObj.GetComponent<Button>();
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = objectStruct.ObjectName;

            int clickIndex = i;
            button.onClick.AddListener(() => {
                OnClickObjButton?.Invoke(clickIndex);
            });

            objectButtonList.Add(buttonObj);
        }
        SetActiveForObjectList(true);
    }

    #endregion

    #region UI Methods

    public void SetActiveForObjectList(bool active)
    {
        objectListScrollRect.SetActive(active);
    }

    public void SetDisplayButtonText(bool b)
    {
        displayButton.GetComponentInChildren<Text>().text = b ? "放回" : "3D展示";
    }

    #endregion
}
