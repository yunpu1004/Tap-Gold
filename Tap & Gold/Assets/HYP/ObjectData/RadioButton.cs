using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnchorFitter), typeof(RectTransform))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData))]
public class RadioButton : ObjectData
{
    [SerializeField, HideInInspector] private int activatedChild_Serialized;
    public Toggle[] children;
    private ValueFlag<int> activatedChild;
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        children = GetComponentsInChildren<Toggle>();
        bool hasActivated = false;
        if(children.Length == 0) return;
        for (int i = 0; i < children.Length; i++)
        {
            int temp = i;
            if(children[i].isOn && !hasActivated) hasActivated = true;
            else if(children[i].isOn && hasActivated) throw new Exception("RadioButton에는 하나의 토글만 활성화되어야 합니다.");
            if(children[i].interactable == children[i].isOn) throw new Exception("RadioButton에 속한 토글은 활성화 상태와 입력 가능 상태가 달라야 합니다.");
            children[i].onValueChanged.AddListener((isOn) => activatedChild.value = temp);
        }
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        activatedChild = new ValueFlag<int>(activatedChild_Serialized);
        ComponentManager.AddComponent(hierarchyName, this);
    }

    public void ApplyActivatedChild(int value)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetIsOnWithoutNotify(i == value);
            children[i].interactable = i != value;
        }
    }
    
    public int GetActivatedChild()
    {
        return activatedChild.value;
    }

    public void SetActivatedChild(int index)
    {
        if(index < 0 || index >= children.Length) return;
        activatedChild.value = index;
    }

    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }
    
    private bool IsSyncRequired()
    {
        return gameObjectData.IsChanged() || rectTransformData.IsChanged() || activatedChild.isChanged;
    }

    private void Sync()
    {
        gameObjectData.Sync();
        rectTransformData.Sync();
        if(activatedChild.isChanged) 
        {
            ApplyActivatedChild(activatedChild.value);
            activatedChild.isChanged = false;
        }
    }
}
