using System;

/// EventData는 동적으로 등록되는 이벤트에 대한 정보를 담고 있습니다.
public class EventData : DefaultData
{
    private event Action touchDataEvent;
    private event Action dataUpdateEvent;
    private event Action<string, string> animationChangedEvent;
    


    public override void OnInit()
    {
        ComponentManager.AddComponent(hierarchyName, this);
    }


    /// 오브젝트를 터치하면 등록된 이벤트가 TouchData 에서 실행됩니다.
    public void InvokeTouchDataEvent() 
    {
        touchDataEvent?.Invoke();
    }

    public void AddTouchDataEvent(in Action value)
    {
        touchDataEvent += value;
    }

    /// 활성화된 오브젝트의 업데이트 이벤트가 매프레임 마다 실행됩니다.
    public void InvokeDataUpdateEvent()
    {
        dataUpdateEvent?.Invoke();
    }

    public void AddDataUpdateEvent(in Action value)
    {
        dataUpdateEvent += value;
    }

    /// 애니메이션의 상태가 변경되면 등록된 이벤트가 실행됩니다.
    /// before : 이전 애니메이션 State
    /// after : 현재 애니메이션 State
    public void InvokeAnimationChangedEvent(string before, string after)
    {
        animationChangedEvent?.Invoke(before, after);
    }

    /// 애니메이션의 상태가 변경되면 실행될 이벤트를 추가합니다.
    /// before : 이전 애니메이션 State
    /// after : 현재 애니메이션 State
    public void AddAnimationChangedEvent(Action<string, string> value)
    {
        animationChangedEvent += value;
    }
}