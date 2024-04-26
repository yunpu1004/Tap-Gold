using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData), typeof(CanvasData))]
public class SimpleCanvas : ObjectData
{    
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public CanvasData canvasData { get; private set; }
    public EventData eventData { get; private set; }

    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        canvasData = ComponentManager.GetComponent<CanvasData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }

    public void SetDisplay(bool value)
    {
        gameObjectData.SetEnabled(value);
    }

    public bool GetDisplay()
    {
        return gameObjectData.GetEnabled();
    }


    private bool IsSyncRequired()
    {
        return gameObjectData.IsChanged() || rectTransformData.IsChanged() || canvasData.IsChanged();
    }


    private void Sync()
    {
        canvasData.Sync();
        rectTransformData.Sync();
        gameObjectData.Sync();
    }
}