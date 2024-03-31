using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

public class UIEditorHelper
{
    public static int IntInput(string label, int def)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label);
        def = EditorGUILayout.IntField(def);
        EditorGUILayout.EndHorizontal();
        return def;
    }

    public static float FloatInput(string label, float def)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label);
        def = EditorGUILayout.FloatField(def);
        EditorGUILayout.EndHorizontal();
        return def;
    }

    public static string StringInput(string label,string def)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label);
        def = EditorGUILayout.TextField(def);
        EditorGUILayout.EndHorizontal();
        return def;
    }

    public static Vector3 Vector3Input(string label,Vector3 def)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label);
        def = EditorGUILayout.Vector3Field(string.Empty,def );
        EditorGUILayout.EndHorizontal();
        return def;
    }

    public static int SelectList(string label, string[] list, int selected)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label);
        selected = EditorGUILayout.Popup(selected, list);
        EditorGUILayout.EndHorizontal();
        return selected;
    }


}

