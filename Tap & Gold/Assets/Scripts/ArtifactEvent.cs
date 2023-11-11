using UnityEngine;

public partial class ClickerTemp
{
    private void UpdateSpecialTokenText()
    {
        if(Execute_UpdateSpecialTokenText)
        {
            artifact_prestigePointText.textData.SetText($"<sprite=0> {prestigePoint}");
            Execute_UpdateSpecialTokenText = false;
        }
    }

    /// === 실행 조건 ===
    /// 1. 게임 시작시
    /// 2. State 가 GachaAnimation -> Normal 로 변경될때
    /// === 실행 내용 ===
    /// 1. artifactList 에 있는 모든 버프를 화면에 표시한다
    private void UpdateArtifactScroll()
    {
        if(Execute_UpdateArtifactScroll)
        {            
            Vector2 pos = new(0.5f, 0.87f);
            Vector2 height = new(0, 0.06f);

            for(int i = 0; i < artifactList.Count; i++)
            {
                /// 오브젝트의 id 를 읽고 그에 해당하는 Artifact UI를 찾는다
                SimpleImage ui = artifact_Artifacts[artifactList[i].buffID - 1];

                /// 현재 인덱스를 사용해서 Artifact UI 의 위치를 지정한다
                ui.rectTransformData.SetAnchorPos(pos - height * i);

                /// Artifact UI를 활성화 시킨다 
                ui.gameObjectData.SetEnabled(true);
            }

            Execute_UpdateArtifactScroll = false;
        }
    }
}
