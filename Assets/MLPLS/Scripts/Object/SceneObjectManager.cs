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

    public GameObject BuildingModel; // 建筑物模型
}
