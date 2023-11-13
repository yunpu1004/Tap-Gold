using System;
using UnityEngine;


/// 이 클래스는 플레이어의 게임 데이터를 관리합니다.
[System.Serializable]
public class PlayerSavedData
{
    public long ticks;
    public bool volumeOn;
    public double defaultGoldPerTap;
    public double defaultGoldPerSec;
    public double gold;
    public double returningRewardGold;
    public int prestigePoint;
    public double highestDefaultGoldPerTap;
    public double highestDefaultGoldPerSec;
    public double highestGold;
    public int adCount;
    public int playTime;
    public int tapCount;
    public int prestigeCount;
    public bool[] artifactList;
    public int challenge_HoldGold_Level;
    public int challenge_TapCount_Level;
    public int challenge_GoldPerTap_Level;
    public int challenge_GoldPerSec_Level;
    public int challenge_AdCount_Level;
    public int challenge_PlayTime_Level;
    public int challenge_PrestigeCount_Level;
    public int challenge_ArtifactCount_Level;
    public UpgradeData tapUpgradeData;
    public UpgradeData[] secUpgradeList;
    public AutoTapSkillData autoTapSkillData;
    public BonusTapSkillData bonusTapSkillData;
    public BonusSecSkillData bonusSecSkillData;
    public CoolDownSkillData coolDownSkillData;



    /// 기본 게임 데이터를 생성합니다.
    public static PlayerSavedData CreateDefaultInstance()
    {
        bool[] artifactList = new bool[15];
        for(int i = 0; i < artifactList.Length; i++)
        {
            artifactList[i] = false;
        }
        return new PlayerSavedData
        {
            ticks = DateTime.Now.Ticks,
            volumeOn = true,
            defaultGoldPerTap = 1,
            defaultGoldPerSec = 5,
            gold = 0,
            returningRewardGold = 0,
            prestigePoint = 0,
            highestDefaultGoldPerTap = 1,
            highestDefaultGoldPerSec = 5,
            highestGold = 0,
            adCount = 0,
            playTime = 0,
            tapCount = 0,
            prestigeCount = 0,
            artifactList = artifactList,
            challenge_HoldGold_Level = 0,
            challenge_TapCount_Level = 0,
            challenge_GoldPerTap_Level = 0,
            challenge_GoldPerSec_Level = 0,
            challenge_AdCount_Level = 0,
            challenge_PlayTime_Level = 0,
            challenge_PrestigeCount_Level = 0,
            challenge_ArtifactCount_Level = 0,
            tapUpgradeData = UpgradeData.GetTapUpgradeData(),
            secUpgradeList = UpgradeData.GetSecUpgradeList(),
            autoTapSkillData = AutoTapSkillData.CreateDefaultInstance(),
            bonusTapSkillData = BonusTapSkillData.CreateDefaultInstance(),
            bonusSecSkillData = BonusSecSkillData.CreateDefaultInstance(),
            coolDownSkillData = CoolDownSkillData.CreateDefaultInstance()
        };
    }



    /// 현재 게임 데이터를 저장합니다.
    public static void SavePlayerData(Tap_N_Gold instance)
    {
        bool[] artifactList = new bool[15];
        for(int i = 0; i < artifactList.Length; i++)
        {
            artifactList[i] = false;
        }

        foreach(var buff in instance.artifactList)
        {
            artifactList[buff.buffID - 1] = true;
        }

        var data = new PlayerSavedData
        {
            ticks = instance.currentTicks,
            volumeOn = instance.volumeOn,
            defaultGoldPerTap = instance.defaultGoldPerTap,
            defaultGoldPerSec = instance.defaultGoldPerSec,
            gold = instance.gold,
            returningRewardGold = instance.returningRewardGold,
            prestigePoint = instance.prestigePoint,
            highestDefaultGoldPerTap = instance.highestDefaultGoldPerTap,
            highestDefaultGoldPerSec = instance.highestDefaultGoldPerSec,
            highestGold = instance.highestGold,
            adCount = instance.adCount,
            playTime = instance.totalPlayTime,
            tapCount = instance.tapCount,
            prestigeCount = instance.prestigeCount,
            artifactList = artifactList,
            challenge_HoldGold_Level = instance.challenge_HoldGold_Level,
            challenge_TapCount_Level = instance.challenge_TapCount_Level,
            challenge_GoldPerTap_Level = instance.challenge_GoldPerTap_Level,
            challenge_GoldPerSec_Level = instance.challenge_GoldPerSec_Level,
            challenge_AdCount_Level = instance.challenge_AdCount_Level,
            challenge_PlayTime_Level = instance.challenge_PlayTime_Level,
            challenge_PrestigeCount_Level = instance.challenge_PrestigeCount_Level,
            challenge_ArtifactCount_Level = instance.challenge_ArtifactCount_Level,
            tapUpgradeData = instance.tapUpgradeData,
            secUpgradeList = instance.secUpgradeList,
            autoTapSkillData = instance.autoTapSkillData,
            bonusTapSkillData = instance.bonusTapSkillData,
            bonusSecSkillData = instance.bonusSecSkillData,
            coolDownSkillData = instance.coolDownSkillData
        };

        if(!CheckValidation(data)) throw new System.Exception("Save Data Check Validation Failed");
        
        // Convert the player data to JSON
        string json = JsonUtility.ToJson(data);

        // Save the JSON string to a file
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerSavedData.json", json);
    }
    


    /// 저장된 게임 데이터를 불러옵니다.
    public static PlayerSavedData LoadPlayerData()
    {
        try
        {
            string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerSavedData.json");
            PlayerSavedData playerData = JsonUtility.FromJson<PlayerSavedData>(json);
            return playerData;
        }
        catch (System.Exception)
        {
            return CreateDefaultInstance();
        }
    }



    /// 게임 데이터가 비정상적인지 확인합니다.
    private static bool CheckValidation(PlayerSavedData data)
    {
        if(data.tapUpgradeData == null)
        {
            Debug.LogError("tapUpgradeData == null");
            return false;
        }

        if(data.secUpgradeList.Length != 15)
        {
            Debug.LogError("secUpgradeList.Length != 15");
            return false;
        }

        if(data.gold < 0)
        {
            Debug.LogError("gold < 0");
            return false;
        }

        if(data.defaultGoldPerSec <= 0)
        {
            Debug.LogError("defaultGoldPerSec <= 0");
            return false;
        }

        if(data.defaultGoldPerTap <= 0)
        {
            Debug.LogError("defaultGoldPerTap <= 0");
            return false;
        }        

        if(data.autoTapSkillData == null)
        {
            Debug.LogError("autoTapSkillData == null");
            return false;
        }

        if(data.bonusTapSkillData == null)
        {
            Debug.LogError("autoSecSkillData == null");
            return false;
        }

        if(data.bonusTapSkillData == null)
        {
            Debug.LogError("bonusTapSkillData == null");
            return false;
        }

        if(data.bonusSecSkillData == null)
        {
            Debug.LogError("bonusSecSkillData == null");
            return false;
        }

        if(data.coolDownSkillData == null)
        {
            Debug.LogError("coolDownSkillData == null");
            return false;
        }

        return true;
    }
}
