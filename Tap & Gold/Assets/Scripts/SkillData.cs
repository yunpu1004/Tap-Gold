using UnityEngine;

/// 이 클래스는 오토 탭 스킬 데이터를 관리합니다.
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

    
    /// 기본 오토 탭 스킬 데이터를 생성합니다.
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


    /// 오토 탭 스킬의 레벨을 반환합니다.
    public int GetLevel()
    {
        return level;
    }


    /// 오토 탭 스킬의 최대 지속 시간을 반환합니다.
    public float GetMaxTime()
    {
        return maxTime;
    }


    /// 오토 탭 스킬의 초당 탭 수를 반환합니다.
    public int GetTapPerSec()
    {
        return tapPerSec;
    }


    /// 오토 탭 스킬의 남은 지속 시간을 반환합니다.
    public float GetRemainedTime()
    {
        return remainedTime;
    }


    /// 오토 탭 스킬의 레벨을 설정합니다.
    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.tapPerSec = 1 + level/2;
        this.maxTime = 5f + 1f * (level - 1);
        this.maxDeltaTime = 1f / tapPerSec;
    }


    /// 오토 탭 스킬이 활성화되어있는지 여부를 반환합니다.
    public bool IsOnActivated()
    {
        return remainedTime > 0;
    }


    /// 오토 탭 스킬이 활성화 가능한지 여부를 반환합니다.
    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }


    /// 오토 탭 스킬을 활성화합니다.
    public void ActivateSkill()
    {
        if(level <= 0) return;
        if(remainedCoolTime > 0) return;
        remainedTime = maxTime;
        remainedCoolTime = maxCoolTime;
        remainedDeltaTime = maxDeltaTime;
    }


    /// 오토 탭 스킬의 남은 시간, 쿨타임을 업데이트합니다.
    public void UpdateData(ClickerTemp instance)
    {
        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이면 아무것도 하지 않음
        if(remainedTime <= 0 && remainedCoolTime <= 0) return;

        // 만약 스킬이 활성화 되어있지 않고 쿨타임이 0이 아니면 쿨타임을 줄임
        if(remainedTime <= 0 && remainedCoolTime > 0)
        {
            remainedCoolTime -= AppData.deltaTime;
            if(remainedCoolTime <= 0) remainedCoolTime = 0;
            return;
        }


        // 만약 스킬이 활성화 되어있으면 스킬 효과를 적용하고 시간을 줄임
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


    /// 오토 탭 스킬의 업그레이드 설명을 반환합니다.
    public string GetUpgradeDesc()
    {
        int nextTapPerSec = 1 + (level + 1)/2;
        float nextMaxTime = 5f + 1f * (level);

        return $"Auto tap <color=green><b>{tapPerSec}</b></color> / Sec for <color=orange><b>{maxTime}</b></color> seconds\n<sprite=2>  Auto tap <color=green><b>{nextTapPerSec}</b></color> / Sec for <color=orange><b>{nextMaxTime}</b></color> seconds";
    }
}


/// 이 클래스는 탭당 골드 보너스 스킬 데이터를 관리합니다.
[System.Serializable]
public class BonusTapSkillData
{
    [SerializeField] private int level;
    [SerializeField] private float bonusPerTap;
    public const float maxCoolTime = 100f;
    public float remainedCoolTime;
    [SerializeField] private float maxTime;
    public float remainedTime;


    /// 기본 탭당 골드 보너스 스킬 데이터를 생성합니다.
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

    /// 탭당 골드 보너스 스킬의 레벨을 반환합니다.
    public int GetLevel()
    {
        return level;
    }

    /// 탭당 골드 보너스 스킬의 최대 지속 시간을 반환합니다.
    public float GetMaxTime()
    {
        return maxTime;
    }

    /// 탭당 골드 보너스 스킬의 탭 당 보너스를 반환합니다.
    public float GetBonusPerTap()
    {
        return bonusPerTap;
    }

    /// 탭당 골드 보너스 스킬의 남은 지속 시간을 반환합니다.
    public float GetRemainedTime()
    {
        return remainedTime;
    }

    /// 탭당 골드 보너스 스킬의 레벨을 설정합니다.
    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.bonusPerTap = 1 + 0.1f * level;
        this.maxTime = 10f + 2f * (level - 1);
    }

    /// 탭당 골드 보너스 스킬이 활성화되어있는지 여부를 반환합니다.
    public bool IsOnActivated()
    {
        return remainedTime > 0;
    }

    /// 탭당 골드 보너스 스킬이 활성화 가능한지 여부를 반환합니다.
    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }

    /// 탭당 골드 보너스 스킬을 활성화합니다.
    public void ActivateSkill()
    {
        if(level <= 0) return;
        if(remainedCoolTime > 0) return;
        remainedTime = maxTime;
        remainedCoolTime = maxCoolTime;
    }

    /// 탭당 골드 보너스 스킬의 남은 시간, 쿨타임을 업데이트합니다.
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


    /// 탭당 골드 보너스 스킬의 업그레이드 설명을 반환합니다.
    public string GetUpgradeDesc()
    {
        float nextBonusPerTap = 1 + 0.1f * (level + 1);
        float nextMaxTime = 10f + 2f * (level);

        return $"Get <sprite=1> <color=green><b>{bonusPerTap}X</b></color> / Tap for <color=orange><b>{maxTime}</b></color> seconds\n<sprite=2>  Get <sprite=1> <color=green><b>{nextBonusPerTap}X</b></color> / Tap for <color=orange><b>{nextMaxTime}</b></color> seconds";
    }
}


/// 이 클래스는 초당 골드 보너스 스킬 데이터를 관리합니다.
[System.Serializable]
public class BonusSecSkillData
{
    [SerializeField] private int level;
    [SerializeField] private float bonusPerSec;
    public const float maxCoolTime = 100f;
    public float remainedCoolTime;
    [SerializeField] private float maxTime;
    public float remainedTime;


    /// 기본 초당 골드 보너스 스킬 데이터를 생성합니다.
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


    /// 초당 골드 보너스 스킬의 레벨을 반환합니다.
    public int GetLevel()
    {
        return level;
    }


    /// 초당 골드 보너스 스킬의 최대 지속 시간을 반환합니다.
    public float GetMaxTime()
    {
        return maxTime;
    }


    /// 초당 골드 보너스 스킬의 초당 보너스를 반환합니다.
    public float GetBonusPerSec()
    {
        return bonusPerSec;
    }


    /// 초당 골드 보너스 스킬의 남은 지속 시간을 반환합니다.
    public float GetRemainedTime()
    {
        return remainedTime;
    }


    /// 초당 골드 보너스 스킬의 레벨을 설정합니다.
    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.bonusPerSec = 1 + 0.1f * level;
        this.maxTime = 10f + 2f * (level - 1);
    }


    /// 초당 골드 보너스 스킬이 활성화되어있는지 여부를 반환합니다.
    public bool IsOnActivated()
    {
        return remainedTime > 0;
    }


    /// 초당 골드 보너스 스킬이 활성화 가능한지 여부를 반환합니다.
    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }


    /// 초당 골드 보너스 스킬을 활성화합니다.
    public void ActivateSkill()
    {
        if(level <= 0) return;
        if(remainedCoolTime > 0) return;
        remainedTime = maxTime;
        remainedCoolTime = maxCoolTime;
    }


    /// 초당 골드 보너스 스킬의 남은 시간, 쿨타임을 업데이트합니다.
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


    /// 초당 골드 보너스 스킬의 업그레이드 설명을 반환합니다.
    public string GetUpgradeDesc()
    {
        float nextBonusPerSec = 1 + 0.1f * (level + 1);
        float nextMaxTime = 10f + 2f * (level);

        return $"Get <sprite=1> <color=green><b>{bonusPerSec}X</b></color> / Sec for <color=orange><b>{maxTime}</b></color> seconds\n<sprite=2>  Get <sprite=1> <color=green><b>{nextBonusPerSec}X</b></color> / Sec for <color=orange><b>{nextMaxTime}</b></color> seconds";
    }
}


/// 이 클래스는 쿨다운 스킬 데이터를 관리합니다.
[System.Serializable]
public class CoolDownSkillData
{
    [SerializeField] private int level;
    [SerializeField] private float coolDownReduceRate;
    public const float maxCoolTime = 100f;
    public float remainedCoolTime;


    /// 기본 쿨다운 스킬 데이터를 생성합니다.
    public static CoolDownSkillData CreateDefaultInstance()
    {
        return new CoolDownSkillData
        {
            level = 0,
            coolDownReduceRate = 0,
            remainedCoolTime = 0
        };
    }


    /// 쿨다운 스킬의 레벨을 반환합니다.
    public int GetLevel()
    {
        return level;
    }


    /// 쿨다운 스킬의 레벨을 설정합니다.
    public void SetLevel(int level)
    {
        if(level <= 0) return;
        this.level = level;
        this.coolDownReduceRate = 0.01f * level;
    }


    /// 쿨다운 스킬이 활성화 가능한지 여부를 반환합니다.
    public bool IsActivatable()
    {
        return remainedCoolTime <= 0;
    }


    /// 쿨다운 스킬을 활성화합니다.
    /// 다른 스킬들이 쿨타임을 가지고 있으면 일정 % 만큼 쿨타임을 줄입니다.
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


    /// 쿨다운 스킬의 남은 쿨타임 시간을 업데이트합니다.
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


    /// 쿨다운 스킬의 업그레이드 설명을 반환합니다.
    public string GetUpgradeDesc()
    {
        string txt;
        if(level < 100) txt = $"Reduce all cooldown times by <color=green><b>{level}%</b></color>\n<sprite=2>  Reduce all cooldown times by <color=green><b>{level+1}%</b></color>";
        else txt = $"Reduce all cooldown times by <color=green><b>{level}%</b></color>";
        return txt;
    }
}