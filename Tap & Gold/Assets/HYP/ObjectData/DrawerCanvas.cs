using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Mathf;


[RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
[RequireComponent(typeof(GameObjectData))]
[RequireComponent(typeof(EventData), typeof(RectTransformData), typeof(CanvasData))]
public class DrawerCanvas : ObjectData
{
    [SerializeField] private Vector2 offAnchorMin; 
    [SerializeField] private Vector2 offAnchorMax;
    [SerializeField] private Vector2 onAnchorMin;
    [SerializeField] private Vector2 onAnchorMax;

    private ValueFlag<bool> display;
    private ValueFlag<float>  progress;

    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public CanvasData canvasData { get; private set; }
    public EventData eventData { get; private set; }



    protected override void OnInit()
    {
        display = new (enabled);
        progress = new ((enabled) ?1f :0f);

        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        canvasData = ComponentManager.GetComponent<CanvasData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);

        ComponentManager.AddComponent(hierarchyName, this);
    }



    /// Drawer의 화면 표시상태를 반환합니다.
    public bool GetDisplay()
    {
        return display.value;
    }

    /// Drawer의 화면 표시상태를 설정합니다. (progress가 0 또는 1 인 상태에서만 작동합니다.)
    public void SetDisplay(bool value)
    {
        if(progress.value > 0 && progress.value < 1) {Debug.Log("progress 이 0 과 1 사이의 값이 아닙니다."); return;}
        display.value = value;
    }


    protected override void DataUpdate()
    {
        if(display.value && progress.value < 1)
        {
            SetProgress(Min(GetProgress() + 0.1f, 1));
        }
        else if(!display.value && progress.value > 0)
        {
            SetProgress(Max(GetProgress() - 0.1f, 0));
        }

        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private float GetProgress()
    {
        return progress.value;
    }

    private void SetProgress(float value)
    {
        if(value < 0 || value > 1) {Debug.Log("progress 이 0 과 1 사이의 값이 아닙니다."); return;}

        rectTransformData.SetAnchorMin(TweenUtil.Ease(offAnchorMin, onAnchorMin, value));
        rectTransformData.SetAnchorMax(TweenUtil.Ease(offAnchorMax, onAnchorMax, value));

        if(value.Equals(1)) 
        {
            canvasData.SetInteractable(true);
        }
        else 
        {
            canvasData.SetInteractable(false);
        }

        if(value.Equals(0)) 
        {
            gameObjectData.SetEnabled(false);
        }
        else 
        {
            gameObjectData.SetEnabled(true);
        }

        progress.value = value;
    }


    private bool IsSyncRequired()
    {
        return canvasData.IsChanged() || gameObjectData.IsChanged() || rectTransformData.IsChanged();
    }


    private void Sync()
    {
        if(display.isChanged) display.isChanged = false;
        if(progress.isChanged) progress.isChanged = false;
        canvasData.Sync();
        rectTransformData.Sync();
        gameObjectData.Sync();
    }
}
