using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(RectTransformData))]
public class RectTransformDataEditor : Editor
{
    SerializedProperty valueProperty;

    void OnEnable()
    {
        valueProperty = serializedObject.FindProperty("panelRectTransform");
    }

    public override void OnInspectorGUI()
    {
        var component = target as RectTransformData; 
        if(component.gameObject.GetComponent<Canvas>() != null)
        {
            base.OnInspectorGUI();
        }

    }
}