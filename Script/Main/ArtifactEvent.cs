using UnityEngine;

/// 이 cs파일은 아티팩트 스크롤의 UI를 관리합니다.
public partial class Tap_N_Gold
{
    /// 현재 보유한 프레스티지 포인트를 표시하는 텍스트를 업데이트 합니다.
    private void UpdatePrestigePointText()
    {
        if(Execute_UpdatePrestigePointText)
        {
            artifact_prestigePointText.textData.SetText($"<sprite=0> {prestigePoint}");
            Execute_UpdatePrestigePointText = false;
        }
    }

    /// 게임 시작시 또는 State 가 GachaAnimation -> Normal 로 변경될때 실행됩니다.
    /// 아티팩트 스크롤에 표시되는 보유 아티팩트 항목을 적절한 위치에 배치합니다.
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
