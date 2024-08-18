using UnityEngine;

// 오디오 관리 클래스입니다. 배경음악의 볼륨을 조절합니다.
public static class AudioManager
{
    private static AudioSource bgm;

    // 볼륨 상태를 설정합니다
    public static void SetVolumeState(bool value)
    {
        if(bgm == null)
        {
            bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        }

        if(value)
        {
            bgm.volume = 1;
        }
        else
        {
            bgm.volume = 0;
        }
    }

    // 볼륨 상태를 가져옵니다
    public static bool GetVolumeState()
    {
        if(bgm == null)
        {
            bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        }

        return bgm.volume == 1;
    }
}
