using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandTableGUI : MonoBehaviour
{
    [SerializeField] private Button rightButton;

    [SerializeField] private Button leftButton;

    public event Action OnClickRightButton;

    public event Action OnClickLeftButton;

    private void Start()
    {
        rightButton.onClick.AddListener(() =>
        {
            OnClickRightButton?.Invoke();
        });

        leftButton.onClick.AddListener(() =>
        {
            OnClickLeftButton?.Invoke();
        });
    }
}
