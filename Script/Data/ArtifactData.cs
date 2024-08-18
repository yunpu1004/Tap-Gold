using System;
using System.Collections.Generic;
using UnityEngine;

// 이 클래스는 아티팩트 데이터를 관리합니다
// CSV 파일에서 데이터를 읽어와서 Dictionary에 저장하고, 필요한 데이터를 제공합니다
public class ArtifactData
{
    private static Dictionary<int, ArtifactData> dict;

    public int id { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public string spritePath { get; private set; }
    public Sprite sprite { get; private set; }
    public float tapBonus { get; private set; }
    public float secBonus { get; private set; }
    public int tapPlus { get; private set; }
    public int secPlus { get; private set; }
    public float prestigeBonus { get; private set; }

    private ArtifactData(){}

    // id에 맞는 아티팩트 데이터를 제공합니다
    public static ArtifactData GetArtifactData(int id)
    {
        if (dict == null) LoadDataFromCSV();
        return dict[id];
    }

    // CSV 파일에서 데이터를 읽어와서 Dictionary에 저장합니다
    private static void LoadDataFromCSV()
    {
        dict = new Dictionary<int, ArtifactData>();

        // CSV 파일을 불러옴
        var csv = Resources.Load<TextAsset>("ArtifactDatas");

        // 줄 단위로 나눔
        string[] lines = csv.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        //각 줄을 아티팩트 데이터로 변환
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            int id = int.Parse(values[0]);
            string name = values[1];
            string description = values[2];
            string spritePath = values[3];
            float tapBonus = float.Parse(values[4]);
            float secBonus = float.Parse(values[5]);
            int tapPlus = int.Parse(values[6]);
            int secPlus = int.Parse(values[7]);
            float prestigeBonus = float.Parse(values[8]);

            // Sprite는 유니티의 리소스에서 로드
            Sprite sprite = Resources.Load<Sprite>(spritePath);

            // ArtifactData 객체를 생성
            ArtifactData artifact = new ArtifactData
            {
                id = id,
                name = name,
                description = description,
                spritePath = spritePath,
                sprite = sprite,
                tapBonus = tapBonus,
                secBonus = secBonus,
                tapPlus = tapPlus,
                secPlus = secPlus,
                prestigeBonus = prestigeBonus
            };

            // Dictionary에 추가
            dict.Add(id, artifact);
        }
    }
}