using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Image), typeof(Button))]
[RequireComponent(typeof(GameObjectData))]
[RequireComponent(typeof(EventData), typeof(RectTransformData), typeof(AnimationData))]
public class AnimationButton : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public AnimationData animationData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
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
        return gameObjectData.IsChanged() || rectTransformData.IsChanged() || animationData.IsChanged();
    }


    private void Sync()
    {
        gameObjectData.Sync();
        rectTransformData.Sync();
        animationData.Sync();
    }
}