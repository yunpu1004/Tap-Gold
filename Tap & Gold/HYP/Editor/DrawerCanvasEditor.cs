using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(DrawerCanvas))]
public class DrawerCanvasEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var component = target as DrawerCanvas;
        VisualElement myInspector = new VisualElement();
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/HYP/VisualTree/DrawerCanvas.uxml");
        visualTree.CloneTree(myInspector);
        return myInspector;
    }
}