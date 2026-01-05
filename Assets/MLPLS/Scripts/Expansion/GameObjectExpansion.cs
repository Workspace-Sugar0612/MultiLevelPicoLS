using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExpansion
{
    public static void NetworkSetActive(this GameObject obj, bool isActive)
    {
        if (obj == null) return;
        Renderer[] renders = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renders)
        {
            var prop = renderer.GetType().GetProperty("enabled");
            if (prop != null && prop.CanWrite && prop.PropertyType == typeof(bool))
            {
                prop.SetValue(renderer, isActive, null);
            }
        }
    }
}
