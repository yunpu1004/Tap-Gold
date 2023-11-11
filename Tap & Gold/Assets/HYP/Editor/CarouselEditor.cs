using UnityEditor;


[CustomEditor(typeof(Carousel))]
public class CarouselEditor : Editor
{
    bool show;
    SerializedProperty interactableProp;
    SerializedProperty currentIndexProp;
    SerializedProperty maxPageProp;


    void OnEnable()
    {
        interactableProp = serializedObject.FindProperty("interactable_Serialized");
        currentIndexProp = serializedObject.FindProperty("currentIndex_Serialized");
        maxPageProp = serializedObject.FindProperty("maxPage_Serialized");
    }

    public override void OnInspectorGUI()
    {
        Carousel component = target as Carousel;

        bool interactable_oldValue = interactableProp.boolValue;
        int currentPage_oldValue = currentIndexProp.intValue;
        int maxPage_oldValue = maxPageProp.intValue;

        bool interactable_newValue = EditorGUILayout.Toggle("Interactable", interactable_oldValue);
        int maxPage_newValue = EditorGUILayout.IntSlider("Max Page", maxPage_oldValue, 1, 10);
        int currentPage_newValue = EditorGUILayout.IntSlider("Current Index", currentPage_oldValue, 0, maxPage_newValue-1);

        show = EditorGUILayout.Foldout(show, "Objects");
        if(show)
        {
            EditorGUI.indentLevel++;
            base.OnInspectorGUI();
            EditorGUI.indentLevel--;
        }

        if(interactable_oldValue == interactable_newValue && currentPage_oldValue == currentPage_newValue && maxPage_oldValue == maxPage_newValue) return;
        interactableProp.boolValue = interactable_newValue;
        currentIndexProp.intValue = currentPage_newValue;
        maxPageProp.intValue = maxPage_newValue;
        serializedObject.ApplyModifiedProperties();

        component.ApplyDataOnEditor(interactable_newValue, currentPage_newValue, maxPage_newValue);
        if(component.background != null) PrefabUtility.RecordPrefabInstancePropertyModifications(component.background);
        if(component.leftButton != null) PrefabUtility.RecordPrefabInstancePropertyModifications(component.leftButton);
        if(component.rightButton != null) PrefabUtility.RecordPrefabInstancePropertyModifications(component.rightButton);
        if(component.pages != null) PrefabUtility.RecordPrefabInstancePropertyModifications(component.pages);
        if(component.dots != null) PrefabUtility.RecordPrefabInstancePropertyModifications(component.dots);
        for (int i = 0; i < 10; i++)
        {
            PrefabUtility.RecordPrefabInstancePropertyModifications(component.pageRectArray[i]);
            PrefabUtility.RecordPrefabInstancePropertyModifications(component.dotRectArray[i]);
            PrefabUtility.RecordPrefabInstancePropertyModifications(component.dotImageArray[i]);
        }

        EditorUtility.SetDirty(component);
    }
}