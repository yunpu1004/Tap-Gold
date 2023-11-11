using UnityEngine;

/// 이 cs파일은 상단 UI를 관리합니다.
public partial class ClickerTemp
{

    /// === 실행 조건 ===
    /// 1. 게임이 시작되었을때
    /// 2. 탭당 얻는 골드가 변경되었을때 (업그레이드, 아티팩트, 버프)
    /// === 실행 내용 ===
    /// 1. 탭당 얻는 골드 텍스트를 데이터에 맞게 업데이트
    private void UpdateTopCanvasGoldPerTapText()
    {
        if(Execute_UpdateTopCanvasGoldPerTapText)
        {
            string numericalText_realGoldPerTap = realGoldPerTap < 1000000 ? realGoldPerTap.ToString("N0") : realGoldPerTap.ToString("0.000e0");
            topCanvas_GoldPerTapText.textData.SetText($"<sprite=1> {numericalText_realGoldPerTap} / Tap");
            Execute_UpdateTopCanvasGoldPerTapText = false;
        }
    }

    /// === 실행 조건 ===
    /// 1. 게임이 시작되었을때
    /// 2. 초당 얻는 골드가 변경되었을때 (업그레이드, 아티팩트, 버프)
    /// === 실행 내용 ===
    /// 1. 초당 얻는 골드 텍스트를 데이터에 맞게 업데이트
    private void UpdateTopCanvasGoldPerSecText()
    {
        if(Execute_UpdateTopCanvasGoldPerSecText)
        {
            string numericalText_realGoldPerSec = realGoldPerSec < 1000000 ? realGoldPerSec.ToString("N0") : realGoldPerSec.ToString("0.000e0");
            topCanvas_GoldPerSecText.textData.SetText($"<sprite=1> {numericalText_realGoldPerSec} / Sec");
            Execute_UpdateTopCanvasGoldPerSecText = false;
        }
    }

    /// === 실행 조건 ===
    /// 1. 게임이 시작되었을때
    /// 2. 보유한 골드가 변경되었을때
    /// === 실행 내용 ===
    /// 1. 보유한 골드 텍스트를 데이터에 맞게 업데이트
    private void UpdateTopCanvasGoldText()
    {
        if(Execute_UpdateTopCanvasGoldText)
        {
            string numericalText_gold = gold < 1000000 ? gold.ToString("N0") : gold.ToString("0.000e0");
            topCanvas_GoldText.textData.SetText($"<sprite=1> {numericalText_gold}");
            Execute_UpdateTopCanvasGoldText = false;
        }
    }

    /// === 실행 조건 ===
    /// 1. 게임이 시작되었을때
    /// 2. 탭 버프의 남은 시간이 변경되었을때
    /// === 실행 내용 ===
    /// 1. 탭 버프 남은시간 텍스트를 데이터에 맞게 업데이트
    private void UpdateTopCanvasTapTempBuffBtn()
    {
        if(!Execute_UpdateTopCanvasTapTempBuffBtn) return;

        string remainedTimeInSec = (tapTempBuffData.GetIsBuffOn()) ?tapTempBuffData.GetRemainedTime().ToString("N0") : "";
        topCanvas_TapTempBuffBtnText.textData.SetText(remainedTimeInSec);
        topCanvas_TapTempBuffBtn.SetInteractable(!tapTempBuffData.GetIsBuffOn());
        Color color = (tapTempBuffData.GetIsBuffOn()) ? new Color(0.25f, 0.25f, 0.25f) : new Color(1, 1, 1);
        topCanvas_TapTempBuffBtn.spriteData.SetColor(color);

        Execute_UpdateTopCanvasTapTempBuffBtn = false;
    }

    /// === 실행 조건 ===
    /// 1. 게임이 시작되었을때
    /// 2. 초당 버프의 남은 시간이 변경되었을때
    /// === 실행 내용 ===
    /// 1. 초당 버프 남은시간 텍스트를 데이터에 맞게 업데이트
    private void UpdateTopCanvasSecTempBuffBtn()
    {
        if(!Execute_UpdateTopCanvasSecTempBuffBtn) return;

        string remainedTimeInSec = (secTempBuffData.GetIsBuffOn()) ?secTempBuffData.GetRemainedTime().ToString("N0") : "";
        topCanvas_SecTempBuffBtnText.textData.SetText(remainedTimeInSec);
        topCanvas_SecTempBuffBtn.SetInteractable(!secTempBuffData.GetIsBuffOn());
        Color color = (secTempBuffData.GetIsBuffOn()) ? new Color(0.25f, 0.25f, 0.25f) : new Color(1, 1, 1);
        topCanvas_SecTempBuffBtn.spriteData.SetColor(color);

        Execute_UpdateTopCanvasSecTempBuffBtn = false;
    }

    /// === 실행 조건 ===
    /// 1. 게임이 시작되었을때
    /// 2. 볼륨 On/Off 버튼을 눌렀을때
    /// === 실행 내용 ===
    /// 1. 볼륨 On/Off 버튼의 이미지를 볼륨상태에 맞게 업데이트
    private void UpdateTopCanvasVolumeBtn()
    {
        if(!Execute_UpdateTopCanvasVolumeBtn) return;
        Execute_UpdateTopCanvasVolumeBtn = false;

        topCanvas_VolumeButton.spriteData.SetSprite(volumeOn ? volumeSprites[0] : volumeSprites[1]);
        topCanvas_VolumeButton_BGM.audioData.SetVolume(volumeOn ? 1 : 0);
    }
}
