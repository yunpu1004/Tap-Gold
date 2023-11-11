using UnityEngine;

public partial class ClickerTemp
{
    private void UpdateTopCanvasGoldPerTapText()
    {
        if(Execute_UpdateTopCanvasGoldPerTapText)
        {
            string numericalText_realGoldPerTap = realGoldPerTap < 1000000 ? realGoldPerTap.ToString("N0") : realGoldPerTap.ToString("0.000e0");
            topCanvas_GoldPerTapText.textData.SetText($"<sprite=1> {numericalText_realGoldPerTap} / Tap");
            Execute_UpdateTopCanvasGoldPerTapText = false;
        }
    }

    private void UpdateTopCanvasGoldPerSecText()
    {
        if(Execute_UpdateTopCanvasGoldPerSecText)
        {
            string numericalText_realGoldPerSec = realGoldPerSec < 1000000 ? realGoldPerSec.ToString("N0") : realGoldPerSec.ToString("0.000e0");
            topCanvas_GoldPerSecText.textData.SetText($"<sprite=1> {numericalText_realGoldPerSec} / Sec");
            Execute_UpdateTopCanvasGoldPerSecText = false;
        }
    }

    private void UpdateTopCanvasGoldText()
    {
        if(Execute_UpdateTopCanvasGoldText)
        {
            string numericalText_gold = gold < 1000000 ? gold.ToString("N0") : gold.ToString("0.000e0");
            topCanvas_GoldText.textData.SetText($"<sprite=1> {numericalText_gold}");
            Execute_UpdateTopCanvasGoldText = false;
        }
    }

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

    private void UpdateTopCanvasVolumeBtn()
    {
        if(!Execute_UpdateTopCanvasVolumeBtn) return;
        Execute_UpdateTopCanvasVolumeBtn = false;

        topCanvas_VolumeButton.spriteData.SetSprite(volumeOn ? volumeSprites[0] : volumeSprites[1]);
        topCanvas_VolumeButton_BGM.audioData.SetVolume(volumeOn ? 1 : 0);
    }
}
