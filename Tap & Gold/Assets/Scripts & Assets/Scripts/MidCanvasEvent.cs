using Unity.Mathematics;
using UnityEngine;

public partial class ClickerTemp
{
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

    private void UpdateMidCanvasSpecialBonus()
    {
        if(!Execute_UpdateMidCanvasSpecialBonus) return;
        Execute_UpdateMidCanvasSpecialBonus = false;

        if(lastSec % 300 == 45 && midCanvas_SpecialBonus.gameObjectData.GetEnabled())
        {
            midCanvas_SpecialBonus_Button.animationData.SetCurrentState("IDLE");
            midCanvas_SpecialBonus.gameObjectData.SetEnabled(false);
            return;
        }

        if(lastSec % 300 == 30 && !midCanvas_SpecialBonus.gameObjectData.GetEnabled() && RewardAdRequest.CanShowAd())
        {
            var random = new System.Random();
            midCanvas_SpecialBonus_Button.animationData.SetCurrentState("Play");
            midCanvas_SpecialBonus.gameObjectData.SetEnabled(true);

            float2 pos = new float2(random.Next(-400, 400), random.Next(-300, 300));
            midCanvas_SpecialBonus.rectTransformData.SetAnchoredPos(pos);
            return;
        }
    }
}
