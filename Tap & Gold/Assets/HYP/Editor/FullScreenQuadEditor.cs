using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(FullScreenQuad))]
public class FullScreenQuadEditor : Editor
{

    public override VisualElement CreateInspectorGUI()
    {
        var component = target as FullScreenQuad;
        VisualElement myInspector = new VisualElement();
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/HYP/VisualTree/FullScreenQuad.uxml");
        visualTree.CloneTree(myInspector);
        var button = myInspector.Q<Button>("Button");
        button.clickable.clicked += component.GenerateQuad;
        return myInspector;
    }
}