using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(TransformData), typeof(ShaderData))]
public partial class SimpleShader : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public ShaderData shaderData { get; private set; }
    public EventData eventData { get; private set; }



    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        shaderData = ComponentManager.GetComponent<ShaderData>(hierarchyName);
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
        return transformData.IsChanged() ||
               shaderData.IsChanged() ||
               gameObjectData.IsChanged();
    }

    
    private void Sync()
    {
        transformData.Sync();
        shaderData.Sync();
        gameObjectData.Sync();
    }
}