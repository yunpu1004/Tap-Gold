using UnityEditor;


[CustomEditor(typeof(RadioButton))]
public class RadioButtonEditor : Editor
{
    SerializedProperty valueProperty;

    void OnEnable()
    {
        valueProperty = serializedObject.FindProperty("activatedChild_Serialized");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RadioButton component = target as RadioButton;
        int oldValue = valueProperty.intValue;
        int newValue = EditorGUILayout.IntSlider("ActivatedChild", oldValue, 0, component.children.Length - 1);
        
        if(newValue != oldValue)
        {
            component.ApplyActivatedChild(newValue);
            valueProperty.intValue = newValue;
            serializedObject.ApplyModifiedProperties();

            for (int i = 0; i < component.children.Length; i++)
            {
                PrefabUtility.RecordPrefabInstancePropertyModifications(component.children[i]);
            }
        }
    }
}