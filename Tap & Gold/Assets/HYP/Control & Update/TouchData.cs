using System.Collections.Generic;
using UnityEngine;
using static Unity.Mathematics.math;
using float2 = Unity.Mathematics.float2;

/// TouchData는 입력에 대한 정보를 담고 있습니다.
/// 이 클래스는 UpdateManager의 FixedUpdate 에서만 업데이트됩니다.
public class TouchData
{
    public static TouchType touchType { get; private set;}
    public static float2 pixelDelta { get; private set;}
    public static float2 screenDelta { get; private set;}
    public static float2 worldDelta { get; private set;}

    public static float2 pointerPixelPos { get; private set;}
    public static float2 pointerScreenPos { get; private set;}
    public static float2 pointerWorldPos { get; private set;}
    public static string pointerHierarchyName {get; private set;}

    public static float2 selectPixelPos { get; private set;}
    public static float2 selectScreenPos { get; private set;}
    public static float2 selectWorldPos { get; private set;}
    public static string selectHierarchyName { get; private set;}

    private static List<GameObject> tempList = new List<GameObject>();


    public enum TouchType
    {
        None,
        Down,
        Hold,
        Up,
    }


    public static void Init()
    {
        touchType = TouchType.None;
        pixelDelta = 0;
        screenDelta = 0;
        worldDelta = 0;
        pointerPixelPos = 0;
        pointerScreenPos = 0;
        pointerWorldPos = 0;
        pointerHierarchyName = "";
        selectPixelPos = 0;
        selectScreenPos = 0;
        selectWorldPos = 0;
        selectHierarchyName = "";
        
        tempList.Clear();
    }


    public static void Update()
    {
        UpdateDefaultData();
        UpdatePointerObject();
        UpdateSelectObject();
        InvokeTouchDataEvent();
        InvokeDragEvent();
    }


    private static void UpdateDefaultData()
    {
        var prevInputType = touchType;
        var down = Input.GetMouseButtonDown(0);
        var hold = Input.GetMouseButton(0);
        var up = Input.GetMouseButtonUp(0);
        var input = down || hold || up;

        if((touchType == TouchType.None || touchType == TouchType.Up) && input) 
        {
            touchType = TouchType.Down; 
            pixelDelta = 0; 
            screenDelta = 0;
            worldDelta = 0;
        }
        else if(input) 
        {
            touchType = TouchType.Hold; 
            pixelDelta = (float2(Input.mousePosition.x, Input.mousePosition.y) - pointerPixelPos); 
            screenDelta = pixelDelta / AppData.screenSize;
            worldDelta = pixelDelta / (AppData.screenSize.y/2) * AppData.orthographicSize;
        }
        else if((touchType == TouchType.Down || touchType == TouchType.Hold) && !input) 
        {
            touchType = TouchType.Up; 
            pixelDelta = 0; 
            screenDelta = 0;
            worldDelta = 0;
        }
        else 
        {
            touchType = TouchType.None; 
            pixelDelta = 0; 
            screenDelta = 0;
            worldDelta = 0; 
        }

        // 이 코드는 비정상적인 입력을 찾기 위한 코드입니다. 오류 발생시 주석을 해제하고 사용합니다.
        // if(prevInputType == TouchType.Down && touchType == TouchType.Down) {Debug.Log($"Down Down ## down : {down}, hold : {hold}, up : {up}");} 
        // if(prevInputType == TouchType.Up && touchType == TouchType.Up) {Debug.Log($"Up Up ## down : {down}, hold : {hold}, up : {up}");}
        // if(prevInputType == TouchType.None && touchType == TouchType.Hold) {Debug.Log($"None Hold ## down : {down}, hold : {hold}, up : {up}");}
        // if(prevInputType == TouchType.Hold && touchType == TouchType.None) {Debug.Log($"Hold None ## down : {down}, hold : {hold}, up : {up}");}
    }
 

    private static void UpdatePointerObject()
    {
        if(touchType == TouchType.None)
        {
            pointerPixelPos = float2.zero;
            pointerScreenPos = float2.zero;
            pointerWorldPos = float2.zero;
            pointerHierarchyName = "";
        }

        else if(touchType == TouchType.Down || touchType == TouchType.Hold)
        {
            pointerPixelPos = float2(Input.mousePosition.x, Input.mousePosition.y);
            pointerScreenPos = pointerPixelPos / AppData.screenSize;
            pointerWorldPos = AppData.cameraPos.xy + (pointerPixelPos - AppData.screenSize / 2) / AppData.screenSize.y * 2 * AppData.orthographicSize;
            RaycastUtil.RaycastUI(pointerPixelPos, tempList);
            if(tempList.Count != 0)
            {
                var target = tempList[0];
                var hierarchyName = GameObjectUtil.GetHierarchyName(target);
                var gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
                pointerHierarchyName = (gameObjectData) ?hierarchyName : $"";
                return;
            }

            RaycastUtil.RaycastSprite(pointerWorldPos, tempList);
            if(tempList.Count != 0)
            {
                var target = tempList[0];
                var hierarchyName = GameObjectUtil.GetHierarchyName(target);
                var gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
                pointerHierarchyName = (gameObjectData) ?hierarchyName : $"";
                return;
            }

            pointerHierarchyName = $"";
        }
    }



    private static void UpdateSelectObject()
    {
        if(touchType == TouchType.None)
        {
            selectPixelPos = float2.zero;
            selectScreenPos = float2.zero;
            selectHierarchyName = "";
        }

        else if(touchType == TouchType.Down)
        {
            selectPixelPos = float2(Input.mousePosition.x, Input.mousePosition.y);
            selectScreenPos = selectPixelPos / AppData.screenSize;
            selectWorldPos = AppData.cameraPos.xy + (selectPixelPos - AppData.screenSize / 2) / AppData.screenSize.y * 2 * AppData.orthographicSize;

            RaycastUtil.RaycastUI(selectPixelPos, tempList);
            if(tempList.Count != 0)
            {
                var target = tempList[0];
                var hierarchyName = GameObjectUtil.GetHierarchyName(target);
                var gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
                selectHierarchyName = (gameObjectData) ?hierarchyName : $"";
                return;
            }

            RaycastUtil.RaycastSprite(selectWorldPos, tempList);
            if(tempList.Count != 0)
            {
                var target = tempList[0];
                var hierarchyName = GameObjectUtil.GetHierarchyName(target);
                var gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
                selectHierarchyName = (gameObjectData) ?hierarchyName : $"";
                return;
            }

            selectHierarchyName = $"";
        }
    }


    /// 터치가 발생한 오브젝트의 터치 이벤트를 호출합니다.
    private static void InvokeTouchDataEvent()
    {
        if(touchType == TouchType.None) return;
        if(selectHierarchyName == $"" && pointerHierarchyName == $"") return;

        if(pointerHierarchyName == $"")
        {
            if(ComponentManager.HasComponent<EventData>(selectHierarchyName))
            {
                ComponentManager.GetComponent<EventData>(selectHierarchyName).InvokeTouchDataEvent();
                return;
            }
        }

        if(selectHierarchyName == $"")
        {
            if(ComponentManager.HasComponent<EventData>(pointerHierarchyName))
            {
                ComponentManager.GetComponent<EventData>(pointerHierarchyName).InvokeTouchDataEvent();
                return;
            }
        }

        if(selectHierarchyName == pointerHierarchyName)
        {
            if(ComponentManager.HasComponent<EventData>(selectHierarchyName))
            {
                ComponentManager.GetComponent<EventData>(selectHierarchyName).InvokeTouchDataEvent();
                return;
            }
        }
        else
        {
            if(ComponentManager.HasComponent<EventData>(selectHierarchyName))
            {
                ComponentManager.GetComponent<EventData>(selectHierarchyName).InvokeTouchDataEvent();
                return;
            }

            if(ComponentManager.HasComponent<EventData>(pointerHierarchyName))
            {
                ComponentManager.GetComponent<EventData>(pointerHierarchyName).InvokeTouchDataEvent();
                return;
            }
        }
    }

    /// 드래그가 발생한 오브젝트의 드래그 이벤트를 호출합니다.
    private static void InvokeDragEvent()
    {
        if(touchType == TouchType.None) return;
        if(selectHierarchyName == $"") return;
        TransformData transformData;
        RectTransformData rectTransformData;

        if(rectTransformData = ComponentManager.GetComponent<RectTransformData>(selectHierarchyName))
        {
            var dragType = rectTransformData.GetDragType();
            if(dragType == DragType.Horizontal)
            {
                var pos = rectTransformData.GetAnchorPos();
                pos.x = pointerScreenPos.x;
                rectTransformData.SetAnchorPos(pos);
            }
            else if(dragType == DragType.Vertical)
            {
                var pos = rectTransformData.GetAnchorPos();
                pos.y = pointerScreenPos.y;
                rectTransformData.SetAnchorPos(pos);
            }
            else if(dragType == DragType.All)
            {
                var pos = rectTransformData.GetAnchorPos();
                pos.xy = pointerScreenPos;
                rectTransformData.SetAnchorPos(pos);
            }
            return;
        }

        if(transformData = ComponentManager.GetComponent<TransformData>(selectHierarchyName))
        {
            var dragType = transformData.GetDragType();
            if(dragType == DragType.Horizontal)
            {
                var pos = transformData.GetLocalPosition();
                pos.x = pointerWorldPos.x;
                transformData.SetLocalPosition(pos);
            }
            else if(dragType == DragType.Vertical)
            {
                var pos = transformData.GetLocalPosition();
                pos.y = pointerWorldPos.y;
                transformData.SetLocalPosition(pos);
            }
            else if(dragType == DragType.All)
            {
                var pos = transformData.GetLocalPosition();
                pos.xy = pointerWorldPos;
                transformData.SetLocalPosition(pos);
            }
            return;
        }
    }


    public static void Log(){
        Debug.Log($"mouseEventType:{touchType.ToString()} \n " +
            $"delta:{pixelDelta.ToString()} \n " +
            $"screenDelta:{screenDelta.ToString()} \n " +
            $"pointerPixelPos:{pointerPixelPos.ToString()} \n " +
            $"pointerScreenPos:{pointerScreenPos.ToString()} \n " +
            $"pointerWorldPos:{pointerWorldPos.ToString()} \n " +
            $"pointerObjectName:{pointerHierarchyName.ToString()} \n " +
            $"selectPixelPos:{selectPixelPos.ToString()} \n " +
            $"selectScreenPos:{selectScreenPos.ToString()} \n " +
            $"selectWorldPos:{selectWorldPos.ToString()} \n " +
            $"selectObjectName:{selectHierarchyName.ToString()} \n ");
    }
}
 


public enum DragType
{
    None,
    Horizontal,
    Vertical,
    All
}
