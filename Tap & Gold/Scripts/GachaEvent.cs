
/// 이 cs 파일은 가챠 스크롤과 팝업의 UI를 관리합니다.
public partial class Tap_N_Gold
{

    /// === 실행 조건 ===
    /// 가챠 버튼을 눌렀을때 실행 
    /// === 실행 내용 ===
    /// 1. 가챠 여부를 묻는 팝업을 활성화
    /// 2. 프레스티지 포인트가 부족하면 가챠 실행 버튼을 비활성화 
    private void UpdateGachaPopup()
    {
        if(!Execute_UpdateGachaPopup) return;
        Execute_UpdateGachaPopup = false;
        bool cond = prestigePoint >= 10;
        gacha_Button.SetInteractable(cond);
        gacha_Button.SetRaycastTarget(cond);
        gacha_Image.spriteData.SetSprite(artifactSprites[1]);
    }


    /// === 실행 조건 ===
    /// 1. 가챠 애니메이션을 시작할때
    /// === 실행 내용 ===
    /// 1. 팝업 중앙에 있는 가챠 이미지를 비활성화
    /// 2. 가챠 애니메이션에 해당하는 애니메이션 State 를 재생
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
    /// 1. gacha_Image 의 이미지를 1번에서 지급된 Artifact 의 이미지로 변경
    /// 2. gacha_Text 의 텍스트를 1번에서 지급된 Artifact 의 Description 로 변경
    private void OnGachaAnimationEnd()
    {
        if(!Execute_OnGachaAnimationEnd) return;
        Execute_OnGachaAnimationEnd = false;
        ArtifactData buffData = artifactList[artifactList.Count - 1];
        gacha_Image.spriteData.SetSprite(artifactSprites[buffData.buffID]);
        gacha_Image.gameObjectData.SetEnabled(true);
    }
}
