using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
[RequireComponent(typeof(Image), typeof(GameObjectData))]
[RequireComponent(typeof(EventData), typeof(RectTransformData), typeof(SpriteData))]
public class SimpleImage : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public SpriteData spriteData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        spriteData = ComponentManager.GetComponent<SpriteData>(hierarchyName);
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
        return gameObjectData.IsChanged() || rectTransformData.IsChanged() || spriteData.IsChanged();
    }

    private void Sync()
    {
        spriteData.Sync();
        rectTransformData.Sync();
        gameObjectData.Sync();
    }
}

