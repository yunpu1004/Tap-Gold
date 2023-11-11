using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Water))]
public class WaterEntityEditor : Editor
{
    void OnSceneGUI()
    {
        base.OnInspectorGUI();

        if(Event.current.type == EventType.MouseUp)
        {
            Water waterEntity = target as Water;
            waterEntity.SetMaterialProperty();
        }
    }
}
