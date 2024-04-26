using UnityEngine;
using UnityEngine.UI;
using static Unity.Mathematics.math;


[RequireComponent(typeof(Image), typeof(Button))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData), typeof(SpriteData))]
public class SimpleButton : ObjectData
{
    private ValueFlag<float> progress;
    private ValueFlag<bool> isSelected;
    private ValueFlag<bool> interactable;
    private ValueFlag<bool> raycastTarget;
    public Button button { get; private set; }
    public Image image { get; private set; }
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public SpriteData spriteData { get; private set; }
    public EventData eventData { get; private set; }

    protected override void OnInit()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        progress = new (0);
        isSelected = new (false);
        interactable = new (button.interactable);
        raycastTarget = new (image.raycastTarget);
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        spriteData = ComponentManager.GetComponent<SpriteData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        eventData.AddTouchDataEvent(SelectEvent);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    public void SetInteractable(bool value)
    {
        interactable.value = value;
    }

    public bool GetInteractable()
    {
        return interactable.value;
    }

    public void SetRaycastTarget(bool value)
    {
        raycastTarget.value = value;
    }

    public bool GetRaycastTarget()
    {
        return raycastTarget.value;
    }


    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private bool IsSyncRequired()
    {
        return progress.isChanged || isSelected.isChanged || interactable.isChanged || raycastTarget.isChanged || gameObjectData.IsChanged() || rectTransformData.IsChanged() || spriteData.IsChanged();
    }


    private void Sync()
    {
        progress.isChanged = false;
        isSelected.isChanged = false;
        if(interactable.isChanged)
        {
            button.interactable = interactable.value;
            interactable.isChanged = false;
        }
        if(raycastTarget.isChanged)
        {
            image.raycastTarget = raycastTarget.value;
            raycastTarget.isChanged = false;
        }
        spriteData.Sync();
        rectTransformData.Sync();
        gameObjectData.Sync();
    }

    private void SelectEvent()
    {
        if(!TouchData.selectHierarchyName.Equals(hierarchyName)) return;

        if(TouchData.touchType != TouchData.TouchType.Up && progress.value < 1)
        {
            progress.value = min(progress.value + 1/10f, 1);
            var scale = lerp(1, 0.9f, progress.value);
            rectTransformData.SetSizeScale(scale);
        }
        
        if(TouchData.touchType == TouchData.TouchType.Up)
        {
            progress.value = 0;
            isSelected.value = false;
            rectTransformData.SetSizeScale(1);
        }
    }
}
