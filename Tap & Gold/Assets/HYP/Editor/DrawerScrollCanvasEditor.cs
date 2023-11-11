using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(DrawerScrollCanvas))]
public class DrawerScrollCanvasEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var component = target as DrawerScrollCanvas;
        VisualElement myInspector = new VisualElement();
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/HYP/VisualTree/DrawerScrollCanvas.uxml");
        visualTree.CloneTree(myInspector);
        return myInspector;
    }
}