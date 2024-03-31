
//using UnityEngine;
//using System.Collections.Generic;
//using UnityEditor;
//using GrowII;


//[CustomEditor(typeof(CameraPoint))]
//public class CameraPointControlEditor : Editor
//{
//    CameraPoint myCameraPoint;

//    void OnEnable()
//    {
//        myCameraPoint = (CameraPoint)target;
//    }

    
//    public override void OnInspectorGUI()
//    {
        
//        myCameraPoint.Type = (CameraPoint.PointType)EditorGUILayout.EnumPopup("Type", myCameraPoint.Type);
//        switch (myCameraPoint.Type)
//        {
//            case CameraPoint.PointType.Fixed:
//                myCameraPoint.FixedSize = EditorGUILayout.Vector2Field("Fixed Size", myCameraPoint.FixedSize);
//                break;
//            case CameraPoint.PointType.FollowHorizontal:
//                myCameraPoint.FixedSize = EditorGUILayout.Vector2Field("Size", myCameraPoint.FixedSize);
//                myCameraPoint.HorizontalSpeed = EditorGUILayout.FloatField("Horizontal Speed", myCameraPoint.HorizontalSpeed);
//                break;
//            case CameraPoint.PointType.FollowVertical:
//                myCameraPoint.FixedSize = EditorGUILayout.Vector2Field("Size", myCameraPoint.FixedSize);
//                myCameraPoint.VerticalSpeed = EditorGUILayout.FloatField("Vertical Speed", myCameraPoint.VerticalSpeed);
//                break;
//            default:
//                break;
//        }
//    }

//    public void OnSceneGUI()
//    {
//        Vector3 LeftUp = new Vector3(myCameraPoint.transform.position.x - myCameraPoint.FixedSize.x, myCameraPoint.transform.position.y + myCameraPoint.FixedSize.y, myCameraPoint.transform.position.z);
//        Vector3 RightUp = new Vector3(myCameraPoint.transform.position.x + myCameraPoint.FixedSize.x, myCameraPoint.transform.position.y + myCameraPoint.FixedSize.y, myCameraPoint.transform.position.z);
//        Vector3 LeftDown = new Vector3(myCameraPoint.transform.position.x - myCameraPoint.FixedSize.x, myCameraPoint.transform.position.y - myCameraPoint.FixedSize.y, myCameraPoint.transform.position.z);
//        Vector3 RightDown = new Vector3(myCameraPoint.transform.position.x + myCameraPoint.FixedSize.x, myCameraPoint.transform.position.y - myCameraPoint.FixedSize.y, myCameraPoint.transform.position.z);

//        Vector3[] BoxVertexs = { LeftUp, RightUp, RightUp, RightDown, RightDown, LeftDown, LeftDown, LeftUp };

//        Handles.color = Color.green;
//        Handles.DrawLines(BoxVertexs);
//    }
//}
