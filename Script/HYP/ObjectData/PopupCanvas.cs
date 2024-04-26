using System;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Mathematics.math;


[RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData), typeof(CanvasData))]
public class PopupCanvas : ObjectData
{
    [SerializeField] private TweenStyle tweenStyle; 
    private ValueFlag<bool> display;
    private ValueFlag<float> progress;
    
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

        display = new (gameObjectData.GetEnabled());
        progress = new ((gameObjectData.GetEnabled()) ?1f :0f);

        ComponentManager.AddComponent(hierarchyName, this);
    }


    /// Popup의 진행정도를 0~1 사이의 값으로 반환합니다. (0: 닫힘, 1: 열림)
    public float GetProgress()
    {
        return progress.value;
    }

    /// Popup의 진행정도를 0~1 사이의 값으로 변경합니다. (0: 닫힘, 1: 열림)
    public void SetProgress(float value)
    {
        if(value < 0 || value > 1) {Debug.Log($"progress 이 0 과 1 사이의 값이 아닙니다."); return;}

        progress.value = value;

        float scaleValue = value;
        if(tweenStyle == TweenStyle.Linear) scaleValue = value;
        else if(tweenStyle == TweenStyle.Sin) scaleValue = TweenUtil.Sin(0, 1, value);
        else if(tweenStyle == TweenStyle.Ease) scaleValue = TweenUtil.Ease(0, 1, value);
        else if(tweenStyle == TweenStyle.EaseBack) scaleValue = TweenUtil.EaseBack(0, 1, value);

        canvasData.SetCanvasAlpha(scaleValue);

        rectTransformData.SetSizeScale(scaleValue);

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
    }


    /// Popup의 화면 표시상태를 반환합니다.
    public bool GetDisplay()
    {
        return display.value;
    }

    /// Popup의 화면 표시상태를 변경합니다.
    public void SetDisplay(bool value)
    {
        if(progress.value > 0 && progress.value < 1) {Debug.Log($"progress 이 0 과 1 사이의 값이 아닙니다."); return;}
        if(!display.value && value) {gameObjectData.SetEnabled(true); display.value = value;}
        else if(display.value && !value) {display.value = value;}
    }


    protected override void DataUpdate()
    {
        if(display.value && progress.value < 1)
        {
            if(tweenStyle == TweenStyle.None) SetProgress(1);
            else {SetProgress(clamp(GetProgress() + 0.25f, 0, 1));}

        }
        else if(!display.value && progress.value > 0)
        {
            if(tweenStyle == TweenStyle.None) SetProgress(0);
            else {SetProgress(clamp(GetProgress() - 0.25f, 0, 1));}
        }
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private bool IsSyncRequired()
    {
        return canvasData.IsChanged() || gameObjectData.IsChanged() || rectTransformData.IsChanged();
    }


    private void Sync()
    {
        display.isChanged = false;
        progress.isChanged = false;
        canvasData.Sync();
        rectTransformData.Sync();
        gameObjectData.Sync();
    }
}