using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


/// CanvasData는 Canvas Component에 대한 정보를 담고 있습니다.
/// (주의 : 캔버스는 반드시 RenderMode.ScreenSpaceCamera = MainCamera 로 설정되어 있어야 합니다.)
/// (주의 : 스케일러는 반드시 Factor = 1, PPU = 100 으로 설정되어 있어야 합니다)
public class CanvasData : DefaultData
{
    private Canvas canvas;
    private CanvasScaler scaler;
    private CanvasGroup canvasGroup;
    public float2 canvasSize { get; private set; }
    private ValueFlag<float> canvasAlpha;
    private ValueFlag<bool> interactable;
    private ValueFlag<bool> blockRaycast;


    public override void OnInit()
    {        
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        scaler = GetComponent<CanvasScaler>();
        
        if (canvas.renderMode != RenderMode.ScreenSpaceCamera) throw new Exception($"{hierarchyName} 캔버스의 RenderMode가 ScreenSpaceCamera가 아닙니다.");
        if (canvas.worldCamera != Camera.main) throw new Exception($"{hierarchyName} 캔버스의 렌더 카메라가 MainCamera가 아닙니다.");
        
        if (scaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize) throw new Exception($"{hierarchyName} 캔버스의 스케일러의 ScaleMode가 ScaleWithScreenSize가 아닙니다.");
        if (scaler.referencePixelsPerUnit != 100) throw new Exception($"{hierarchyName} 캔버스의 스케일러의 PPU가 100이 아닙니다.");

        canvasSize = GetComponent<Canvas>().pixelRect.size;
        canvasAlpha = new (canvasGroup.alpha);
        interactable = new (canvasGroup.interactable);
        blockRaycast = new (canvasGroup.blocksRaycasts);

        ComponentManager.AddComponent(hierarchyName, this);
    }



    /// Canvas의 투명도를 반환합니다.
    public float GetCanvasAlpha()
    {
        return canvasAlpha.value;
    }

    /// Canvas의 투명도를 변경합니다.
    public void SetCanvasAlpha(float value)
    {
        canvasAlpha.value = value;
    }

    /// Canvas의 상호작용 상태를 반환합니다.
    public bool GetInteractable()
    {
        return interactable.value;
    }

    /// Canvas의 상호작용 상태를 변경합니다.
    public void SetInteractable(bool value)
    {
        interactable.value = value;
    }

    /// Canvas의 레이캐스트 차단 상태를 반환합니다.
    public bool GetBlockRaycast()
    {
        return blockRaycast.value;
    }

    /// Canvas의 레이캐스트 차단 상태를 변경합니다.
    public void SetBlockRaycast(bool value)
    {
        blockRaycast.value = value;
    }


    public bool IsChanged()
    {
        return canvasAlpha.isChanged || interactable.isChanged || blockRaycast.isChanged;
    }


    public void Sync()
    {
        if(canvasAlpha.isChanged) 
        { 
            canvasGroup.alpha = canvasAlpha.value;
            canvasAlpha.isChanged = false; 
        }

        if(interactable.isChanged) 
        { 
            canvasGroup.alpha = canvasAlpha.value;
            interactable.isChanged = false; 
        }

        if(blockRaycast.isChanged) 
        { 
            canvasGroup.alpha = canvasAlpha.value;
            blockRaycast.isChanged = false; 
        }
    }
}