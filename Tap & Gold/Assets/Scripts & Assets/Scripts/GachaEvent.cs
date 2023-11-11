public partial class ClickerTemp
{
    private void UpdateGachaPopup()
    {
        if(!Execute_UpdateGachaPopup) return;
        Execute_UpdateGachaPopup = false;
        bool cond = prestigePoint >= 10;
        gacha_Button.SetInteractable(cond);
        gacha_Button.SetRaycastTarget(cond);
        gacha_Image.spriteData.SetSprite(artifactSprites[1]);
    }

    private void OnGachaAnimationStart()
    {
        if(!Execute_OnGachaAnimationStart) return;
        Execute_OnGachaAnimationStart = false;
        gacha_Image.gameObjectData.SetEnabled(false);
        gacha_AnimImage.animationData.SetCurrentState("Gacha");
    }


    /// === 실행 조건 ===
    /// 1. State 가 GachaAnimation -> Normal 로 변경될때
    /// === 실행 내용 ===
    /// 1. gacha_Image 의 이미지를 1번에서 지급된 Artifact 의 이미지로 변경한다
    /// 2. gacha_Text 의 텍스트를 1번에서 지급된 Artifact 의 Description 로 변경한다
    private void OnGachaAnimationEnd()
    {
        if(!Execute_OnGachaAnimationEnd) return;
        Execute_OnGachaAnimationEnd = false;
        ArtifactData buffData = artifactList[artifactList.Count - 1];
        gacha_Image.spriteData.SetSprite(artifactSprites[buffData.buffID]);
        gacha_Image.gameObjectData.SetEnabled(true);
    }
}
