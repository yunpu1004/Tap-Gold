using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(GameObjectData))]
[RequireComponent(typeof(EventData), typeof(TransformData), typeof(AnimationData))]
public class AnimationSprite : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public AnimationData animationData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        animationData = ComponentManager.GetComponent<AnimationData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }
 

    protected override void DataUpdate()
    {
        if(animationData.GetCurrentStateChanged())
        {
            var state = animationData.GetState();
            eventData.InvokeAnimationChangedEvent(state.before, state.current);
        }
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private bool IsSyncRequired() 
    {
        return gameObjectData.IsChanged() || transformData.IsChanged() || animationData.IsChanged();
    }

    
    private void Sync()
    {
        gameObjectData.Sync();
        transformData.Sync();
        animationData.Sync();
    }
}