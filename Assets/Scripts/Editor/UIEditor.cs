using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UIEditor : EditorWindow
{
    [MenuItem("Tools/Map Editor")]
    static void ShowWindow()
    {
        GetWindow<UIEditor>("UI Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("UI Editor", EditorStyles.boldLabel);

        if (GUILayout.Button("Auto Anchors"))
        {
            SetAnchors();
        }

        if (GUILayout.Button("Set Auto Anchor"))
        {
            SetAnchor();
        }
    }

    private void SetAnchor()
    {
        var objs = Selection.gameObjects;

        foreach (var o in objs)
        {
            if (o != null && o.GetComponent<RectTransform>() != null)
            {
                var r = o.GetComponent<RectTransform>();
                r.offsetMin = new Vector2(0, 0);
                r.offsetMax = new Vector2(0, 0);
                r.anchorMin = new Vector2(0, 0);
                r.anchorMax = new Vector2(1, 1);
                r.pivot = new Vector2(0.5f, 0.5f);
            }
        }
    }

    private void SetAnchors()
    {
        var objs = Selection.gameObjects;

        foreach (var o in objs)
        {
            if (o != null && o.GetComponent<RectTransform>() != null)
            {
                var r = o.GetComponent<RectTransform>();
                var p = o.transform.parent.GetComponent<RectTransform>();

                var offsetMin = r.offsetMin;
                var offsetMax = r.offsetMax;
                var _anchorMin = r.anchorMin;
                var _anchorMax = r.anchorMax;

                var parent_width = p.rect.width;
                var parent_height = p.rect.height;

                var anchorMin = new Vector2(_anchorMin.x + (offsetMin.x / parent_width),
                    _anchorMin.y + (offsetMin.y / parent_height));
                var anchorMax = new Vector2(_anchorMax.x + (offsetMax.x / parent_width),
                    _anchorMax.y + (offsetMax.y / parent_height));

                r.anchorMin = anchorMin;
                r.anchorMax = anchorMax;

                r.offsetMin = new Vector2(0, 0);
                r.offsetMax = new Vector2(0, 0);
                r.pivot = new Vector2(0.5f, 0.5f);
            }
        }
    }
}
