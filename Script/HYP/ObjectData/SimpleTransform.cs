using UnityEngine;


[RequireComponent(typeof(EventData), typeof(TransformData), typeof(GameObjectData))]
public class SimpleTransform : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
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
        return gameObjectData.IsChanged() || transformData.IsChanged();
    }

    private void Sync()
    {
        transformData.Sync();
        gameObjectData.Sync();
    }
}
