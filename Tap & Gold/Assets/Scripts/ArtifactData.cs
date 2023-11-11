using System.Collections.Generic;
using static Unity.Mathematics.math;

/// 이 클래스는 
[System.Serializable]
public class ArtifactData
{
    public readonly string name;
    public readonly string description;
    public readonly int buffID = -1;
    public readonly bool isPermanent;
    public readonly float tapBonus;
    public readonly float secBonus;
    public readonly int tapPlus;
    public readonly int secPlus;
    public readonly float prestigeBonus;
    private float remainedTime;
    private bool isBuffOn;


    private ArtifactData(string name, string description, int buffID, bool isPermanent, float tapBonus, float secBonus, int tapPlus, int secPlus, float buffTimeBonus)
    {
        this.name = name;
        this.description = description;
        this.buffID = buffID;
        this.isPermanent = isPermanent;
        this.tapBonus = tapBonus;
        this.secBonus = secBonus;
        this.tapPlus = tapPlus;
        this.secPlus = secPlus;
        this.prestigeBonus = buffTimeBonus;
        this.remainedTime = 0;
        this.isBuffOn = false;
    }


    public static float PrestigeBonus(List<ArtifactData> buffs, float originalPrestigeReward)
    {
        float prestigeBonusSum = 0;
        foreach(ArtifactData buff in buffs)
        {
            prestigeBonusSum += buff.prestigeBonus;
        }
        return originalPrestigeReward * (1 + prestigeBonusSum);
    }

    public float GetRemainedTime()
    {
        return remainedTime;
    }

    public void SetRemainedTime(float value)
    {
        if(isPermanent) return;
        remainedTime = max(0, value);
        isBuffOn = (remainedTime > 0) ? true : false;
    }

    public bool GetIsBuffOn()
    {
        return isPermanent || isBuffOn;
    }

    public static ArtifactData GetArtifactBuff1()
    {
        return new ArtifactData(
            name: "Ancient Shield",
            description: "Increases the prestige reward by 20%.",
            buffID : 1,
            isPermanent: true,
            tapBonus: 0,
            secBonus: 0,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0.2f
        );
    }

    public static ArtifactData GetArtifactBuff2()
    {
        return new ArtifactData(
            name: "Antique Pottery",
            description: "Increases the prestige reward by 25%.",
            buffID : 2,
            isPermanent: true,
            tapBonus: 0,
            secBonus: 0,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0.25f
        );
    }

    public static ArtifactData GetArtifactBuff3()
    {
        return new ArtifactData(
            name: "Mystery Mask",
            description: "Increases the prestige reward by 30%.",
            buffID : 3,
            isPermanent: true,
            tapBonus: 0,
            secBonus: 0,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0.3f
        );
    }

    public static ArtifactData GetArtifactBuff4()
    {
        return new ArtifactData(
            name: "Arming Sword",
            description: "Increases the gold per tap by 50%.",
            buffID : 4,
            isPermanent: true,
            tapBonus: 0.5f,
            secBonus: 0,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff5()
    {
        return new ArtifactData(
            name: "Golden Crown",
            description: "Increases the gold per tap by 75%.",
            buffID : 5,
            isPermanent: true,
            tapBonus: 0.75f,
            secBonus: 0,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff6()
    {
        return new ArtifactData(
            name: "Bishop's Mitre",
            description: "Increases the gold per tap by 100%.",
            buffID : 6,
            isPermanent: true,
            tapBonus: 1f,
            secBonus: 0,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff7()
    {
        return new ArtifactData(
            name: "Magical Glass Marble",
            description: "Increases the gold per second by 50%.",
            buffID : 7,
            isPermanent: true,
            tapBonus: 0,
            secBonus: 0.5f,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff8()
    {
        return new ArtifactData(
            name: "Magic Lamp",
            description: "Increases the gold per second by 75%.",
            buffID : 8,
            isPermanent: true,
            tapBonus: 0,
            secBonus: 0.75f,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff9()
    {
        return new ArtifactData(
            name: "Diamond Ring",
            description: "Increases the gold per second by 100%.",
            buffID : 9,
            isPermanent: true,
            tapBonus: 0,
            secBonus: 1f,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff10()
    {
        return new ArtifactData(
            name: "Lucky Coin",
            description: "Increases the gold per tap and per second by 30%.",
            buffID : 10,
            isPermanent: true,
            tapBonus: 0.3f,
            secBonus: 0.3f,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff11()
    {
        return new ArtifactData(
            name: "Spell Book",
            description: "Increases the gold per tap and per second by 40%.",
            buffID : 11,
            isPermanent: true,
            tapBonus: 0.4f,
            secBonus: 0.4f,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff12()
    {
        return new ArtifactData(
            name: "Papyrus",
            description: "Increases the gold per tap and per second by 50%.",
            buffID : 12,
            isPermanent: true,
            tapBonus: 0.5f,
            secBonus: 0.5f,
            tapPlus: 0,
            secPlus: 0,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff13()
    {
        return new ArtifactData(
            name: "Daimyo's Hand Fan",
            description: "Increases the gold per tap by 1000 and per second by 2000.",
            buffID : 13,
            isPermanent: true,
            tapBonus: 0f,
            secBonus: 0f,
            tapPlus: 1000,
            secPlus: 2000,
            buffTimeBonus: 0
        );
    }


    public static ArtifactData GetArtifactBuff14()
    {
        return new ArtifactData(
            name: "Four-Leaf Clover",
            description: "Increases the gold per tap by 10000 and per second by 20000.",
            buffID : 14,
            isPermanent: true,
            tapBonus: 0f,
            secBonus: 0f,
            tapPlus: 10000,
            secPlus: 20000,
            buffTimeBonus: 0
        );
    }

    public static ArtifactData GetArtifactBuff15()
    {
        return new ArtifactData(
            name: "Easter Egg",
            description: "Increases the gold per tap by 100000 and per second by 200000.",
            buffID : 15,
            isPermanent: true,
            tapBonus: 0f,
            secBonus: 0f,
            tapPlus: 100000,
            secPlus: 200000,
            buffTimeBonus: 0
        );
    }

    public static List<ArtifactData> GetArtifactList(bool[] arr)
    {
        var list = new List<ArtifactData>();
        if (arr[0])
        {
            list.Add(GetArtifactBuff1());
        }

        if (arr[1])
        {
            list.Add(GetArtifactBuff2());
        }
        
        if (arr[2])
        {
            list.Add(GetArtifactBuff3());
        }

        if (arr[3])
        {
            list.Add(GetArtifactBuff4());
        }

        if (arr[4])
        {
            list.Add(GetArtifactBuff5());
        }

        if (arr[5])
        {
            list.Add(GetArtifactBuff6());
        }

        if (arr[6])
        {
            list.Add(GetArtifactBuff7());
        }

        if (arr[7])
        {
            list.Add(GetArtifactBuff8());
        }

        if (arr[8])
        {
            list.Add(GetArtifactBuff9());
        }

        if (arr[9])
        {
            list.Add(GetArtifactBuff10());
        }

        if (arr[10])
        {
            list.Add(GetArtifactBuff11());
        }

        if (arr[11])
        {
            list.Add(GetArtifactBuff12());
        }

        if (arr[12])
        {
            list.Add(GetArtifactBuff13());
        }

        if (arr[13])
        {
            list.Add(GetArtifactBuff14());
        }

        if (arr[14])
        {
            list.Add(GetArtifactBuff15());
        }

        return list;
    }
}
