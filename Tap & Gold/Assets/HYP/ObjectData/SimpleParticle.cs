using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(TransformData), typeof(ParticleData))]
public class SimpleParticle : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public ParticleData particleData { get; private set; }
    public EventData eventData { get; private set; }

    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        particleData = ComponentManager.GetComponent<ParticleData>(hierarchyName);
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
        return gameObjectData.IsChanged() ||
               transformData.IsChanged() ||
               particleData.IsChanged();
    }


    private void Sync()
    {
        if (gameObjectData.GetEnabled())
        {
            gameObjectData.Sync();
            transformData.Sync();
            particleData.Sync();
        }
        else
        {
            transformData.Sync();
            particleData.Sync();
            gameObjectData.Sync();
        }
    }
}