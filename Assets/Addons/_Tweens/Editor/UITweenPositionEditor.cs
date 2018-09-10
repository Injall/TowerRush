using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;

[CustomEditor(typeof(UITweenPosition))]
[CanEditMultipleObjects]
public class UITweenPositionEditor : Editor
{
    private UITweenPosition obj;
    private SerializedProperty resetOnStop;
    private SerializedProperty duration   ;
    private SerializedProperty curve      ;
    private SerializedProperty behavior   ;
    private SerializedProperty direction  ;

    public SerializedProperty offsetMinX;
    public SerializedProperty offsetMinY;
    public SerializedProperty offsetMaxX;
    public SerializedProperty offsetMaxY;

    public SerializedProperty posX;
    public SerializedProperty posY;

    public SerializedProperty mode;

    void OnEnable()
    {
        obj = (UITweenPosition)target;

        resetOnStop = serializedObject.FindProperty("resetOnStop");
        duration = serializedObject.FindProperty("duration");
        curve = serializedObject.FindProperty("curve");
        behavior = serializedObject.FindProperty("behavior");
        direction = serializedObject.FindProperty("direction");

        offsetMinX = serializedObject.FindProperty("offsetMinX");
        offsetMinY = serializedObject.FindProperty("offsetMinY");
        offsetMaxX = serializedObject.FindProperty("offsetMaxX");
        offsetMaxY = serializedObject.FindProperty("offsetMaxY");

        posX = serializedObject.FindProperty("posX");
        posY = serializedObject.FindProperty("posY");

        mode = serializedObject.FindProperty("mode");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (obj.RectTransform == null)
            GUILayout.Label("Apply this script on a RectTransform", EditorStyles.boldLabel);
        else
        {
            EditorGUILayout.PropertyField(resetOnStop);
            EditorGUILayout.PropertyField(duration);
            EditorGUILayout.PropertyField(curve);
            EditorGUILayout.PropertyField(behavior);
            EditorGUILayout.PropertyField(direction);

            GUILayout.Space(18);

            EditorGUILayout.PropertyField(mode);

            GUILayout.Space(9);

            if (AnchorTool.IsStretchedHorizontally(obj.RectTransform) && obj.mode == UITweenPosition.Mode.Auto)
            {
                GUILayout.Label("Horizontal Stretching Offset", EditorStyles.largeLabel);

                GUILayout.Label("Left", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(offsetMinX.FindPropertyRelative("src"));
                EditorGUILayout.PropertyField(offsetMinX.FindPropertyRelative("dst"));

                GUILayout.Space(9);

                GUILayout.Label("Right", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(offsetMaxX.FindPropertyRelative("src"));
                EditorGUILayout.PropertyField(offsetMaxX.FindPropertyRelative("dst"));

                bool copyXCurrentSRC = GUILayout.Button("Copy Stretch to src");
                bool copyXCurrentDST = GUILayout.Button("Copy Stretch to dst");

                if (copyXCurrentSRC)
                {
                    offsetMaxX.FindPropertyRelative("src").floatValue = -obj.RectTransform.offsetMax.x;
                    offsetMinX.FindPropertyRelative("src").floatValue = obj.RectTransform.offsetMin.x;
                }

                if (copyXCurrentDST)
                {
                    offsetMaxX.FindPropertyRelative("dst").floatValue = -obj.RectTransform.offsetMax.x;
                    offsetMinX.FindPropertyRelative("dst").floatValue = obj.RectTransform.offsetMin.x;
                }
            }
            else
            {
                GUILayout.Label("Horizontal Position", EditorStyles.largeLabel);

                EditorGUILayout.PropertyField(posX.FindPropertyRelative("src"));
                EditorGUILayout.PropertyField(posX.FindPropertyRelative("dst"));

                bool copyXCurrentSRC = GUILayout.Button("Copy Position to src");
                bool copyXCurrentDST = GUILayout.Button("Copy Position to dst");

                if (copyXCurrentSRC)
                    posX.FindPropertyRelative("src").floatValue = obj.RectTransform.anchoredPosition.x;

                if (copyXCurrentDST)
                    posX.FindPropertyRelative("dst").floatValue = obj.RectTransform.anchoredPosition.x;

                GUILayout.Space(18 * 3 + 9);
            }

            if (AnchorTool.IsStretchedVertically(obj.RectTransform) && obj.mode == UITweenPosition.Mode.Auto)
            {
                GUILayout.Label("Vertical Stretching Offset", EditorStyles.largeLabel);

                GUILayout.Label("Top", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(offsetMaxY.FindPropertyRelative("src"));
                EditorGUILayout.PropertyField(offsetMaxY.FindPropertyRelative("dst"));

                GUILayout.Space(9);

                GUILayout.Label("Bottom", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(offsetMinY.FindPropertyRelative("src"));
                EditorGUILayout.PropertyField(offsetMinY.FindPropertyRelative("dst"));


                bool copyYCurrentSRC = GUILayout.Button("Copy Stretch to src");
                bool copyYCurrentDST = GUILayout.Button("Copy Stretch to dst");

                if (copyYCurrentSRC)
                {
                    offsetMaxY.FindPropertyRelative("src").floatValue = -obj.RectTransform.offsetMax.y;
                    offsetMinY.FindPropertyRelative("src").floatValue = obj.RectTransform.offsetMin.y;
                }

                if (copyYCurrentDST)
                {
                    offsetMaxY.FindPropertyRelative("dst").floatValue = -obj.RectTransform.offsetMax.y;
                    offsetMinY.FindPropertyRelative("dst").floatValue = obj.RectTransform.offsetMin.y;
                }
            }
            else
            {
                GUILayout.Label("Vertical Position", EditorStyles.largeLabel);

                EditorGUILayout.PropertyField(posY.FindPropertyRelative("src"));
                EditorGUILayout.PropertyField(posY.FindPropertyRelative("dst"));

                bool copyYCurrentSRC = GUILayout.Button("Copy Position to src");
                bool copyYCurrentDST = GUILayout.Button("Copy Position to dst");

                if (copyYCurrentSRC)
                    posY.FindPropertyRelative("src").floatValue = obj.RectTransform.anchoredPosition.y;

                if (copyYCurrentDST)
                    posY.FindPropertyRelative("dst").floatValue = obj.RectTransform.anchoredPosition.y;

                GUILayout.Space(9);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}