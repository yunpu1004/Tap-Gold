using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Mathematics.math;


/// Carousel은 10개의 페이지와 점 오브젝트로 이루어진 UI 입니다.
/// 최초 실행시 interactable = true, currentIndex = 0, maxPage = 10으로 초기화 됩니다.
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(GameObjectData))]
[RequireComponent(typeof(EventData), typeof(RectTransformData))]
public class Carousel : ObjectData
{
    public Image background;
    public Button leftButton;
    public Button rightButton;
    public Image pages;
    public RectTransform dots;

    public GameObjectData gameObjectData {get; private set;}
    public RectTransformData rectTransformData {get; private set;}
    public EventData eventData {get; private set;}

    [HideInInspector] public RectTransform[] pageRectArray;
    [HideInInspector] public RectTransform[] dotRectArray;
    [HideInInspector] public Image[] dotImageArray;
    [HideInInspector] public float4[] pageAnchorArray;
    [HideInInspector] public float4[] dotsAnchorArray;
    [HideInInspector] public Color[] dotColorArray;

    [SerializeField, HideInInspector] private bool interactable_Serialized;
    [SerializeField, HideInInspector] private int currentIndex_Serialized;
    [SerializeField, HideInInspector] private int maxPage_Serialized;

    private ValueFlag<bool> interactable;
    private ValueFlag<int> currentIndex;
    private ValueFlag<int> maxPage;

    private ValueFlag<float2> dotRectAnchorSize;


    protected override void OnInit()
    {
        SetData();
        interactable = new (interactable_Serialized);
        currentIndex = new (currentIndex_Serialized);
        maxPage = new (maxPage_Serialized);

        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    private void SetData()
    {
        pageRectArray = GameObjectUtil.GetChildrenComponents<RectTransform>(pages.gameObject);
        dotRectArray = GameObjectUtil.GetChildrenComponents<RectTransform>(dots.gameObject);
        dotImageArray = GameObjectUtil.GetChildrenComponents<Image>(dots.gameObject);
        pageAnchorArray = ArrayUtil.Select(pageRectArray, x => float4(x.anchorMin, x.anchorMax));
        dotsAnchorArray = ArrayUtil.Select(dotRectArray, x => float4(x.anchorMin, x.anchorMax));
        dotColorArray = ArrayUtil.Select(dotImageArray, x => x.color);
        var dotRectPixelSize = dotRectArray[0].parent.GetComponent<RectTransform>().rect.size;
        dotRectAnchorSize = new(float2(dotRectPixelSize.y/dotRectPixelSize.x,1)); 
    }


    public void ApplyDataOnEditor(bool interactable, int currentIndex, int maxPage)
    {
        SetData();
        var firstDotPos = float2((-maxPage + 1) * dotRectAnchorSize.value.x / 2, 0) + 0.5f;
        var xMin = firstDotPos.x - dotRectAnchorSize.value.x / 2;
        var xMax = firstDotPos.x + dotRectAnchorSize.value.x / 2;
        var yMin = firstDotPos.y - dotRectAnchorSize.value.y / 2;
        var yMax = firstDotPos.y + dotRectAnchorSize.value.y / 2;
        var firstDotMinMax = float4(xMin, yMin, xMax, yMax);

        for (int i = 0; i < 10; i++)
        {
            dotsAnchorArray[i] = firstDotMinMax + float4(dotRectAnchorSize.value.x * i, 0, dotRectAnchorSize.value.x * i, 0);
            if(i >= maxPage) dotsAnchorArray[i] = float4(-1000, -1000, -1000, -1000);
        }

        for (int i = 0; i < 10; i++)
        {
            var min = dotsAnchorArray[i].xy;
            var max = dotsAnchorArray[i].zw;
            var mid = (min + max) / 2;
            min = lerp(mid, min, 0.7f);
            max = lerp(mid, max, 0.7f);
            dotsAnchorArray[i] = float4(min, max);
        }

        for (int i = 0; i < 10; i++)
        {
            dotColorArray[i] = i == currentIndex ? Color.white : Color.gray;
        }

        var firstPageMinMax = float4(currentIndex, 0, currentIndex + 1, 1);
        for (int i = 0; i < 10; i++)
        {
            pageAnchorArray[i] = firstPageMinMax + float4(-i, 0, -i, 0);
        }

        for (int i = 0; i < 10; i++)
        {
            pageRectArray[i].anchorMin = pageAnchorArray[i].xy;
            pageRectArray[i].anchorMax = pageAnchorArray[i].zw;
            pageRectArray[i].offsetMax = Vector2.zero;
            pageRectArray[i].offsetMin = Vector2.zero;
        }

        for (int i = 0; i < 10; i++)
        {
            dotRectArray[i].anchorMin = dotsAnchorArray[i].xy;
            dotRectArray[i].anchorMax = dotsAnchorArray[i].zw;
            dotRectArray[i].offsetMax = Vector2.zero;
            dotRectArray[i].offsetMin = Vector2.zero;
        }

        for (int i = 0; i < 10; i++)
        {
            dotImageArray[i].color = dotColorArray[i];
        }
    }


    public void LeftButton()
    {
        if(currentIndex.value <= 0) return;
        SetData();
        SetCurrentIndex(currentIndex.value - 1);
    }

    public void RightButton()
    {
        if(currentIndex.value >= maxPage.value - 1) return;
        SetData();
        SetCurrentIndex(currentIndex.value + 1);
    }


    public void SetInteractable(bool value)
    {
        interactable.value = value;
    }

    public bool GetInteractable()
    {
        return interactable.value;
    }

    public void SetCurrentIndex(int value)
    {
        if (value < 0 || value >= maxPage.value) return;
        currentIndex.value = value;
        SetData();
        ApplyDataOnEditor(interactable.value, currentIndex.value, maxPage.value);
    }

    public int GetCurrentIndex()
    {
        return currentIndex.value;
    }

    public void SetMaxPage(int value)
    {
        if (value < 1 || value > 10) return;
        maxPage.value = value;
        SetData();
        ApplyDataOnEditor(interactable.value, currentIndex.value, maxPage.value);
    }

    public int GetMaxPage()
    {
        return maxPage.value;
    }


    protected override void DataUpdate()
    {
        if(currentIndex.isChanged || maxPage.isChanged || interactable.isChanged)
        {
            var firstDotPos = float2((-maxPage.value + 1) * dotRectAnchorSize.value.x / 2, 0) + 0.5f;
            var xMin = firstDotPos.x - dotRectAnchorSize.value.x / 2;
            var xMax = firstDotPos.x + dotRectAnchorSize.value.x / 2;
            var yMin = firstDotPos.y - dotRectAnchorSize.value.y / 2;
            var yMax = firstDotPos.y + dotRectAnchorSize.value.y / 2;
            var firstDotMinMax = float4(xMin, yMin, xMax, yMax);

            for (int i = 0; i < 10; i++)
            {
                dotsAnchorArray[i] = firstDotMinMax + float4(dotRectAnchorSize.value.x * i, 0, dotRectAnchorSize.value.x * i, 0);
                if(i >= maxPage.value) dotsAnchorArray[i] = float4(-1000, -1000, -1000, -1000);
            }

            for (int i = 0; i < 10; i++)
            {
                var min = dotsAnchorArray[i].xy;
                var max = dotsAnchorArray[i].zw;
                var mid = (min + max) / 2;
                min = lerp(mid, min, 0.7f);
                max = lerp(mid, max, 0.7f);
                dotsAnchorArray[i] = float4(min, max);
            }

            for (int i = 0; i < 10; i++)
            {
                dotColorArray[i] = i == currentIndex.value ? Color.white : Color.gray;
            }

            var firstPageMinMax = float4(currentIndex.value, 0, currentIndex.value + 1, 1);
            for (int i = 0; i < 10; i++)
            {
                pageAnchorArray[i] = firstPageMinMax + float4(-i, 0, -i, 0);
            }
        }
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private bool IsSyncRequired()
    {
        return currentIndex.isChanged || maxPage.isChanged || interactable.isChanged || gameObjectData.IsChanged() || rectTransformData.IsChanged();
    }

    private void Sync()
    {
        if(currentIndex.isChanged || maxPage.isChanged || interactable.isChanged)
        {
            for (int i = 0; i < 10; i++)
            {
                pageRectArray[i].anchorMin = pageAnchorArray[i].xy;
                pageRectArray[i].anchorMax = pageAnchorArray[i].zw;
                pageRectArray[i].offsetMax = Vector2.zero;
                pageRectArray[i].offsetMin = Vector2.zero;
            }

            for (int i = 0; i < 10; i++)
            {
                dotRectArray[i].anchorMin = dotsAnchorArray[i].xy;
                dotRectArray[i].anchorMax = dotsAnchorArray[i].zw;
                dotRectArray[i].offsetMax = Vector2.zero;
                dotRectArray[i].offsetMin = Vector2.zero;
            }

            for (int i = 0; i < 10; i++)
            {
                dotImageArray[i].color = dotColorArray[i];
            }

            currentIndex.isChanged = false;
            maxPage.isChanged = false;
            interactable.isChanged = false;
            dotRectAnchorSize.isChanged = false;
        }

        rectTransformData.Sync();
        gameObjectData.Sync();
    }
}


