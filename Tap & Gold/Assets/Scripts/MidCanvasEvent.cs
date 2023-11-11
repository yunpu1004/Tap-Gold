using Unity.Mathematics;
using UnityEngine;

/// 이 cs파일은 유저가 탭을 하는 영역에 해당하는 UI를 관리합니다. 
public partial class ClickerTemp
{
    /// === 실행 조건 ===
    /// 1. 활성화된 터치 이펙트가 존재할때
    /// === 실행 내용 ===
    /// 1. 터치 이펙트를 위로 이동시키고 투명도를 낮춤
    /// 2. 투명도가 0이 되면 터치 이펙트를 비활성화
    private void UpdateMidCanvasTouchEffect()
    {
        if(!Execute_UpdateMidCanvasTouchEffect) return;
        Execute_UpdateMidCanvasTouchEffect = false;

        for (int i = 0; i < midCanvas_Effects.Length; i++)
        {
            if(!midCanvas_Effects[i].gameObjectData.GetEnabled()) continue;

            /// 올라가는 효과
            var anchorPos = midCanvas_Effects[i].rectTransformData.GetAnchorPos();
            anchorPos.y += 0.01f;
            midCanvas_Effects[i].rectTransformData.SetAnchorPos(anchorPos);

            /// 페이드 아웃 효과
            var color = midCanvas_Effects[i].textData.GetColor();
            color.a = Mathf.Max(0, color.a - 0.02f);
            midCanvas_Effects[i].textData.SetColor(color);
            if(color.a <= 0) midCanvas_Effects[i].gameObjectData.SetEnabled(false);
        }
    }


    /// === 실행 조건 ===
    /// 1. 탭/초당 얻는 골드가 지정된 값을 넘어서 배경 이미지가 전환될때
    /// 2. 게임을 시작해서 탭/초당 얻는 골드에 맞는 배경 이미지가 활성화될때
    /// === 실행 내용 ===
    /// 1. 현재 활성화된 배경 이미지를 페이드 아웃
    /// 2. 탭/초당 얻는 골드에 맞는 배경 이미지를 페이드 인
    private void UpdateMidCanvasArtAnimation()
    {
        if(!Execute_UpdateMidCanvasArtAnimation) return;
        Execute_UpdateMidCanvasArtAnimation = false;

        int currentArt = -1;
        int targetArt = -1;
        double sum_TapSec = defaultGoldPerSec + defaultGoldPerTap;
        for (int i = 0; i < midCanvas_Art.Length; i++)
        {
            if(midCanvas_Art[i].gameObjectData.GetEnabled()) currentArt = i;
            if(sum_TapSec > midCanvasArtTiming[i]) targetArt = i;
        }

        if(currentArt == targetArt) return;

        // 게임을 실행했을때
        if(currentArt == -1)
        {
            midCanvas_Art[targetArt].gameObjectData.SetEnabled(true);
        }

        else
        {
            var fadeOutObject = midCanvas_Art[currentArt];
            var fadeInObject = midCanvas_Art[targetArt];
            fadeInObject.gameObjectData.SetEnabled(true);
            fadeOutObject.animationData.SetCurrentState("FadeOut");
            fadeInObject.animationData.SetCurrentState("FadeIn");
        }
    }


    /// === 실행 조건 ===
    /// 1. 3분마다 발생하는 특별 보상(광고시청) 이벤트가 활성화 될때 
    /// === 실행 내용 ===
    /// 1. 특별 보상(광고시청) 오브젝트를 랜덤한 위치에 활성화하고 애니메이션을 재생
    /// 2. 15초 후 애니메이션을 정지하고 오브젝트를 비활성화
    private void UpdateMidCanvasSpecialBonus()
    {
        if(!Execute_UpdateMidCanvasSpecialBonus) return;
        Execute_UpdateMidCanvasSpecialBonus = false;

        if(lastSec % 300 == 30 && !midCanvas_SpecialBonus.gameObjectData.GetEnabled() && RewardAdRequest.CanShowAd())
        {
            var random = new System.Random();
            midCanvas_SpecialBonus_Button.animationData.SetCurrentState("Play");
            midCanvas_SpecialBonus.gameObjectData.SetEnabled(true);

            float2 pos = new float2(random.Next(-400, 400), random.Next(-300, 300));
            midCanvas_SpecialBonus.rectTransformData.SetAnchoredPos(pos);
            return;
        }

        if(lastSec % 300 == 45 && midCanvas_SpecialBonus.gameObjectData.GetEnabled())
        {
            midCanvas_SpecialBonus_Button.animationData.SetCurrentState("IDLE");
            midCanvas_SpecialBonus.gameObjectData.SetEnabled(false);
            return;
        }
    }
}
