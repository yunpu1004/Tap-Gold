using UnityEngine;
using UnityEngine.EventSystems;
using System;

// 포인터 클릭 이벤트를 처리하는 클래스입니다
public class PointerClickHandler : MonoBehaviour, IPointerClickHandler
{
    public event Action<PointerEventData> OnPointerClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(eventData);
    }
}
