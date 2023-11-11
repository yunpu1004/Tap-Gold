using UnityEditor;


[CustomEditor(typeof(SliderBar)), CanEditMultipleObjects]
public class SliderBarEditor : Editor
{
    SerializedProperty valueProperty;

    void OnEnable()
    {
        valueProperty = serializedObject.FindProperty("barValue_Serialized");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        float currentValue = EditorGUILayout.Slider("BarValue", valueProperty.floatValue, 0, 1);

        if(currentValue != valueProperty.floatValue)
        {
            SliderBar component = target as SliderBar;
            valueProperty.floatValue = currentValue;
            component.ApplyValue(currentValue);
            serializedObject.ApplyModifiedProperties();
        }
    }
}