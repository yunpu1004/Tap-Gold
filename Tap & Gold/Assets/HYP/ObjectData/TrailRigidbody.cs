using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D),typeof(BoxCollider2D))]
[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(GameObjectData), typeof(EventData), typeof(TransformData))]
[RequireComponent(typeof(RigidbodyData), typeof(ColliderData), typeof(TrailData))]
public class TrailRigidbody : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public RigidbodyData rigidbodyData { get; private set; }
    public ColliderData colliderData { get; private set; }
    public TrailData trailData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        rigidbodyData = ComponentManager.GetComponent<RigidbodyData>(hierarchyName);
        colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
        trailData = ComponentManager.GetComponent<TrailData>(hierarchyName);
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
               colliderData.IsChanged() ||
               rigidbodyData.IsChanged() ||
               trailData.IsChanged() ||
               gameObjectData.IsChanged();
    }


    private void Sync()
    {
        transformData.Sync();
        colliderData.Sync();
        rigidbodyData.Sync();
        trailData.Sync();
        gameObjectData.Sync();
    }
}