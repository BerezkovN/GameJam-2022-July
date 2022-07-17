using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LevelBuilder : MonoBehaviour
{
    [MenuItem("GameObject/Platform Builder/Extend", false, 10)]
    private static void ExtendElement(MenuCommand menuCommand)
    {
        GameObject obj = Selection.activeObject as GameObject;

        if (obj is null || obj.GetComponent<BoxCollider>() is null)
            return;

        BoxCollider collider = obj.GetComponent<BoxCollider>();

        GUIUtility.systemCopyBuffer = collider.bounds.size.x.ToString();
    }
}
