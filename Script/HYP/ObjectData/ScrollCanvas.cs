using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData), typeof(CanvasData))]
public class ScrollCanvas : ObjectData
{    
    private ScrollRect scrollRect;
    private RectTransform content;
    private RectTransform[] contentChildren;
    private float normalizedViewportHeight;
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public CanvasData canvasData { get; private set; }
    public EventData eventData { get; private set; }

    protected override void OnInit()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        content = scrollRect.content;
        contentChildren = GameObjectUtil.GetChildrenComponents<RectTransform>(content.gameObject);
        normalizedViewportHeight = scrollRect.viewport.rect.height / content.rect.height;
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


    private bool IsSyncRequired()
    {
        return gameObjectData.IsChanged() || canvasData.IsChanged() || rectTransformData.IsChanged();
    }


    private void Sync()
    {
        canvasData.Sync();
        rectTransformData.Sync();
        gameObjectData.Sync();
    }

    public void CheckContent(Vector2 pos)
    {
        normalizedViewportHeight = scrollRect.viewport.rect.height / content.rect.height;
        
        float yAnchorMin = 1 - normalizedViewportHeight;
        foreach (RectTransform child in contentChildren)
        {
            if(!child.gameObject.activeSelf) continue;
            var minmax = RectTransformUtil.GetLocalNormalizedMinMax(child, content);
            yAnchorMin = Mathf.Min(yAnchorMin, minmax.min.y);
        }
        
        float yAnchorMinNormalized = Mathf.InverseLerp(0, 1 - normalizedViewportHeight, yAnchorMin);
        
        scrollRect.verticalNormalizedPosition = Mathf.Max(scrollRect.verticalNormalizedPosition, yAnchorMinNormalized);
    }
}