using TMPro;
using UnityEditor;

[CustomEditor(typeof(ValueBar)), CanEditMultipleObjects]
public class ValueBarEditor : Editor
{
    SerializedProperty barValueProperty;
    SerializedProperty decimalTypeProperty;

    void OnEnable()
    {
        barValueProperty = serializedObject.FindProperty("barValue_Serialize");
        decimalTypeProperty = serializedObject.FindProperty("decimalType_Serialize");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        float currentValue = EditorGUILayout.Slider("BarValue", barValueProperty.floatValue, 0, 1);
        DecimalType decimalType = (DecimalType)EditorGUILayout.EnumPopup("DecimalType", (DecimalType)decimalTypeProperty.enumValueIndex);

        if(currentValue != barValueProperty.floatValue || decimalType != (DecimalType)decimalTypeProperty.enumValueIndex)
        {
            ValueBar component = target as ValueBar;
            barValueProperty.floatValue = currentValue;
            decimalTypeProperty.enumValueIndex = (int)decimalType;
            component._ApplyValueFromEditor(currentValue, decimalType);
            serializedObject.ApplyModifiedProperties();
            PrefabUtility.RecordPrefabInstancePropertyModifications(component.GetComponentInChildren<TextMeshProUGUI>());
        }
    }
}