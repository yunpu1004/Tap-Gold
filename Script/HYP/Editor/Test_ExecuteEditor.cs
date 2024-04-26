using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Test_Execute))]
public class Test_ExecuteEditor : Editor
{

    public override VisualElement CreateInspectorGUI()
    {
        var component = target as Test_Execute;
        VisualElement myInspector = new VisualElement();
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/HYP/VisualTree/Test_Execute.uxml");
        visualTree.CloneTree(myInspector);

        var button = myInspector.Q<Button>("Button");
        button.clickable.clicked += component.Test;

        var imGUIContainer = myInspector.Q<IMGUIContainer>("IMGUIContainer");
        imGUIContainer.onGUIHandler += () =>
        {
            base.OnInspectorGUI();
        };

        return myInspector;
    }
}