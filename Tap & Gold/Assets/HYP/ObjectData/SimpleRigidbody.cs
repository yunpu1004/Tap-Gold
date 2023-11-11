using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D),typeof(BoxCollider2D))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(TransformData), typeof(RigidbodyData), typeof(ColliderData))]
public class SimpleRigidbody : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public RigidbodyData rigidbodyData { get; private set; }
    public ColliderData colliderData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        rigidbodyData = ComponentManager.GetComponent<RigidbodyData>(hierarchyName);
        colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
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
        return transformData.IsChanged() || colliderData.IsChanged() || rigidbodyData.IsChanged() || gameObjectData.IsChanged();
    }


    private void Sync()
    {
        transformData.Sync();
        colliderData.Sync();
        rigidbodyData.Sync();
        gameObjectData.Sync();
    }
} 