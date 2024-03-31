using System.Collections;
using UnityEditor;
using UnityEngine;

public class BaseEditorWindow<T> : EditorWindow where T : EditorWindow
{
    void OnFocus()
    {
        autoRepaintOnSceneChange = true;
        // Remove delegate listener if it has previously
        // been assigned.
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;

        // Add (or re-add) the delegate.
        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDestroy()
    {
        // When the window is destroyed, remove the delegate
        // so that it will no longer do any drawing.
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    protected virtual void OnSceneGUI(SceneView sceneView)
    {

    }

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(T));
    }
}