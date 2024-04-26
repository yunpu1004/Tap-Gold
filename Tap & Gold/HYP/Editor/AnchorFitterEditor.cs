using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnchorFitter)), CanEditMultipleObjects]
public class AnchorFitterEditor : Editor
{
    void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseUp)
        {
            AnchorFitter anchorFitter = target as AnchorFitter;
            anchorFitter.GuiEvent();
        }
    }
}
