using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(SortingGroup))]
[RequireComponent(typeof(FullScreenQuad))]
[RequireComponent(typeof(GameObjectData), typeof(ShaderData), typeof(MeshData))]
[RequireComponent(typeof(EventData), typeof(TransformData))]
public class Filter : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public ShaderData shaderData { get; private set; }
    public MeshData meshData { get; private set; }
    public EventData eventData { get; private set; }

    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        shaderData = ComponentManager.GetComponent<ShaderData>(hierarchyName);
        meshData = ComponentManager.GetComponent<MeshData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }

    private bool IsSyncRequired()
    {
        return gameObjectData.IsChanged() || transformData.IsChanged() || shaderData.IsChanged() || meshData.IsChanged();
    }


    private void Sync()
    {
        gameObjectData.Sync();
        transformData.Sync();
        shaderData.Sync();
        meshData.Sync();
    }
}