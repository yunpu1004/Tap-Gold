using UnityEngine;

[System.Serializable]
public class AutoTapSkillData
{
    [SerializeField] private int level;
    [SerializeField] private int tapPerSec;
    public const float maxCoolTime = 100f;
    public float remainedCoolTime;
    [SerializeField] private float maxTime;
    public float remainedTime;
    [SerializeField] private float maxDeltaTime;
    public float remainedDeltaTime;
    
    public static AutoTapSkillData CreateDefaultInstance()
    {
        return new AutoTapSkillData
        {
            level = 0,
            tapPerSec = 0,
            maxTime = 0,
            remainedTime = 0,
            remainedCoolTime = 0,
            maxDeltaTime = 0,
            remainedDeltaTime = 0
        };
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetMaxTime()
    {
        return maxTime;
    }

    public int GetTapPerSec()
    {
        return tapPerSec;
    }

    public float GetRemainedTime()
    {
        return remainedTime;
    }

    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.tapPerSec = 1 + level/2;
        this.maxTime = 5f + 1f * (level - 1);
        this.maxDeltaTime = 1f / tapPerSec;
    }

    public bool IsOnActivated()
    {
        return remainedTime > 0;
    }

    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }

    public void ActivateSkill()
    {
        if(level <= 0) return;
        if(remainedCoolTime > 0) return;
        remainedTime = maxTime;
        remainedCoolTime = maxCoolTime;
        remainedDeltaTime = maxDeltaTime;
    }

    public void UpdateData(ClickerTemp instance)
    {
        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이면 아무것도 하지 않는다.
        if(remainedTime <= 0 && remainedCoolTime <= 0) return;

        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이 아니면 쿨타임을 줄인다.
        if(remainedTime <= 0 && remainedCoolTime > 0)
        {
            remainedCoolTime -= AppData.deltaTime;
            if(remainedCoolTime <= 0) remainedCoolTime = 0;
            return;
        }


        // 만약 스킬이 활성화 되어있으면 스킬 효과를 적용하고 시간을 줄인다.
        if(remainedTime > 0)
        {
            remainedTime -= AppData.deltaTime;
            remainedDeltaTime -= AppData.deltaTime;
            if(remainedDeltaTime <= 0)
            {
                remainedDeltaTime = maxDeltaTime;
                instance.gold += instance.realGoldPerTap;
            }

            if(remainedTime <= 0) remainedTime = 0;
        }
    }

    public string GetUpgradeDesc()
    {
        int nextTapPerSec = 1 + (level + 1)/2;
        float nextMaxTime = 5f + 1f * (level);

        return $"Auto tap <color=green><b>{tapPerSec}</b></color> / Sec for <color=orange><b>{maxTime}</b></color> seconds\n<sprite=2>  Auto tap <color=green><b>{nextTapPerSec}</b></color> / Sec for <color=orange><b>{nextMaxTime}</b></color> seconds";
    }
}

[System.Serializable]
public class BonusTapSkillData
{
    [SerializeField] private int level;
    [SerializeField] private float bonusPerTap;
    public const float maxCoolTime = 100f;
    public float remainedCoolTime;
    [SerializeField] private float maxTime;
    public float remainedTime;

    public static BonusTapSkillData CreateDefaultInstance()
    {
        return new BonusTapSkillData
        {
            level = 0,
            bonusPerTap = 0,
            maxTime = 0,
            remainedTime = 0,
            remainedCoolTime = 0
        };
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetMaxTime()
    {
        return maxTime;
    }

    public float GetBonusPerTap()
    {
        return bonusPerTap;
    }

    public float GetRemainedTime()
    {
        return remainedTime;
    }

    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.bonusPerTap = 1 + 0.1f * level;
        this.maxTime = 10f + 2f * (level - 1);
    }

    public bool IsOnActivated()
    {
        return remainedTime > 0;
    }

    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }

    public void ActivateSkill()
    {
        if(level <= 0) return;
        if(remainedCoolTime > 0) return;
        remainedTime = maxTime;
        remainedCoolTime = maxCoolTime;
    }

    public void UpdateData()
    {
        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이면 아무것도 하지 않는다.
        if(remainedTime <= 0 && remainedCoolTime <= 0) return;

        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이 아니면 쿨타임을 줄인다.
        if(remainedTime <= 0 && remainedCoolTime > 0)
        {
            remainedCoolTime -= AppData.deltaTime;
            if(remainedCoolTime <= 0) remainedCoolTime = 0;
            return;
        }

        // 만약 스킬이 활성화 되어있으면 스킬 효과를 적용하고 시간을 줄인다.
        if(remainedTime > 0)
        {
            remainedTime -= AppData.deltaTime;
            if(remainedTime <= 0) remainedTime = 0;
        }
    }

    public string GetUpgradeDesc()
    {
        float nextBonusPerTap = 1 + 0.1f * (level + 1);
        float nextMaxTime = 10f + 2f * (level);

        return $"Get <sprite=1> <color=green><b>{bonusPerTap}X</b></color> / Tap for <color=orange><b>{maxTime}</b></color> seconds\n<sprite=2>  Get <sprite=1> <color=green><b>{nextBonusPerTap}X</b></color> / Tap for <color=orange><b>{nextMaxTime}</b></color> seconds";
    }
}

[System.Serializable]
public class BonusSecSkillData
{
    [SerializeField] private int level;
    [SerializeField] private float bonusPerSec;
    public const float maxCoolTime = 100f;
    public float remainedCoolTime;
    [SerializeField] private float maxTime;
    public float remainedTime;

    public static BonusSecSkillData CreateDefaultInstance()
    {
        return new BonusSecSkillData
        {
            level = 0,
            bonusPerSec = 0,
            maxTime = 0,
            remainedTime = 0,
            remainedCoolTime = 0
        };
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetMaxTime()
    {
        return maxTime;
    }

    public float GetBonusPerSec()
    {
        return bonusPerSec;
    }

    public float GetRemainedTime()
    {
        return remainedTime;
    }

    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.bonusPerSec = 1 + 0.1f * level;
        this.maxTime = 10f + 2f * (level - 1);
    }

    public bool IsOnActivated()
    {
        return remainedTime > 0;
    }

    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }

    public void ActivateSkill()
    {
        if(level <= 0) return;
        if(remainedCoolTime > 0) return;
        remainedTime = maxTime;
        remainedCoolTime = maxCoolTime;
    }

    public void UpdateData()
    {
        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이면 아무것도 하지 않는다.
        if(remainedTime <= 0 && remainedCoolTime <= 0) return;

        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이 아니면 쿨타임을 줄인다.
        if(remainedTime <= 0 && remainedCoolTime > 0)
        {
            remainedCoolTime -= AppData.deltaTime;
            if(remainedCoolTime <= 0) remainedCoolTime = 0;
            return;
        }

        // 만약 스킬이 활성화 되어있으면 스킬 효과를 적용하고 시간을 줄인다.
        if(remainedTime > 0)
        {
            remainedTime -= AppData.deltaTime;
            if(remainedTime <= 0) remainedTime = 0;
        }
    }

    public string GetUpgradeDesc()
    {
        float nextBonusPerSec = 1 + 0.1f * (level + 1);
        float nextMaxTime = 10f + 2f * (level);

        return $"Get <sprite=1> <color=green><b>{bonusPerSec}X</b></color> / Sec for <color=orange><b>{maxTime}</b></color> seconds\n<sprite=2>  Get <sprite=1> <color=green><b>{nextBonusPerSec}X</b></color> / Sec for <color=orange><b>{nextMaxTime}</b></color> seconds";
    }
}

[System.Serializable]
public class CoolDownSkillData
{
    [SerializeField] private int level;
    [SerializeField] private float coolDownReduceRate;
    public const float maxCoolTime = 100f;
    public float remainedCoolTime;

    public static CoolDownSkillData CreateDefaultInstance()
    {
        return new CoolDownSkillData
        {
            level = 0,
            coolDownReduceRate = 0,
            remainedCoolTime = 0
        };
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.coolDownReduceRate = 0.01f * level;
    }

    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }

    public void ActivateSkill(ClickerTemp instance)
    {
        if(level <= 0) return;
        if(remainedCoolTime > 0) return;
        remainedCoolTime = maxCoolTime;
        
        if(!instance.autoTapSkillData.IsActivatable())
        {
            float remainedCoolTime = instance.autoTapSkillData.remainedCoolTime;
            remainedCoolTime -= AutoTapSkillData.maxCoolTime * coolDownReduceRate;
            if(remainedCoolTime < 0) remainedCoolTime = 0;
            instance.autoTapSkillData.remainedCoolTime = remainedCoolTime;
        }

        if(!instance.bonusTapSkillData.IsActivatable())
        {
            float remainedCoolTime = instance.bonusTapSkillData.remainedCoolTime;
            remainedCoolTime -= BonusTapSkillData.maxCoolTime * coolDownReduceRate;
            if(remainedCoolTime < 0) remainedCoolTime = 0;
            instance.bonusTapSkillData.remainedCoolTime = remainedCoolTime;
        }

        if(!instance.bonusSecSkillData.IsActivatable())
        {
            float remainedCoolTime = instance.bonusSecSkillData.remainedCoolTime;
            remainedCoolTime -= BonusSecSkillData.maxCoolTime * coolDownReduceRate;
            if(remainedCoolTime < 0) remainedCoolTime = 0;
            instance.bonusSecSkillData.remainedCoolTime = remainedCoolTime;
        }
    }

    public void UpdateData()
    {
        if(remainedCoolTime <= 0) return;

        if(remainedCoolTime > 0)
        {
            remainedCoolTime -= AppData.deltaTime;
            if(remainedCoolTime <= 0) remainedCoolTime = 0;
            return;
        }
    }

    public string GetUpgradeDesc()
    {
        string txt = "";
        if(level < 100) txt = $"Reduce all cooldown times by <color=green><b>{level}%</b></color>\n<sprite=2>  Reduce all cooldown times by <color=green><b>{level+1}%</b></color>";
        else txt = $"Reduce all cooldown times by <color=green><b>{level}%</b></color>";
        return txt;
    }
}